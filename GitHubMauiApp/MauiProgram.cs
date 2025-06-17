using Microsoft.Extensions.Logging;

namespace GitHubMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Register services
		builder.Services.AddSingleton<Services.IGitHubAuthService, Services.GitHubAuthService>();
		builder.Services.AddHttpClient<Services.IGitHubApiService, Services.GitHubApiService>();
		// Register ViewModels
		builder.Services.AddTransient<ViewModels.LoginViewModel>();
		builder.Services.AddTransient<ViewModels.RepositoryListViewModel>();
		builder.Services.AddTransient<ViewModels.IssueListViewModel>();
		builder.Services.AddTransient<ViewModels.IssueEditViewModel>();
		// Register Views
		builder.Services.AddTransient<Views.LoginPage>();
		builder.Services.AddTransient<Views.RepositoryListPage>();
		builder.Services.AddTransient<Views.IssueListPage>();
		builder.Services.AddTransient<Views.IssueEditPage>();

		return builder.Build();
	}
}
