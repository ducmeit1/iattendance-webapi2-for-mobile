using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class StudentFacesRepository : Repository<StudentFace>, IStudentFacesRepository
    {
        public StudentFacesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}