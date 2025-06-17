namespace GitHubMauiApp.Helpers
{
    public static class Constants
    {
        // TODO: Fill these with your GitHub OAuth app credentials
        public const string GitHubClientId = "YOUR_CLIENT_ID"; // <-- Set your GitHub OAuth Client ID here
        public const string GitHubClientSecret = "YOUR_CLIENT_SECRET"; // <-- Set your GitHub OAuth Client Secret here
        public const string GitHubRedirectUri = "myapp://auth"; // <-- Set your registered redirect URI here
        public const string GitHubOAuthAuthorizeUrl = "https://github.com/login/oauth/authorize";
        public const string GitHubOAuthTokenUrl = "https://github.com/login/oauth/access_token";
        public const string GitHubApiBaseUrl = "https://api.github.com/";
    }
} 