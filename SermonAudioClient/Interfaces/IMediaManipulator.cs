namespace SermonAudioClient.Interfaces;

public interface IMediaManipulator
{
   Task ClipMediaToLength(string inputFilePath, string outputFilePath, TimeSpan start, TimeSpan duration);
   Task ConvertMediaToMp3(string inputFilePath, string outputFilePath);
}