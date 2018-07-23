using System;

namespace IAttendanceWebAPI.Core.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        public User FromUser { get; set; }
        public string ToUserId { get; set; }
        public User ToUser { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public int StatusMessageId { get; set; }
        public StatusMessage StatusMessage { get; set; }
        public int TimeTableId { get; set; }
        public TimeTable TimeTable { get; set; }
        public DateTime Date { get; set; }
    }
}