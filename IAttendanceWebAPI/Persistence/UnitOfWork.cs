using IAttendanceWebAPI.Core.Repositories;
using IAttendanceWebAPI.Persistence.Repositories;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork()
        {
            _dbContext = new ApplicationDbContext();
            Users = new UserRepository(_dbContext);
            Teachers = new TeacherRepository(_dbContext);
            Students = new StudentRepository(_dbContext);
            Courses = new CourseRepository(_dbContext);
            Rooms = new RoomRepository(_dbContext);
            Campuses = new CampusRepository(_dbContext);
            Attendances = new AttendanceRepository(_dbContext);
            StatusAttendances = new StatusAttendanceRepository(_dbContext);
            StatusTimeTables = new StatusTimeTableRepository(_dbContext);
            TimeSlots = new TimeSlotRepository(_dbContext);
            TimeTables = new TimeTableRepository(_dbContext);
            StudentGroups = new StudentGroupRepository(_dbContext);
            StatusTakeAttendances = new StatusTakeAttendanceRepository(_dbContext);
            StudentFaces = new StudentFacesRepository(_dbContext);
            IdentityStudents = new IdentityStudentRepository(_dbContext);
            RecognitionImages = new RecognitionImageRepository(_dbContext);
            Messages = new MessageRepository(_dbContext);
            StatusesMessage = new StatusMessageRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public IUserRepository Users { get; }
        public ITeacherRepository Teachers { get; }
        public IStudentRepository Students { get; }
        public ICourseRepository Courses { get; }
        public IRoomRepository Rooms { get; }
        public ICampusRepository Campuses { get; }
        public IAttendanceRepository Attendances { get; }
        public IStatusAttendanceRepository StatusAttendances { get; }
        public IStatusTimeTableRepository StatusTimeTables { get; }
        public ITimeSlotRepository TimeSlots { get; }
        public ITimeTableRepository TimeTables { get; }
        public IStudentGroupRepository StudentGroups { get; }
        public IStatusTakeAttendanceRepository StatusTakeAttendances { get; }
        public IStudentFacesRepository StudentFaces { get; }
        public IIdentityStudentRepository IdentityStudents { get; }
        public IRecognitionImageRepository RecognitionImages { get; }
        public IMessageRepository Messages { get; }
        public IStatusMessageRepository StatusesMessage { get; }

        public async Task<int> Completed()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}