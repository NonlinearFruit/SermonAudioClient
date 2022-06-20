using System.Text.Json.Serialization;

namespace SermonAudioClient;

public class SermonAudioResponse
{
    public string? Message { get; set; }
    [JsonPropertyName("sermonID")]
    public string? SermonId { get; set; }
}