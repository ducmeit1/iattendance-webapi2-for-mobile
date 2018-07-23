using IAttendanceWebAPI.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Name).HasMaxLength(100).IsOptional();
            Property(u => u.DateOfBirth).IsOptional();
            Property(u => u.PhoneNumber).HasMaxLength(100).IsOptional();
            HasMany(u => u.MessagesReceived).WithRequired(u => u.ToUser).HasForeignKey(u => u.ToUserId)
                .WillCascadeOnDelete(false);
            HasMany(u => u.MessagesSent).WithRequired(u => u.FromUser).HasForeignKey(u => u.FromUserId)
                .WillCascadeOnDelete(false);
        }
    }
}