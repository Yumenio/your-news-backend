namespace News.Api.Configuration
{
    public class AppConfiguration
    {
        public string ApiKey { get; set; }
        public string TopHeadlinesUrl { get; set; }
        public AppConfiguration(string apiKey, string topHeadlinesUrl)
        {
            ApiKey = apiKey;
            TopHeadlinesUrl = topHeadlinesUrl;
        }
    }
}
