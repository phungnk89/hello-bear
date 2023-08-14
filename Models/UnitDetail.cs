namespace HelloBear.Models
{
    public class UnitDetail
    {
        public UnitDetail()
        {
            Name = string.Empty;
            LanguageFocus = string.Empty;
            Number = string.Empty;
            Phonics = string.Empty;
            Description = string.Empty;
        }

        public string Name { get; set; }
        public string LanguageFocus { get; set; }
        public string Number { get; set; }
        public string Phonics { get; set; }
        public string Description { get; set; }
    }
}
