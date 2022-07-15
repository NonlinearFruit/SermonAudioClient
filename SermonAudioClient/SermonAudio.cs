using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using SermonAudioClient.Interfaces;
using SermonAudioClient.Utilities.Interfaces;

namespace SermonAudioClient;

public class SermonAudio : ISermonAudio
{
    private readonly IHttpClient _http;
    private readonly string _apiKey;
    private readonly IFile _file;

    public SermonAudio(IHttpClient http, string apiKey, IFile file)
    {
        _http = http;
        _apiKey = apiKey;
        _file = file;
    }

    public string CreateSermon(Sermon sermon)
    {
        var sermonId = CreateSermonMetadata(sermon);
        UploadAudioFileToSermon(sermonId, sermon);
        return default;
    }

    private string CreateSermonMetadata(Sermon sermon)
    {
        var queries = HttpUtility.ParseQueryString(string.Empty);
        queries.Add("fullTitle", sermon.Title);
        queries.Add("speakerName", sermon.Speaker);
        queries.Add("bibleText", sermon.BibleText);
        queries.Add("preachDate", sermon.PreachDate.ToString("yyyy-MM-dd"));
        queries.Add("acceptAdditionalCharges", "false");
        var urlBuilder = new UriBuilder("https://api.sermonaudio.com/v2/node/sermons")
        {
            Query = queries.ToString()
        };
        var response = _http.Send(new HttpRequestMessage(HttpMethod.Post, urlBuilder.Uri)
        {
            Headers = {{"X-Api-Key", _apiKey}}
        });
        return JsonSerializer.Deserialize<SermonAudioResponse>(response?.Content.ReadAsStringAsync().Result ?? "{}")?.SermonId ?? "";
    }

    private void UploadAudioFileToSermon(string sermonId, Sermon sermon)
    {
        var data = _file.GetFileData(sermon.AudioFile);
        var queries = HttpUtility.ParseQueryString(string.Empty);
        queries.Add("sermonID", sermonId);
        queries.Add("originalFilename", sermon.AudioFile);
        queries.Add("uploadType", "original-audio");
        var urlBuilder = new UriBuilder("https://api.sermonaudio.com/v2/media")
        {
            Query = queries.ToString()
        };
        var _ = _http.Send(new HttpRequestMessage(HttpMethod.Post, urlBuilder.Uri)
        {
            Headers = {{"X-Api-Key", _apiKey}}
        });
    }
}