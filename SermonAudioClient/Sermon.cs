namespace SermonAudioClient;

public class Sermon
{
    public string Title { get; init; }
    public string Speaker { get; init; }
    public string BibleText { get; init; }
    public DateTime PreachDate { get; init; }
    public string AudioFile { get; init; }
}