namespace GitHubMauiApp.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public bool Private { get; set; }
        public User Owner { get; set; }
    }
} 