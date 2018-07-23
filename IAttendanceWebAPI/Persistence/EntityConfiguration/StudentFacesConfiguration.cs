using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class StudentFacesConfiguration : EntityTypeConfiguration<StudentFace>
    {
        public StudentFacesConfiguration()
        {
            HasKey(s => s.Id).HasIndex(s => s.Id);
            Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(s => s.Uri).IsRequired();
            Property(s => s.SecureUri).IsRequired();
            HasRequired(s => s.IdentityStudent).WithMany().HasForeignKey(s => s.PersonId).WillCascadeOnDelete(false);
        }
    }
}