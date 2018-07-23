using IAttendanceWebAPI.Core.Entities;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Core.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> GetStudent(string userId);
    }
}