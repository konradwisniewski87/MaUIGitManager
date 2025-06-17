using System.Threading.Tasks;

namespace GitHubMauiApp.Services
{
    public interface IGitHubAuthService
    {
        Task<string> LoginAsync(); // Returns access token
        Task LogoutAsync();
        string GetAccessToken();
        bool IsAuthenticated { get; }
    }
} 