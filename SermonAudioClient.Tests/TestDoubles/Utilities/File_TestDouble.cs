using System.Diagnostics.CodeAnalysis;
using SermonAudioClient.Utilities.Interfaces;

namespace SermonAudioClient.Tests.TestDoubles.Utilities;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class File_TestDouble : IFile
{
    public byte[] ReturnFor_GetFileData { get; set; }
    public string LastFilePassedTo_GetFileData { get; set; }
    public int CountOfCallsTo_GetFileData { get; set; }
    public byte[] GetFileData(string filename)
    {
        CountOfCallsTo_GetFileData++;
        LastFilePassedTo_GetFileData = filename;
        return ReturnFor_GetFileData;
    }

    public string ReturnFor_ToBase64 { get; set; }
    public byte[] LastDataPassedTo_ToBase64 { get; set; }
    public int CountOfCallsTo_ToBase64 { get; set; }
    public string ToBase64(byte[] data)
    {
        CountOfCallsTo_ToBase64++;
        LastDataPassedTo_ToBase64 = data;
        return ReturnFor_ToBase64;
    }
}