using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Teacher> GetTeacher(string userId)
        {
            return await Get(predicate: t => t.UserId == userId);
        }
    }
}