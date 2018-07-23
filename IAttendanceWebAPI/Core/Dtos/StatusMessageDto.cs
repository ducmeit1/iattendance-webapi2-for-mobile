using Newtonsoft.Json;

namespace IAttendanceWebAPI.Core.Dtos
{
    public class StatusMessageDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }
}