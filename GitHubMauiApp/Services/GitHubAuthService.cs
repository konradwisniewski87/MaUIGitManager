using System;
using System.Threading.Tasks;
using GitHubMauiApp.Helpers;
using Microsoft.Maui.Authentication;
using Microsoft.Maui.Storage;

namespace GitHubMauiApp.Services
{
    public class GitHubAuthService : IGitHubAuthService
    {
        private const string AccessTokenKey = "github_access_token";
        public bool IsAuthenticated => !string.IsNullOrEmpty(GetAccessToken());

        public async Task<string> LoginAsync()
        {
            var authUrl = $"{Constants.GitHubOAuthAuthorizeUrl}?client_id={Constants.GitHubClientId}&scope=repo%20user&redirect_uri={Constants.GitHubRedirectUri}";
            var callbackUrl = new Uri(Constants.GitHubRedirectUri);

            WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(new Uri(authUrl), callbackUrl);
            if (result.Properties.TryGetValue("code", out var code))
            {
                // Exchange code for access token
                var token = await ExchangeCodeForTokenAsync(code);
                if (!string.IsNullOrEmpty(token))
                {
                    await SecureStorage.Default.SetAsync(AccessTokenKey, token);
                    return token;
                }
            }
            throw new Exception("Authentication failed");
        }

        public async Task LogoutAsync()
        {
            SecureStorage.Default.Remove(AccessTokenKey);
            await Task.CompletedTask;
        }

        public string GetAccessToken()
        {
            return SecureStorage.Default.GetAsync(AccessTokenKey).GetAwaiter().GetResult();
        }

        private async Task<string> ExchangeCodeForTokenAsync(string code)
        {
            using var client = new System.Net.Http.HttpClient();
            var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, Constants.GitHubOAuthTokenUrl);
            request.Headers.Add("Accept", "application/json");
            var content = new System.Net.Http.FormUrlEncodedContent(new[]
            {
                new System.Collections.Generic.KeyValuePair<string, string>("client_id", Constants.GitHubClientId),
                new System.Collections.Generic.KeyValuePair<string, string>("client_secret", Constants.GitHubClientSecret),
                new System.Collections.Generic.KeyValuePair<string, string>("code", code),
                new System.Collections.Generic.KeyValuePair<string, string>("redirect_uri", Constants.GitHubRedirectUri),
            });
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var obj = System.Text.Json.JsonDocument.Parse(json);
                if (obj.RootElement.TryGetProperty("access_token", out var tokenElement))
                {
                    return tokenElement.GetString();
                }
            }
            return null;
        }
    }
} 