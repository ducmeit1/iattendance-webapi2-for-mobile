using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class TimeSlotConfiguration : EntityTypeConfiguration<TimeSlot>
    {
        public TimeSlotConfiguration()
        {
            HasKey(t => t.Id).HasIndex(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.StartTime).IsRequired();
            Property(t => t.EndTime).IsRequired();
            HasMany(t => t.TimeTables).WithRequired(t => t.TimeSlot).HasForeignKey(t => t.TimeSlotId)
                .WillCascadeOnDelete(false);
        }
    }
}