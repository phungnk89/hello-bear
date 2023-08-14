namespace HelloBear.Models
{
    public class ContentDetail
    {
        public string Name { get; set; }
        public ContentType Type { get; set; }
        public string Link { get; set; }
        public string PageNumber { get; set; }
        public string Description { get; set; }

        public ContentDetail()
        {
            Name = string.Empty;
            Type = ContentType.Read;
            Link = string.Empty;
            PageNumber = string.Empty;
            Description = string.Empty;
        }
    }
}
