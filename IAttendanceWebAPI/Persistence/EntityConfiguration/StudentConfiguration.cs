using IAttendanceWebAPI.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            HasKey(s => s.Id).HasIndex(s => s.Id);
            Property(s => s.Batch).IsRequired().HasMaxLength(20);
            Property(s => s.ImagePath).IsRequired();
            HasRequired(s => s.User).WithMany().HasForeignKey(s => s.UserId).WillCascadeOnDelete(true);
            HasMany(s => s.Attendances).WithRequired(s => s.Student).HasForeignKey(s => s.StudentId)
                .WillCascadeOnDelete(false);
            HasMany(s => s.StudentGroups).WithMany(s => s.Students).Map(s =>
            {
                s.MapLeftKey("StudentId");
                s.MapRightKey("StudentGroupId");
                s.ToTable("StudentsInGroup");
            });
        }
    }
}