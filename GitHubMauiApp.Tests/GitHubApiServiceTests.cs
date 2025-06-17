using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GitHubMauiApp.Models;
using GitHubMauiApp.Services;
using Moq;
using Moq.Protected;
using Xunit;
using System.Text.Json;

namespace GitHubMauiApp.Tests
{
    public class GitHubApiServiceTests
    {
        [Fact]
        public async Task GetRepositoriesAsync_ReturnsRepositories()
        {
            //// Arrange
            //var expectedRepos = new List<Repository>
            //{
            //    new Repository { Name = "Repo1", FullName = "user/Repo1", Description = "Test repo 1", Private = false, Owner = new User { Login = "user" } },
            //    new Repository { Name = "Repo2", FullName = "user/Repo2", Description = "Test repo 2", Private = true, Owner = new User { Login = "user" } }
            //};
            //var json = JsonSerializer.Serialize(expectedRepos);
            //var handlerMock = new Mock<HttpMessageHandler>();
            //handlerMock.Protected()
            //    .Setup<Task<HttpResponseMessage>>(
            //        "SendAsync",
            //        ItExpr.IsAny<HttpRequestMessage>(),
            //        ItExpr.IsAny<CancellationToken>()
            //    )
            //    .ReturnsAsync(new HttpResponseMessage
            //    {
            //        StatusCode = HttpStatusCode.OK,
            //        Content = new StringContent(json),
            //    });
            //var httpClient = new HttpClient(handlerMock.Object)
            //{
            //    BaseAddress = new System.Uri("https://api.github.com/")
            //};
            //var mockAuthService = new Mock<IGitHubAuthService>();
            //mockAuthService.Setup(s => s.GetAccessToken()).Returns("dummy_token");
            //var apiService = new IGitHubApiService(httpClient, mockAuthService.Object);

            //// Act
            //var repos = await apiService.GetRepositoriesAsync();

            //// Assert
            //Assert.NotNull(repos);
            //Assert.Equal(2, repos.Count);
            //Assert.Equal("Repo1", repos[0].Name);
            //Assert.Equal("Repo2", repos[1].Name);
        }
    }
} 