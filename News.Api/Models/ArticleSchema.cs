namespace News.Api.Models
{
    public class ArticleSchema
    {
        public string section { get; private set; }
        public string subsection { get; private set;}
        public string title { get; private set; }
        public string @abstract { get; private set; }
        public string url { get; private set; }
        public string byline { get; private set; }
        public string item_type { get; private set; }
        public string published_date { get; private set; }
        public MultimediaSchema[] multimedia { get; private set; }
        public string short_url { get; private set; }
        public ArticleSchema(string section, string subsection, string title, string @abstract, string url, string byline, string item_type, string published_date, MultimediaSchema[] multimedia, string short_url)
        {
            this.section = section;
            this.subsection = subsection;
            this.title = title;
            this.@abstract = @abstract;
            this.url = url;
            this.byline = byline;
            this.item_type = item_type;
            this.published_date = published_date;
            this.multimedia = multimedia;
            this.short_url = short_url;
        }
    }
}
