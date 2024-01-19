namespace News.Api.Models
{
    public class NewsDto
    {
        public string status { get; set; }
        public string copyright { get; set; }
        public string section { get; set; }
        public string last_updated { get; set; }
        public int num_results { get; set; }
        public ArticleSchema[] results { get; set; }

        public NewsDto(string status, string copyright, string section, string last_updated, int num_results, ArticleSchema[] results)
        {
            this.status = status;
            this.copyright = copyright;
            this.section = section;
            this.last_updated = last_updated;
            this.num_results = num_results;
            this.results = results;
        }
    }
}