using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GitHubMauiApp.Models;
using GitHubMauiApp.Services;
using GitHubMauiApp.Core;

namespace GitHubMauiApp.ViewModels
{
    public class RepositoryListViewModel : INotifyPropertyChanged
    {
        private readonly IGitHubApiService _apiService;
        private bool _isBusy;
        private string _error;
        private ObservableCollection<Repository> _repositories;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Repository> Repositories { get => _repositories; set { _repositories = value; OnPropertyChanged(); } }
        public bool IsBusy { get => _isBusy; set { _isBusy = value; OnPropertyChanged(); } }
        public string Error { get => _error; set { _error = value; OnPropertyChanged(); } }
        public ICommand RefreshCommand { get; }
        public ICommand RepositorySelectedCommand { get; }
        public event EventHandler<Models.Repository> RepositorySelected;

        public RepositoryListViewModel(IGitHubApiService apiService)
        {
            _apiService = apiService;
            Repositories = new ObservableCollection<Repository>();
            RefreshCommand = new RelayCommand(async _ => await LoadRepositoriesAsync(), _ => !IsBusy);
            RepositorySelectedCommand = new RelayCommand<Repository>(repo => OnRepositorySelected(repo));
        }

        public async Task LoadRepositoriesAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            Error = string.Empty;
            try
            {
                var repos = await _apiService.GetRepositoriesAsync();
                Repositories = new ObservableCollection<Repository>(repos);
            }
            catch (System.Exception ex)
            {
                Error = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnRepositorySelected(Repository repo)
        {
            if (repo != null)
                RepositorySelected?.Invoke(this, repo);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 