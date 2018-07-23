using IAttendanceWebAPI.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Core.Repositories
{
    public interface IStatusAttendanceRepository : IRepository<StatusAttendance>
    {
        Task<StatusAttendance> GetStatusAttendance(int statusId);
        Task<IEnumerable<StatusAttendance>> GetStatusAttendanceList();
    }
}