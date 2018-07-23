using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class CampusConfiguration : EntityTypeConfiguration<Campus>
    {
        public CampusConfiguration()
        {
            HasKey(t => t.Id).HasIndex(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Name).IsRequired().HasMaxLength(250);
            HasMany(c => c.TimeTables).WithRequired(c => c.Campus).HasForeignKey(c => c.CampusId)
                .WillCascadeOnDelete(false);
        }
    }
}