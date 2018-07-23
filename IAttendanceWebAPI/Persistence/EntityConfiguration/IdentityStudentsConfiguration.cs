using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class IdentityStudentsConfiguration : EntityTypeConfiguration<IdentityStudent>
    {
        public IdentityStudentsConfiguration()
        {
            HasKey(i => i.PersonId).HasIndex(i => i.PersonId);
            Property(i => i.PersonId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(i => i.PersonGroupId).IsRequired().HasMaxLength(100);
            HasRequired(i => i.Student).WithMany().HasForeignKey(i => i.StudentId).WillCascadeOnDelete(true);
            HasMany(i => i.StudentFaces).WithRequired(i => i.IdentityStudent).HasForeignKey(i => i.PersonId)
                .WillCascadeOnDelete(false);
        }
    }
}