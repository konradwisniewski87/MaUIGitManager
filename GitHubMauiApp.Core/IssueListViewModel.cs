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
    public class IssueListViewModel : INotifyPropertyChanged
    {
        private readonly IGitHubApiService _apiService;
        private bool _isBusy;
        private string _error;
        private ObservableCollection<Issue> _issues;
        private Repository _repository;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Issue> Issues { get => _issues; set { _issues = value; OnPropertyChanged(); } }
        public bool IsBusy { get => _isBusy; set { _isBusy = value; OnPropertyChanged(); } }
        public string Error { get => _error; set { _error = value; OnPropertyChanged(); } }
        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public Repository Repository { get => _repository; set { _repository = value; OnPropertyChanged(); } }
        public event EventHandler<Issue> IssueEditRequested;
        public event EventHandler AddIssueRequested;

        public IssueListViewModel(IGitHubApiService apiService)
        {
            _apiService = apiService;
            Issues = new ObservableCollection<Issue>();
            RefreshCommand = new RelayCommand(async _ => await LoadIssuesAsync(), _ => !IsBusy);
            DeleteCommand = new RelayCommand<Issue>(async issue => await DeleteIssueAsync(issue), issue => !IsBusy);
            EditCommand = new RelayCommand<Issue>(issue => OnEditIssue(issue));
        }

        public async Task LoadIssuesAsync()
        {
            if (IsBusy || Repository == null) return;
            IsBusy = true;
            Error = string.Empty;
            try
            {
                var issues = await _apiService.GetIssuesAsync(Repository.Owner.Login, Repository.Name);
                Issues = new ObservableCollection<Issue>(issues);
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

        public async Task DeleteIssueAsync(Issue issue)
        {
            if (IsBusy || issue == null) return;
            IsBusy = true;
            Error = string.Empty;
            try
            {
                await _apiService.DeleteIssueAsync(Repository.Owner.Login, Repository.Name, issue.Number);
                await LoadIssuesAsync();
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

        private void OnEditIssue(Issue issue)
        {
            if (issue != null)
                IssueEditRequested?.Invoke(this, issue);
        }

        public void RequestAddIssue()
        {
            AddIssueRequested?.Invoke(this, EventArgs.Empty);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 