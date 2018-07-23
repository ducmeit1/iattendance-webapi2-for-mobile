using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class StatusMessageConfiguration : EntityTypeConfiguration<StatusMessage>
    {
        public StatusMessageConfiguration()
        {
            HasKey(s => s.Id).HasIndex(s => s.Id);
            Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(s => s.Name).IsRequired().HasMaxLength(50);
            HasMany(s => s.Messages).WithRequired(s => s.StatusMessage).HasForeignKey(s => s.StatusMessageId)
                .WillCascadeOnDelete(false);
        }
    }
}