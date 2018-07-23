using Newtonsoft.Json;
using System;

namespace IAttendanceWebAPI.Core.Dtos
{
    public class TimeSlotDto
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("start_time")] public TimeSpan StartTime { get; set; }

        [JsonProperty("end_time")] public TimeSpan EndTime { get; set; }
    }
}