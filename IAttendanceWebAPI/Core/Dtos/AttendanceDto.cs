using Newtonsoft.Json;

namespace IAttendanceWebAPI.Core.Dtos
{
    public class AttendanceDto
    {
        [JsonProperty("time_table_id")] public int TimeTableId { get; set; }

        [JsonProperty("student")] public StudentDto Student { get; set; }

        [JsonProperty("status_attendance")] public StatusAttendanceDto StatusAttendance { get; set; }
    }
}