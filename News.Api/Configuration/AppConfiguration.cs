namespace News.Api.Configuration
{
    public class AppConfiguration
    {
        public string ApiKey { get; set; }
        public string TopHeadlinesUrl { get; set; }
        public string AuthSecret { get; init; }
        public AppConfiguration(string apiKey, string topHeadlinesUrl, string authSecret)
        {
            ApiKey = apiKey;
            TopHeadlinesUrl = topHeadlinesUrl;
            AuthSecret = authSecret;
        }
    }
}
