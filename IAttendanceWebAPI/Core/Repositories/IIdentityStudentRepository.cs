using IAttendanceWebAPI.Core.Entities;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Core.Repositories
{
    public interface IIdentityStudentRepository : IRepository<IdentityStudent>
    {
        Task<IdentityStudent> GetIdentityStudent(string personGroupId, string studentId);
    }
}