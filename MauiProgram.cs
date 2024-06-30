using Microsoft.Extensions.Logging;

namespace test_app;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("SpaceGrotesk-Regular.ttf", "SpaceGrotesk");
				fonts.AddFont("SpaceGrotesk-SemiBold.ttf", "SpaceGroteskSemibold");
				fonts.AddFont("SpaceGrotesk-Bold.ttf", "SpaceGroteskBold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
