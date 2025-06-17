using GitHubMauiApp.ViewModels;
using Microsoft.Maui.Controls;

namespace GitHubMauiApp.Views
{
    public partial class IssueListPage : ContentPage
    {
        public IssueListPage(IssueListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            AddIssueButton.Clicked += (s, e) => viewModel.RequestAddIssue();
            viewModel.IssueEditRequested += async (s, issue) =>
            {
                if (issue != null)
                {
                    var navParam = new ShellNavigationQueryParameters
                    {
                        { "edit", "true" },
                        { "number", issue.Number.ToString() },
                        { "owner", viewModel.Repository.Owner.Login },
                        { "repo", viewModel.Repository.Name }
                    };
                    await Shell.Current.GoToAsync("//IssueEditPage", navParam);
                }
            };
            viewModel.AddIssueRequested += async (s, e) =>
            {
                var navParam = new ShellNavigationQueryParameters
                {
                    { "edit", "false" },
                    { "owner", viewModel.Repository.Owner.Login },
                    { "repo", viewModel.Repository.Name }
                };
                await Shell.Current.GoToAsync("//IssueEditPage", navParam);
            };
        }
    }
} 