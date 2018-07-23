using AutoMapper;
using IAttendanceWebAPI.Core.Dtos;
using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<MessageDto>> GetMessages(string userId)
        {
            var messages = (await Get(m => m.FromUserId == userId || m.ToUserId == userId, m => m.OrderBy(t => t.Id),
                "TimeTable, TimeTable.TimeSlot, StatusMessage")).ToList();

            var messagesDto = await Task.FromResult(messages.Select(c => new MessageDto
            {
                Id = c.Id,
                TimeTable = c.TimeTable.Date,
                Sender = (DbContext.Students.FirstOrDefault(r => r.UserId == c.FromUserId))?.Id ??
                (DbContext.Teachers.FirstOrDefault(r => r.UserId == c.FromUserId))?.Id,
                Receiver = (DbContext.Students.FirstOrDefault(r => r.UserId == c.ToUserId))?.Id ??
                    (DbContext.Teachers.FirstOrDefault(r => r.UserId == c.ToUserId))?.Id,
                Date = c.Date,
                TimeSlot = Mapper.Map<TimeSlot, TimeSlotDto>(c.TimeTable.TimeSlot),
                StatusMessage = Mapper.Map<StatusMessage, StatusMessageDto>(c.StatusMessage),
                Description = c.Description,
                Topic = c.Topic
            }));

            return messagesDto;
        }
    }
}