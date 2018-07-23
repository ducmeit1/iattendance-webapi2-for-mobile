using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class StatusTimeTableConfiguration : EntityTypeConfiguration<StatusTimeTable>
    {
        public StatusTimeTableConfiguration()
        {
            HasKey(s => s.Id).HasIndex(s => s.Id);
            Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(s => s.Name).IsRequired().HasMaxLength(100);
            HasMany(s => s.TimeTables).WithRequired(s => s.StatusTimeTable).HasForeignKey(s => s.StatusTimeTableId)
                .WillCascadeOnDelete(false);
        }
    }
}