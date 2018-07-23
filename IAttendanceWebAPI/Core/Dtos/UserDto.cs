using Newtonsoft.Json;
using System;

namespace IAttendanceWebAPI.Core.Dtos
{
    public class UserDto
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("date_of_birth")] public DateTime DateOfBirth { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }
    }
}