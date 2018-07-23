using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Persistence.EntityConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace IAttendanceWebAPI.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("IAttendanceConnection", false)
        {
        }

        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<StatusTimeTable> StatusTimeTables { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<StatusAttendance> StatusAttendances { get; set; }
        public DbSet<StatusTakeAttendance> StatusTakeAttendances { get; set; }
        public DbSet<StudentFace> StudentFaces { get; set; }
        public DbSet<IdentityStudent> IdentityStudents { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<StatusMessage> StatusMessages { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AttendanceConfiguration());
            modelBuilder.Configurations.Add(new CampusConfiguration());
            modelBuilder.Configurations.Add(new RoomConfiguration());
            modelBuilder.Configurations.Add(new CourseConfiguration());
            modelBuilder.Configurations.Add(new StatusAttendanceConfiguration());
            modelBuilder.Configurations.Add(new StatusTimeTableConfiguration());
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new StudentGroupConfiguration());
            modelBuilder.Configurations.Add(new TeacherConfiguration());
            modelBuilder.Configurations.Add(new TimeSlotConfiguration());
            modelBuilder.Configurations.Add(new TimeTableConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new StatusTakeAttendanceConfiguration());
            modelBuilder.Configurations.Add(new StudentFacesConfiguration());
            modelBuilder.Configurations.Add(new IdentityStudentsConfiguration());
            modelBuilder.Configurations.Add(new RecognitionImageConfiguration());
            modelBuilder.Configurations.Add(new MessageConfiguration());
            modelBuilder.Configurations.Add(new StatusMessageConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}