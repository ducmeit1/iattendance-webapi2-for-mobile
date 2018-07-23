using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public enum StatusMessageType
    {
        Waiting = 0,
        Approved = 1,
        Rejected = 2
    }

    public class StatusMessage
    {
        public StatusMessage()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}