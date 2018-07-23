using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;
using IAttendanceWebAPI.Core.ViewModels;
using IAttendanceWebAPI.Persistence.Filters;
using IAttendanceWebAPI.Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Authorize = IAttendanceWebAPI.Persistence.Filters.AuthorizeAttribute;
using FaceAPI = IAttendanceWebAPI.Persistence.Helpers.AzureCognitiveFaceApiHelper;

namespace IAttendanceWebAPI.Controllers.Api
{
    [Authorize(Roles = "Admin, Teacher")]
    [RoutePrefix("api/attendance")]
    [ValidationModelState]
    public class AttendanceController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] int timeTableId)
        {
            if (await _unitOfWork.TimeTables.GetTimeTable(timeTableId) == null)
                return NotFound();

            var attendancesDto = await _unitOfWork.Attendances.GetAttendancesListForTimeTable(timeTableId);
            return Ok(attendancesDto);
        }

        [HttpPost]
        [Route("update/manual")]
        public async Task<IHttpActionResult> Update([FromUri] int timeTableId, [FromBody] AttendanceBindingModel model)
        {
            var attendance =
                await _unitOfWork.Attendances.GetAttendanceForStudent(timeTableId, model.StudentId);
            if (attendance == null)
                return NotFound();
            var statusAttendance =
                await _unitOfWork.StatusAttendances.GetStatusAttendance(model.StatusAttendanceId);
            if (statusAttendance == null)
            {
                ModelState.AddModelError("StatusAttendanceId",
                    $"{model.StatusAttendanceId} - This is invalid Status Attendance Id");
                return BadRequest(ModelState);
            }

            attendance.StatusAttendanceId = statusAttendance.Id;
            attendance.StatusAttendance = statusAttendance;
            var statusAttendanceDto = await _unitOfWork.Attendances.UpdateAttendanceForStudent(attendance);
            return Ok(statusAttendanceDto);
        }

        [HttpPost]
        [Route("update/manual/all")]
        public async Task<IHttpActionResult> Update([FromUri] int timeTableId,
            [FromBody] List<AttendanceBindingModel> model)
        {
            var timeTable = await _unitOfWork.TimeTables.GetTimeTable(timeTableId);
            if (timeTable == null) return NotFound();
            var statusAttendanceListInDb = await _unitOfWork.StatusAttendances.GetStatusAttendanceList();
            var attendanceListInDb = statusAttendanceListInDb.ToList();
            var statusAttendancesIdListInDb = attendanceListInDb.Select(c => c.Id).ToArray();
            var statusAttendancesIdList = model.Select(c => c.StatusAttendanceId).Distinct().ToArray();
            foreach (var id in statusAttendancesIdList)
                if (!statusAttendancesIdListInDb.Contains(id))
                {
                    ModelState.AddModelError("StatusAttendanceId", $"{0} - This is invalid Status Attendance Id");
                    return BadRequest(ModelState);
                }

            var studentsIdList = model.Select(c => c.StudentId);

            var attendancesListWillBeUpdated =
                (await _unitOfWork.Attendances.GetAttendanceListForStudentsList(timeTableId, studentsIdList)).ToList();

            foreach (var attendance in attendancesListWillBeUpdated)
            {
                var statusAttendanceId = model.FirstOrDefault(c =>
                                             c.StudentId.Equals(attendance.StudentId,
                                                 StringComparison.CurrentCultureIgnoreCase))?.StatusAttendanceId ?? 1;
                var statusAttendance = attendanceListInDb.FirstOrDefault(c => c.Id == statusAttendanceId);
                attendance.StatusAttendanceId = statusAttendanceId;
                attendance.StatusAttendance = statusAttendance;
            }

            var statusAttendancesList =
                await _unitOfWork.Attendances.UpdateAttendanceForStudentList(attendancesListWillBeUpdated);

            timeTable.StatusTakeAttendanceId = (int)StatusTakeAttendanceType.Taken;
            _unitOfWork.TimeTables.Update(timeTable);
            await _unitOfWork.Completed();
            return Ok(statusAttendancesList);
        }

        [HttpPost]
        [ValidationMimeMultipart]
        [Route("update/identity")]
        public async Task<IHttpActionResult> UpdateByFaceScan([FromUri] int timeTableId)
        {
            var timeTable = await _unitOfWork.TimeTables.GetTimeTable(timeTableId);
            if (timeTable == null) return NotFound();
            var provider = await Request.Content.ReadAsMultipartAsync();
            var fileUploads = new List<FileUpload>();
            foreach (var content in provider.Contents)
                if (content.Headers != null)
                    fileUploads.Add(new FileUpload
                    {
                        FileStream = await content.ReadAsStreamAsync(),
                        FileName = content.Headers.ContentDisposition.FileName
                    });

            var isInvalidFile = ValidateFileUpload(fileUploads.ToArray());
            if (isInvalidFile) return BadRequest(ModelState);
            var uploadResults = (await CloudinaryUploadHelper.UploadImagesToCloudinary(fileUploads)).ToArray();

            foreach (var result in uploadResults)
            {
                var recognitionImage = new RecognitionImage
                {
                    TimeTableId = timeTableId,
                    Uri = result.Uri,
                    SecureUri = result.SecureUri
                };
                _unitOfWork.RecognitionImages.Add(recognitionImage);
            }

            await _unitOfWork.Completed();

            var identifiedPersons = (await FaceAPI.RecognizeFaces(new RecognizeFacesBindingModel
            {
                PersonGroupId = FaceAPI.PersonGroupId,
                FacesUrl = uploadResults.Select(c => c.SecureUri)
            })).ToList();

            var studentIds = (await _unitOfWork.Attendances.GetStudentIdListForTimeTable(timeTableId)).ToArray();
            var statusAttendanceListInDb = (await _unitOfWork.StatusAttendances.GetStatusAttendanceList()).ToArray();
            var attendedStatus =
                statusAttendanceListInDb.FirstOrDefault(c => c.Id == (int)StatusAttendanceType.Attended);
            var absentStatus = statusAttendanceListInDb.FirstOrDefault(c => c.Id == (int)StatusAttendanceType.Absent);
            var attendancesListWillBeUpdated =
                (await _unitOfWork.Attendances.GetAttendanceListForStudentsList(timeTableId, studentIds)).ToList();

            if (identifiedPersons.Count == 0)
                attendancesListWillBeUpdated.ForEach(a =>
                {
                    a.StatusAttendanceId = (int)StatusAttendanceType.Absent;
                    a.StatusAttendance = absentStatus;
                });
            else
                attendancesListWillBeUpdated.ForEach(a =>
                {
                    var identifiedPerson = identifiedPersons.FirstOrDefault
                        (ad => ad.Name.Equals(a.StudentId, StringComparison.CurrentCultureIgnoreCase));
                    if (identifiedPerson != null)
                    {
                        a.StatusAttendanceId = (int)StatusAttendanceType.Attended;
                        a.StatusAttendance = attendedStatus;
                    }

                    else
                    {
                        a.StatusAttendanceId = (int)StatusAttendanceType.Absent;
                        a.StatusAttendance = absentStatus;
                    }

                });
            var statusAttendancesList =
                await _unitOfWork.Attendances.UpdateAttendanceForStudentList(attendancesListWillBeUpdated);

            timeTable.StatusTakeAttendanceId = (int)StatusTakeAttendanceType.Taken;
            _unitOfWork.TimeTables.Update(timeTable);
            await _unitOfWork.Completed();
            return Ok(statusAttendancesList);
        }

        private bool ValidateFileUpload(FileUpload[] fileUploads)
        {
            var isInvalidFiles = false;
            foreach (var file in fileUploads)
            {
                var validate = CloudinaryUploadHelper.ValidateFileUpload(file);
                if (validate >= 0)
                {
                    var errorsLibrary = CloudinaryUploadHelper.FileUploadErrorsDictionary;
                    ModelState.AddModelError(errorsLibrary.Keys.ElementAt(validate),
                        errorsLibrary[errorsLibrary.Keys.ElementAt(validate)]);
                    isInvalidFiles = true;
                }
            }

            return isInvalidFiles;
        }
    }
}