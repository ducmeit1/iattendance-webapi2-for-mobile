using IAttendanceWebAPI.Core.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace IAttendanceWebAPI.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/timetable")]
    public class TimeTableController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TimeTableController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] DateTime date)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.Identity.GetUserId();
            var teacher = await _unitOfWork.Teachers.GetTeacher(userId);
            if (teacher != null)
            {
                var timeTableDto = await _unitOfWork.TimeTables.GetTimeTablesForTeacherByDate(teacher.Id, date);
                return Ok(timeTableDto);
            }

            var student = await _unitOfWork.Students.GetStudent(userId);
            if (student != null)
            {
                var timeTableDto = await _unitOfWork.TimeTables.GetTimeTablesForStudentByDate(student.Id, date);

                return Ok(timeTableDto);
            }

            return NotFound();
        }
    }
}