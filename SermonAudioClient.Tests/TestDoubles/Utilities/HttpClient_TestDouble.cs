using System.Diagnostics.CodeAnalysis;
using SermonAudioClient.Utilities.Interfaces;

namespace SermonAudioClient.Tests.TestDoubles.Utilities;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class HttpClient_TestDouble : IHttpClient
{
    public HttpResponseMessage ReturnFor_Send { get; set; }
    public HttpRequestMessage LastMessagePassedTo_Send { get; set; }
    public HttpRequestMessage FirstMessagePassedTo_Send { get; set; }
    public int CountOfCallsTo_Send { get; set; }
    public HttpResponseMessage Send(HttpRequestMessage message)
    {
        CountOfCallsTo_Send++;
        FirstMessagePassedTo_Send ??= message;
        LastMessagePassedTo_Send = message;
        return ReturnFor_Send;
    }
}