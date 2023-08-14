namespace HelloBear.Models
{
    public class ClassDetail
    {
        public ClassDetail()
        {
            ClassCode = string.Empty;
            MainTeacher = string.Empty;
            ClassName = string.Empty;
            SecondaryTeacher = string.Empty;
            Status = string.Empty;
            TextBook = string.Empty;
            StartDate = string.Empty;
            EndDate = string.Empty;
            CommunityLink = string.Empty;
            ClassURL = string.Empty;
        }

        public string ClassCode { get; set; }
        public string MainTeacher { get; set; }
        public string ClassName { get; set; }
        public string SecondaryTeacher { get; set; }
        public string Status { get; set; }
        public string TextBook { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CommunityLink { get; set; }
        public string ClassURL { get; set; }
    }
}
