using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class StudentGroupRepository : Repository<StudentGroup>, IStudentGroupRepository
    {
        public StudentGroupRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}