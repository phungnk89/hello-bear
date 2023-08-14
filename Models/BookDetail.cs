namespace HelloBear.Models
{
    public class BookDetail
    {
        public BookDetail()
        {
            Name = string.Empty;
            ShortName = string.Empty;
            Description = string.Empty;
        }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
