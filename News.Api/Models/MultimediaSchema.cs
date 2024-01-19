namespace News.Api.Models
{
    public class MultimediaSchema
    {
        public string url { get; private set; }
        public string format { get; private set; }
        public int height { get; private set; }
        public int width { get; private set; }
        public string type { get; private set; }
        public string subtype { get; private set; }
        public string caption { get; private set; }
        public string copyright { get; private set; }

        public MultimediaSchema(string url, string format, int height, int width, string type, string subtype, string caption, string copyright)
        {
            this.url= url;
            this.format= format;
            this.height= height;
            this.width= width;
            this.type= type;
            this.subtype= subtype;
            this.caption= caption;
            this.copyright= copyright;
        }
    }
}
