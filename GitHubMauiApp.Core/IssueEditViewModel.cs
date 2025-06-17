using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GitHubMauiApp.Models;
using GitHubMauiApp.Services;
using GitHubMauiApp.Core;

namespace GitHubMauiApp.ViewModels
{
    public class IssueEditViewModel : INotifyPropertyChanged
    {
        private readonly IGitHubApiService _apiService;
        private bool _isBusy;
        private string _error;
        private Issue _issue;
        private Repository _repository;
        private bool _isEditMode;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler IssueSaved;

        public Issue Issue { get => _issue; set { _issue = value; OnPropertyChanged(); } }
        public Repository Repository { get => _repository; set { _repository = value; OnPropertyChanged(); } }
        public bool IsEditMode { get => _isEditMode; set { _isEditMode = value; OnPropertyChanged(); } }
        public bool IsBusy { get => _isBusy; set { _isBusy = value; OnPropertyChanged(); } }
        public string Error { get => _error; set { _error = value; OnPropertyChanged(); } }
        public ICommand SaveCommand { get; }

        public IssueEditViewModel(IGitHubApiService apiService)
        {
            _apiService = apiService;
            SaveCommand = new RelayCommand(async _ => await SaveAsync(), _ => !IsBusy);
        }

        public async Task SaveAsync()
        {
            if (IsBusy || Repository == null || Issue == null) return;
            IsBusy = true;
            Error = string.Empty;
            try
            {
                if (IsEditMode)
                {
                    await _apiService.UpdateIssueAsync(Repository.Owner.Login, Repository.Name, Issue.Number, Issue);
                }
                else
                {
                    await _apiService.CreateIssueAsync(Repository.Owner.Login, Repository.Name, Issue);
                }
                IssueSaved?.Invoke(this, EventArgs.Empty);
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

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 