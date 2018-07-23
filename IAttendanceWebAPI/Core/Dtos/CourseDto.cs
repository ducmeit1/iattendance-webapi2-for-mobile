using Newtonsoft.Json;

namespace IAttendanceWebAPI.Core.Dtos
{
    public class CourseDto
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}