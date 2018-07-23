using IAttendanceWebAPI.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(a => new {a.TimeTableId, a.StudentId}).HasIndex(a => new {a.TimeTableId, a.StudentId});

            HasRequired(a => a.Student).WithMany(a => a.Attendances).HasForeignKey(a => a.StudentId)
                .WillCascadeOnDelete(false);

            HasRequired(a => a.TimeTable).WithMany(a => a.Attendances).HasForeignKey(a => a.TimeTableId)
                .WillCascadeOnDelete(false);

            HasRequired(a => a.StatusAttendance).WithMany(a => a.Attendances).HasForeignKey(a => a.StatusAttendanceId)
                .WillCascadeOnDelete(false);
        }
    }
}