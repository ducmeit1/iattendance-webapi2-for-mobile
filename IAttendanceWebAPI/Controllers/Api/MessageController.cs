using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;
using IAttendanceWebAPI.Persistence.Filters;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http;
using Authorize = IAttendanceWebAPI.Persistence.Filters.AuthorizeAttribute;

namespace IAttendanceWebAPI.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/message")]
    [ValidationModelState]
    public class MessageController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        [Route("report")]
        public async Task<IHttpActionResult> SendMessage([FromUri] int timeTableId,
            [FromBody] MessageBindingModel model)
        {
            var timeTable = await _unitOfWork.TimeTables.GetTimeTable(timeTableId);
            if (timeTable == null) return NotFound();
            var fromUserId = User.Identity.GetUserId();
            var toUserId = (await _unitOfWork.Teachers.Get(predicate: c => c.Id == timeTable.TeacherId)).UserId;
            var message = new Message
            {
                TimeTableId = timeTableId,
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Date = DateTime.Now,
                Topic = model.Topic,
                Description = model.Description,
                StatusMessageId = (int) StatusMessageType.Waiting
            };
            _unitOfWork.Messages.Add(message);
            await _unitOfWork.Completed();
            return Ok(new {message_id = message.Id});
        }

        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        [Route("report")]
        public async Task<IHttpActionResult> SendResponseMessage([FromUri] int messageId, [FromUri] int statusId)
        {
            var message = await _unitOfWork.Messages.Get(predicate: p => p.Id == messageId);
            if (message == null) return NotFound();
            var statusMessage = await _unitOfWork.StatusesMessage.Get(predicate: s => s.Id == statusId);
            if (statusMessage == null)
            {
                ModelState.AddModelError("StatusId", "This is invalid status id!");
                return BadRequest(ModelState);
            }

            message.StatusMessageId = statusMessage.Id;
            _unitOfWork.Messages.Update(message);
            await _unitOfWork.Completed();
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Admin, Student")]
        [Route("")]
        public async Task<IHttpActionResult> GetMessages()
        {
            var userId = User.Identity.GetUserId();
            var messages = await _unitOfWork.Messages.GetMessages(userId);
            return Ok(messages);
        }
    }

    public class MessageBindingModel
    {
        [JsonProperty("topic")]
        [Required]
        [MaxLength(255)]
        public string Topic { get; set; }

        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }
    }
}