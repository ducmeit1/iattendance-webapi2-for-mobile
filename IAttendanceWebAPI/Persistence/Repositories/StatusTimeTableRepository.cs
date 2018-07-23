using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class StatusTimeTableRepository : Repository<StatusTimeTable>, IStatusTimeTableRepository
    {
        public StatusTimeTableRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}