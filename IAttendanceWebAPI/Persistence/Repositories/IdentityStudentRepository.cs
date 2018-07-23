using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class IdentityStudentRepository : Repository<IdentityStudent>, IIdentityStudentRepository
    {
        public IdentityStudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IdentityStudent> GetIdentityStudent(string personGroupId, string studentId)
        {
            return await Get(predicate: s =>
                s.PersonGroupId.Equals(personGroupId, StringComparison.InvariantCultureIgnoreCase) &&
                s.StudentId.Equals(studentId, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}