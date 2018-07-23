using IAttendanceWebAPI.Core.Entities;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Core.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<Teacher> GetTeacher(string userId);
    }
}