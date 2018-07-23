using IAttendanceWebAPI.Core.Entities;
using System.Data.Entity;

namespace IAttendanceWebAPI.Persistence
{
    public interface IApplicationDbContext
    {
        IDbSet<User> Users { get; set; }
        DbSet<TimeTable> TimeTables { get; set; }
        DbSet<TimeSlot> TimeSlots { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<StudentGroup> StudentGroups { get; set; }
        DbSet<StatusTimeTable> StatusTimeTables { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Campus> Campuses { get; set; }
        DbSet<Attendance> Attendances { get; set; }
        DbSet<StatusAttendance> StatusAttendances { get; set; }
        DbSet<StatusTakeAttendance> StatusTakeAttendances { get; set; }
        DbSet<StudentFace> StudentFaces { get; set; }
        DbSet<IdentityStudent> IdentityStudents { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<StatusMessage> StatusMessages { get; set; }
    }
}