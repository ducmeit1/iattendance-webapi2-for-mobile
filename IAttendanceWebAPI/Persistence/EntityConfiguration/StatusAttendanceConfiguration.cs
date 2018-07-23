using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class StatusAttendanceConfiguration : EntityTypeConfiguration<StatusAttendance>
    {
        public StatusAttendanceConfiguration()
        {
            HasKey(s => s.Id).HasIndex(s => s.Id);
            Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(s => s.Name).IsRequired().HasMaxLength(100);
            HasMany(s => s.Attendances).WithRequired(s => s.StatusAttendance).HasForeignKey(s => s.StatusAttendanceId)
                .WillCascadeOnDelete(false);
        }
    }
}