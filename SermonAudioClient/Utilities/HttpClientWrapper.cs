using SermonAudioClient.Utilities.Interfaces;

namespace SermonAudioClient.Utilities;

public class HttpClientWrapper : IHttpClient
{
    private readonly HttpClient _http;

    public HttpClientWrapper()
    {
        _http = new HttpClient();
    }

    public HttpResponseMessage Send(HttpRequestMessage message)
    {
        return _http.Send(message);
    }
}