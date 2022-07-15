using SermonAudioClient.Interfaces;

namespace SermonAudioClient;

public class InputCollector
{
   private readonly ISermonAudio _sermonAudio;

   public InputCollector(ISermonAudio sermonAudio)
   {
      _sermonAudio = sermonAudio;
   }

   public void Run()
   {
      var sermon = new Sermon
      {
         Title = GetInput("Sermon Title"),
         Speaker = GetInput("Sermon Speaker (Philip B. Strong)", "Philip B. Strong"),
         BibleText = GetInput("Biblical Text (Comma Separated)"),
         PreachDate = GetDate($"Preached Date ({DateTime.Now:yyyy-MM-dd})"),
         AudioFile = GetInput("Audio file (clipped 64kbps mp3)")
      };
      var url = _sermonAudio.CreateSermon(sermon);
      Console.WriteLine($"Checkout the sermon here: {url}");
   }

   private string GetInput(string prompt, string defaultValue = "")
   {
      Console.Write(prompt+": ");
      var textReceived = Console.ReadLine();
      return string.IsNullOrWhiteSpace(textReceived)
         ? defaultValue
         : textReceived;
   }

   private DateTime GetDate(string prompt)
   {
      Console.Write(prompt+": ");
      return DateTime.TryParse(Console.ReadLine(), out var date)
         ? date
         : DateTime.Now;
   }
}