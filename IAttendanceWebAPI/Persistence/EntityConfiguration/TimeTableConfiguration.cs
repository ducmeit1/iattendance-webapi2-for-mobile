using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class TimeTableConfiguration : EntityTypeConfiguration<TimeTable>
    {
        public TimeTableConfiguration()
        {
            HasKey(t => t.Id).HasIndex(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(t => t.Attendances).WithRequired(t => t.TimeTable).HasForeignKey(t => t.TimeTableId)
                .WillCascadeOnDelete(false);
            HasMany(t => t.RecognitionImages).WithRequired(t => t.TimeTable).HasForeignKey(t => t.TimeTableId)
                .WillCascadeOnDelete(false);
            HasMany(t => t.Messages).WithRequired(t => t.TimeTable).HasForeignKey(t => t.TimeTableId)
                .WillCascadeOnDelete(false);
        }
    }
}