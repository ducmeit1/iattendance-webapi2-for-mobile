using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class StatusTakeAttendanceRepository : Repository<StatusTakeAttendance>, IStatusTakeAttendanceRepository
    {
        public StatusTakeAttendanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}