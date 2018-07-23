using Newtonsoft.Json;
using System;

namespace IAttendanceWebAPI.Core.Dtos
{
    public class MessageDto
    {
        [JsonProperty("message_id")] public int Id { get; set; }
        [JsonProperty("sender")] public string Sender { get; set; }
        [JsonProperty("receiver")] public string Receiver { get; set; }
        [JsonProperty("topic")] public string Topic { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("date_timetable")] public DateTime TimeTable { get; set; }
        [JsonProperty("slot")] public TimeSlotDto TimeSlot { get; set; }
        [JsonProperty("date_created")] public DateTime Date { get; set; }
        [JsonProperty("status_message")] public StatusMessageDto StatusMessage { get; set; }
    }
}