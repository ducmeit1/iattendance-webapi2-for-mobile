using IAttendanceWebAPI.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class TeacherConfiguration : EntityTypeConfiguration<Teacher>
    {
        public TeacherConfiguration()
        {
            HasKey(t => t.Id).HasIndex(t => t.Id);
            HasRequired(t => t.User).WithMany().HasForeignKey(t => t.UserId).WillCascadeOnDelete(true);
            HasMany(t => t.TimesTables).WithRequired(t => t.Teacher).HasForeignKey(t => t.TeacherId)
                .WillCascadeOnDelete(false);
        }
    }
}