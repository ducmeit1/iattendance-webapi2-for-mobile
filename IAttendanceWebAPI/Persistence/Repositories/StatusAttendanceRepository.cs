using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class StatusAttendanceRepository : Repository<StatusAttendance>, IStatusAttendanceRepository
    {
        public StatusAttendanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<StatusAttendance> GetStatusAttendance(int statusId)
        {
            return await Get(predicate: s => s.Id == statusId);
        }

        public async Task<IEnumerable<StatusAttendance>> GetStatusAttendanceList()
        {
            return await Entities.ToListAsync();
        }
    }
}