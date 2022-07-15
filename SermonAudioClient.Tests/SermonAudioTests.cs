using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Web;
using SermonAudioClient.Interfaces;
using SermonAudioClient.Tests.TestDoubles.Utilities;

namespace SermonAudioClient.Tests;

public class SermonAudioTests
{
    private readonly ISermonAudio _sermonAudio;
    private readonly HttpClient_TestDouble _http;
    private readonly string _apiKey;
    private readonly File_TestDouble _file;

    public SermonAudioTests()
    {
        _apiKey = "broke-strop-black-flame";
        _http = new HttpClient_TestDouble();
        _file = new File_TestDouble();
        _sermonAudio = new SermonAudio(_http, _apiKey, _file);
    }

    [Fact]
    public void creates_the_sermon()
    {
        var title = "Sinners in the Hands of an Angry God";
        var speaker = "Jonathan Edwards";
        var bible = "Deuteronomy 32:35";
        var date = new DateTime(1741, 7, 8);
        var sermon = new Sermon
        {
            Title = title,
            Speaker = speaker,
            BibleText = bible,
            PreachDate = date,
        };

        _sermonAudio.CreateSermon(sermon);

        var message = _http.FirstMessagePassedTo_Send;
        Assert.Equal(HttpMethod.Post, message.Method);
        AssertContainsHeader(message.Headers, "X-Api-Key", _apiKey);
        AssertUrlsMatch("https://api.sermonaudio.com/v2/node/sermons", message.RequestUri);
        var queryParams = HttpUtility.ParseQueryString(message.RequestUri.Query);
        AssertContainsQueryParameter(queryParams, "fullTitle", title);
        AssertContainsQueryParameter(queryParams, "speakerName", speaker);
        AssertContainsQueryParameter(queryParams, "bibleText", bible);
        AssertContainsQueryParameter(queryParams, "preachDate", $"{date:yyyy-MM-dd}");
    }

    [Fact]
    public void uploads_file()
    {
        var sermonId = "549824086";
        _http.ReturnFor_Send = new HttpResponseMessage
        {
            Content = new StringContent($"{{\"sermonID\": \"{sermonId}\"}}")
        };
        var sermon = new Sermon
        {
            Title = "",
            Speaker = "",
            BibleText = "",
            PreachDate = default,
            AudioFile = "the_sermon_audio.mp3"
        };

        _sermonAudio.CreateSermon(sermon);

        var message = _http.LastMessagePassedTo_Send;
        Assert.Equal(HttpMethod.Post, message.Method);
        AssertContainsHeader(message.Headers, "X-Api-Key", _apiKey);
        AssertUrlsMatch("https://api.sermonaudio.com/v2/media", message.RequestUri);
        var queryParams = HttpUtility.ParseQueryString(message.RequestUri.Query);
        AssertContainsQueryParameter(queryParams, "sermonID", sermonId);
        AssertContainsQueryParameter(queryParams, "originalFilename", sermon.AudioFile);
        AssertContainsQueryParameter(queryParams, "uploadType", "original-audio");
    }

    [Fact]
    public void gets_data_from_audio_file()
    {
        var sermon = new Sermon
        {
            Title = "",
            Speaker = "",
            BibleText = "",
            PreachDate = default,
            AudioFile = "the_sermon_audio.mp3"
        };

        _sermonAudio.CreateSermon(sermon);

        Assert.Equal(Verify.Once, _file.CountOfCallsTo_GetFileData);
        Assert.Equal(sermon.AudioFile, _file.LastFilePassedTo_GetFileData);
    }

    private static void AssertUrlsMatch(string expectedUrl, Uri actualUrl)
    {
        Assert.Equal(expectedUrl, actualUrl.GetLeftPart(UriPartial.Path));
    }

    private static void AssertContainsHeader(HttpRequestHeaders headers, string header, string value)
    {
        Assert.True(headers.Contains(header));
        headers.TryGetValues(header, out var values);
        Assert.Contains(value, values);
    }

    private static void AssertContainsQueryParameter(NameValueCollection queryParams, string key, string value)
    {
        Assert.Contains(key, queryParams.AllKeys);
        Assert.Equal(value, queryParams.Get(key));
    }
}