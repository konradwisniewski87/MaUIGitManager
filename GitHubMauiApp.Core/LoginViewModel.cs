using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GitHubMauiApp.Services;
using GitHubMauiApp.Core;

namespace GitHubMauiApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IGitHubAuthService _authService;
        private bool _isBusy;
        private string _error;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler LoginSucceeded;

        public ICommand LoginCommand { get; }
        public bool IsBusy { get => _isBusy; set { _isBusy = value; OnPropertyChanged(); } }
        public string Error { get => _error; set { _error = value; OnPropertyChanged(); } }

        public LoginViewModel(IGitHubAuthService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(async _ => await LoginAsync(), _ => !IsBusy);
        }

        private async Task LoginAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            Error = string.Empty;
            try
            {
                await _authService.LoginAsync();
                LoginSucceeded?.Invoke(this, EventArgs.Empty);
                // Navigation to next page should be handled by the view or a navigation service
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