using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class StatusTakeAttendanceConfiguration : EntityTypeConfiguration<StatusTakeAttendance>
    {
        public StatusTakeAttendanceConfiguration()
        {
            HasKey(t => t.Id).HasIndex(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Name).IsRequired().HasMaxLength(100);
            HasMany(t => t.TimeTables).WithRequired(t => t.StatusTakeAttendance)
                .HasForeignKey(t => t.StatusTakeAttendanceId).WillCascadeOnDelete(false);
        }
    }
}