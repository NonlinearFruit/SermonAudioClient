using CliWrap;
using CliWrap.Buffered;
using SermonAudioClient.Interfaces;

namespace SermonAudioClient;

public class MediaManipulator : IMediaManipulator
{
    public async Task ClipMediaToLength(string inputFilePath, string outputFilePath, TimeSpan start, TimeSpan duration)
    {
        await Cli.Wrap("ffmpeg")
            .WithArguments(new[] {"-ss", start.ToString("g")})
            .WithArguments(new[] {"-i", inputFilePath})
            .WithArguments(new[] {"-t", duration.ToString("g")})
            .WithArguments(new[] {"-codec", "copy"})
            .WithArguments(new[] {outputFilePath})
            .ExecuteBufferedAsync();
    }

    public async Task ConvertMediaToMp3(string inputFilePath, string outputFilePath)
    {
        await Cli.Wrap("ffmpeg")
            .WithArguments(new[] {"-i", inputFilePath})
            .WithArguments(new[] {"-vn", outputFilePath})
            .ExecuteBufferedAsync();
    }
}