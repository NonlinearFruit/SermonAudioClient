namespace SermonAudioClient.Utilities.Interfaces;

public interface IFile
{
    byte[] GetFileData(string filename);
    string ToBase64(byte[] data);
}