using System;

namespace GitHubMauiApp.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string State { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 