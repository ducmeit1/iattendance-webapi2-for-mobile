using System;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITeacherRepository Teachers { get; }
        IStudentRepository Students { get; }
        ICourseRepository Courses { get; }
        IRoomRepository Rooms { get; }
        ICampusRepository Campuses { get; }
        IAttendanceRepository Attendances { get; }
        IStatusAttendanceRepository StatusAttendances { get; }
        IStatusTimeTableRepository StatusTimeTables { get; }
        ITimeSlotRepository TimeSlots { get; }
        ITimeTableRepository TimeTables { get; }
        IStudentGroupRepository StudentGroups { get; }
        IStatusTakeAttendanceRepository StatusTakeAttendances { get; }
        IStudentFacesRepository StudentFaces { get; }
        IIdentityStudentRepository IdentityStudents { get; }
        IRecognitionImageRepository RecognitionImages { get; }
        IMessageRepository Messages { get; }
        IStatusMessageRepository StatusesMessage { get; }
        Task<int> Completed();
    }
}