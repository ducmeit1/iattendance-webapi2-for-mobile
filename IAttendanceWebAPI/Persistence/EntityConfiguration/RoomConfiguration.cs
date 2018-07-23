using IAttendanceWebAPI.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class RoomConfiguration : EntityTypeConfiguration<Room>
    {
        public RoomConfiguration()
        {
            HasKey(r => r.Id).HasIndex(t => t.Id);
            Property(t => t.Name).IsRequired().HasMaxLength(200);
            HasMany(r => r.TimesTables).WithRequired(r => r.Room).HasForeignKey(r => r.RoomId)
                .WillCascadeOnDelete(false);
        }
    }
}