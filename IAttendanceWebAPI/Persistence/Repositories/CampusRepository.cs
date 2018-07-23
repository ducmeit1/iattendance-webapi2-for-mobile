using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class CampusRepository : Repository<Campus>, ICampusRepository
    {
        public CampusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}