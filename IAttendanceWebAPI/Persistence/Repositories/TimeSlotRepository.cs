using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class TimeSlotRepository : Repository<TimeSlot>, ITimeSlotRepository
    {
        public TimeSlotRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}