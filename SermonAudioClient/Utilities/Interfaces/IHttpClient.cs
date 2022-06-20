namespace SermonAudioClient.Utilities.Interfaces;

public interface IHttpClient
{
    HttpResponseMessage Send(HttpRequestMessage message);
}