using GitHubMauiApp.ViewModels;
using Microsoft.Maui.Controls;

namespace GitHubMauiApp.Views
{
    public partial class IssueEditPage : ContentPage
    {
        public IssueEditPage(IssueEditViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            if (viewModel != null)
            {
                viewModel.IssueSaved += async (s, e) =>
                {
                    await Shell.Current.GoToAsync("..", true);
                };
            }
        }
    }
} 