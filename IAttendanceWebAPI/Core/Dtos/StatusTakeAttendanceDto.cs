using Newtonsoft.Json;

namespace IAttendanceWebAPI.Core.Dtos
{
    public class StatusTakeAttendanceDto
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}