using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class StatusMessageRepository : Repository<StatusMessage>, IStatusMessageRepository
    {
        public StatusMessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}