using Microsoft.Extensions.DependencyInjection;
using SermonAudioClient;
using SermonAudioClient.Interfaces;
using SermonAudioClient.Utilities;
using SermonAudioClient.Utilities.Interfaces;

var apiKey = args[0];

var services = new ServiceCollection();
services.AddSingleton(apiKey);
services.AddSingleton<IHttpClient, HttpClientWrapper>();
services.AddSingleton<ISermonAudio, SermonAudio>();
services.AddTransient<IFile, FileWrapper>();
services.AddTransient<IMediaManipulator, MediaManipulator>();
services.AddTransient<InputCollector>();

var provider = services.BuildServiceProvider();
provider.GetRequiredService<InputCollector>().Run();
