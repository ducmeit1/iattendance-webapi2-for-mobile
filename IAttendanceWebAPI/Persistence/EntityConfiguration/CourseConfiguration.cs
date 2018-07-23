using IAttendanceWebAPI.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            HasKey(c => c.Id).HasIndex(c => c.Id);
            Property(t => t.Name).IsRequired().HasMaxLength(250);
            HasMany(c => c.TimesTables).WithRequired(c => c.Course).HasForeignKey(c => c.CourseId)
                .WillCascadeOnDelete(false);
        }
    }
}