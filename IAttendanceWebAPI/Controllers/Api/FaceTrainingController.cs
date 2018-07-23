using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;
using IAttendanceWebAPI.Core.ViewModels;
using IAttendanceWebAPI.Persistence.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Authorize = IAttendanceWebAPI.Persistence.Filters.AuthorizeAttribute;
using CloudinaryAPI = IAttendanceWebAPI.Persistence.Helpers.CloudinaryUploadHelper;
using FaceAPI = IAttendanceWebAPI.Persistence.Helpers.AzureCognitiveFaceApiHelper;

namespace IAttendanceWebAPI.Controllers.Api
{
    [Authorize(Roles = "Admin, Teacher")]
    [RoutePrefix("api/face")]
    [ValidationModelState]
    public class FaceTrainingController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FaceTrainingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("create/group")]
        public async Task<IHttpActionResult> CreatePersonGroup([FromBody] PersonGroupBindingModel model)
        {
            await FaceAPI.CreatePersonGroup(model);
            return Ok();
        }

        [HttpPost]
        [ValidationMimeMultipart]
        [Route("create")]
        public async Task<IHttpActionResult> CreatePersonInPersonGroup([FromUri] string studentId)
        {
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
            var uploadResults = (await CloudinaryAPI.UploadImagesIncludedFaceToCloudinary(fileUploads)).ToArray();

            var identifiedStudent =
                await _unitOfWork.IdentityStudents.GetIdentityStudent(FaceAPI.PersonGroupId, studentId);
            if (identifiedStudent != null)
                await AddFacesPersonIntoPersonGroup(identifiedStudent, uploadResults);
            else
                await AddPersonIntoPersonGroup(studentId, uploadResults);
            return Ok();
        }

        [HttpPost]
        [Route("training")]
        public async Task<IHttpActionResult> TrainingThePersonGroup()
        {
            await FaceAPI.TrainingThePersonGroup(FaceAPI.PersonGroupId);
            return Ok();
        }

        private bool ValidateFileUpload(FileUpload[] fileUploads)
        {
            var isInvalidFiles = false;
            foreach (var file in fileUploads)
            {
                var validate = CloudinaryAPI.ValidateFileUpload(file);
                if (validate >= 0)
                {
                    var errorsLibrary = CloudinaryAPI.FileUploadErrorsDictionary;
                    ModelState.AddModelError(errorsLibrary.Keys.ElementAt(validate),
                        errorsLibrary[errorsLibrary.Keys.ElementAt(validate)]);
                    isInvalidFiles = true;
                }
            }

            return isInvalidFiles;
        }

        private async Task AddFacesPersonIntoPersonGroup(IdentityStudent identifiedStudent,
            CloudinaryResult[] imagesUploaded)
        {
            var addFacesModel = new AddFacesBindingModel
            {
                PersonGroupId = FaceAPI.PersonGroupId,
                PersonId = new Guid(identifiedStudent.PersonId),
                FacesUrl = imagesUploaded.Select(c => c.SecureUri)
            };
            await FaceAPI.AddFacesInPerson(addFacesModel);

            foreach (var result in imagesUploaded)
                identifiedStudent.StudentFaces.Add(new StudentFace
                {
                    PersonId = identifiedStudent.PersonId,
                    Uri = result.Uri,
                    SecureUri = result.SecureUri
                });

            _unitOfWork.IdentityStudents.Update(identifiedStudent);
            await _unitOfWork.Completed();
        }

        private async Task AddPersonIntoPersonGroup(string studentId, CloudinaryResult[] imagesUploaded)
        {
            var person = new PersonBindingModel
            {
                PersonGroupId = FaceAPI.PersonGroupId,
                Name = studentId,
                FacesUrl = imagesUploaded.Select(c => c.SecureUri)
            };
            var personId = await FaceAPI.CreatePersonInPersonGroup(person);
            _unitOfWork.IdentityStudents.Add(new IdentityStudent
            {
                StudentId = studentId,
                PersonGroupId = FaceAPI.PersonGroupId,
                PersonId = personId,
                StudentFaces = imagesUploaded.Select(c => new StudentFace
                {
                    PersonId = personId,
                    Uri = c.Uri,
                    SecureUri = c.SecureUri
                }).ToList()
            });
            await _unitOfWork.Completed();
        }
    }
}