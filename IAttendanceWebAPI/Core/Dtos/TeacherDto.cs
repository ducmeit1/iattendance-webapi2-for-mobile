using Newtonsoft.Json;

namespace IAttendanceWebAPI.Core.Dtos
{
    public class TeacherDto
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("teacher_detail")] public UserDto User { get; set; }
    }
}