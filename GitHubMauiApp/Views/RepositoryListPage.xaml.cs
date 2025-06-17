using GitHubMauiApp.ViewModels;
using Microsoft.Maui.Controls;

namespace GitHubMauiApp.Views
{
    public partial class RepositoryListPage : ContentPage
    {
        private void OnRepositorySelected(object sender, SelectionChangedEventArgs e)
        {
            if (BindingContext is RepositoryListViewModel vm && e.CurrentSelection.Count > 0)
            {
                var repo = e.CurrentSelection[0] as Models.Repository;
                vm.RepositorySelectedCommand.Execute(repo);
            }
        }

        public RepositoryListPage(RepositoryListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.RepositorySelected += async (s, repo) =>
            {
                if (repo != null)
                {
                    var navParam = new ShellNavigationQueryParameters
                    {
                        { "owner", repo.Owner.Login },
                        { "repo", repo.Name }
                    };
                    await Shell.Current.GoToAsync("//IssueListPage", navParam);
                }
            };
        }
    }
} 