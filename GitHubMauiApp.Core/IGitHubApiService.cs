using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubMauiApp.Models;

namespace GitHubMauiApp.Services
{
    public interface IGitHubApiService
    {
        Task<User> GetCurrentUserAsync();
        Task<List<Repository>> GetRepositoriesAsync();
        Task<List<Issue>> GetIssuesAsync(string owner, string repo);
        Task<Issue> GetIssueAsync(string owner, string repo, int issueNumber);
        Task<Issue> CreateIssueAsync(string owner, string repo, Issue issue);
        Task<Issue> UpdateIssueAsync(string owner, string repo, int issueNumber, Issue issue);
        Task DeleteIssueAsync(string owner, string repo, int issueNumber);
    }
} 