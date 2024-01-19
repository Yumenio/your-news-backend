namespace News.Api.Models
{
    public class New
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Abstract { get; private set; }
        public string Url { get; private set; }
        public DateTime PublishedDate { get; private set; }
        public string Content { get; private set; }
        public MultimediaSchema[] Media { get; private set; }

        public New(string? title, string @abstract, string url, DateTime publishedDate, string content, MultimediaSchema[] media)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(title, nameof(title));
            }
            Id = new Guid();
            Title = title;
            Abstract = @abstract;
            Url = url;
            PublishedDate = publishedDate;
            Content = content;
            Media = media;
        }
    }
}
