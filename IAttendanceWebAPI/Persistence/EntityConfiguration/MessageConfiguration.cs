using IAttendanceWebAPI.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAttendanceWebAPI.Persistence.EntityConfiguration
{
    public class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            HasKey(m => m.Id).HasIndex(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(m => m.FromUser).WithMany().HasForeignKey(m => m.FromUserId);
            HasRequired(m => m.ToUser).WithMany().HasForeignKey(m => m.ToUserId);
            Property(m => m.Topic).IsRequired().HasMaxLength(250);
            Property(m => m.Description).IsRequired();
            Property(m => m.Date).IsRequired();
            HasRequired(m => m.TimeTable).WithMany().HasForeignKey(m => m.TimeTableId);
            HasRequired(m => m.StatusMessage).WithMany().HasForeignKey(m => m.StatusMessageId);
        }
    }
}