using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class RecognitionImageConfiguration : EntityTypeConfiguration<RecognitionImage>
    {
        public RecognitionImageConfiguration()
        {
            HasKey(r => r.Id).HasIndex(r => r.Id);
            Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(r => r.Uri).IsRequired();
            Property(r => r.SecureUri).IsRequired();
            HasRequired(r => r.TimeTable).WithMany().HasForeignKey(r => r.TimeTableId).WillCascadeOnDelete(false);
        }
    }
}