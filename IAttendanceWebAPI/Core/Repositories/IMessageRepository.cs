using IAttendanceWebAPI.Core.Dtos;
using IAttendanceWebAPI.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Core.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<MessageDto>> GetMessages(string userId);
    }
}