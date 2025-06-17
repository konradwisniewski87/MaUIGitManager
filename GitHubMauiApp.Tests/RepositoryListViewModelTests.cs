using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubMauiApp.Models;
using GitHubMauiApp.Services;
using GitHubMauiApp.ViewModels;
using Moq;
using Xunit;

namespace GitHubMauiApp.Tests
{
    public class RepositoryListViewModelTests
    {
        [Fact]
        public async Task LoadRepositoriesAsync_PopulatesRepositories()
        {
            // Arrange
            var mockService = new Mock<IGitHubApiService>();
            var expectedRepos = new List<Repository>
            {
                new Repository { Name = "Repo1" },
                new Repository { Name = "Repo2" }
            };
            mockService.Setup(s => s.GetRepositoriesAsync())
                       .ReturnsAsync(expectedRepos);

            var vm = new RepositoryListViewModel(mockService.Object);

            // Act
            await vm.LoadRepositoriesAsync();

            // Assert
            Assert.Equal(2, vm.Repositories.Count);
            Assert.Equal("Repo1", vm.Repositories[0].Name);
            Assert.Equal("Repo2", vm.Repositories[1].Name);
        }
    }
} 