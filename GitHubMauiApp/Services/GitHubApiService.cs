using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GitHubMauiApp.Helpers;
using GitHubMauiApp.Models;

namespace GitHubMauiApp.Services
{
    public class GitHubApiService : IGitHubApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IGitHubAuthService _authService;

        public GitHubApiService(HttpClient httpClient, IGitHubAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
            _httpClient.BaseAddress = new Uri(Constants.GitHubApiBaseUrl);
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GitHubMauiApp", "1.0"));
        }

        private void AddAuthHeader()
        {
            var token = _authService.GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<User> GetCurrentUserAsync()
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync("user");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Repository>> GetRepositoriesAsync()
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync("user/repos");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Repository>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Issue>> GetIssuesAsync(string owner, string repo)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"repos/{owner}/{repo}/issues");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Issue>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Issue> GetIssueAsync(string owner, string repo, int issueNumber)
        {
            AddAuthHeader();
            var response = await _httpClient.GetAsync($"repos/{owner}/{repo}/issues/{issueNumber}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Issue>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Issue> CreateIssueAsync(string owner, string repo, Issue issue)
        {
            AddAuthHeader();
            var payload = new { title = issue.Title, body = issue.Body };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"repos/{owner}/{repo}/issues", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Issue>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Issue> UpdateIssueAsync(string owner, string repo, int issueNumber, Issue issue)
        {
            AddAuthHeader();
            var payload = new { title = issue.Title, body = issue.Body, state = issue.State };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"repos/{owner}/{repo}/issues/{issueNumber}", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Issue>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task DeleteIssueAsync(string owner, string repo, int issueNumber)
        {
            // GitHub API does not support deleting issues, so we close it instead
            AddAuthHeader();
            var payload = new { state = "closed" };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"repos/{owner}/{repo}/issues/{issueNumber}", content);
            response.EnsureSuccessStatusCode();
        }
    }

    // Extension method for PATCH
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri) { Content = content };
            return await client.SendAsync(request);
        }
    }
} 