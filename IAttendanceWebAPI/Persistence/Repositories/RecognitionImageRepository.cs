using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class RecognitionImageRepository : Repository<RecognitionImage>, IRecognitionImageRepository
    {
        public RecognitionImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}