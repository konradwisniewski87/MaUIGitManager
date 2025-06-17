using GitHubMauiApp.ViewModels;
using Microsoft.Maui.Controls;

namespace GitHubMauiApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            if (viewModel != null)
            {
                viewModel.LoginSucceeded += async (s, e) =>
                {
                    await Shell.Current.GoToAsync("//RepositoryListPage");
                };
            }
        }
    }
} 