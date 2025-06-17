using System.Threading.Tasks;
using GitHubMauiApp.ViewModels;
using GitHubMauiApp.Services;
using Moq;
using Xunit;

namespace GitHubMauiApp.Tests
{
    public class LoginViewModelTests
    {
        [Fact]
        public async Task LoginCommand_SuccessfulLogin_RaisesLoginSucceeded()
        {
            // Arrange
            var mockAuthService = new Mock<IGitHubAuthService>();
            mockAuthService.Setup(s => s.LoginAsync()).Returns(Task.FromResult("token"));
            var vm = new LoginViewModel(mockAuthService.Object);
            bool eventRaised = false;
            vm.LoginSucceeded += (s, e) => eventRaised = true;

            // Act
            await Task.Run(() => vm.LoginCommand.Execute(null));
            await Task.Delay(100); // Allow async to complete

            // Assert
            Assert.True(eventRaised);
            Assert.True(vm.IsBusy == false);
            Assert.True(string.IsNullOrEmpty(vm.Error));
        }

        [Fact]
        public async Task LoginCommand_FailedLogin_SetsError()
        {
            // Arrange
            var mockAuthService = new Mock<IGitHubAuthService>();
            mockAuthService.Setup(s => s.LoginAsync()).ThrowsAsync(new System.Exception("Login failed"));
            var vm = new LoginViewModel(mockAuthService.Object);

            // Act
            await Task.Run(() => vm.LoginCommand.Execute(null));
            await Task.Delay(100); // Allow async to complete

            // Assert
            Assert.False(string.IsNullOrEmpty(vm.Error));
            Assert.Contains("Login failed", vm.Error);
            Assert.True(vm.IsBusy == false);
        }
    }
} 