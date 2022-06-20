using SermonAudioClient.Utilities.Interfaces;

namespace SermonAudioClient.Utilities;

public class FileWrapper : IFile
{
    public byte[] GetFileData(string filename) => File.ReadAllBytes(filename);
    public string ToBase64(byte[] data) => Convert.ToBase64String(data);
}