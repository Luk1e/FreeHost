using System.Text.Json.Serialization;
using FreeHost.Infrastructure.Utilities;

namespace FreeHost.Infrastructure.Models.DTOs;

public class RegistrationDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Login { get; set; }
    public string UserName { get; set; }

    [JsonConverter(typeof(ByteArrayConverter))]
    public byte[] Photo { get; set; }
}