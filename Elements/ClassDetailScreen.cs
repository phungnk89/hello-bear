namespace HelloBear.Elements
{
    public class ClassDetailScreen
    {
        public static string txtClassCode = "//*[@name='classCode']";
        public static string txtMainTeacher = "//*[@name='mainTeacher']";
        public static string txtStatus = "//*[@name='status']/..";
        public static string txtClassName = "//*[@name='className']";
        public static string txtTextBook = "//*[@name='textBookId']";
        public static string txtSecondaryTeacher = "//*[@name='secondaryTeacherIds']";
        public static string txtStartDate = "(//*[@placeholder='MMMM YYYY'])[1]";
        public static string txtEndDate = "(//*[@placeholder='MMMM YYYY'])[2]";
        public static string txtCommunityLink = "//*[text()='Community Link']";
        public static string txtURLLink = "//*[text()='URL Link']";
        public static string iconStartDate = "(//*[@data-testid='CalendarIcon']/..)[1]";
        public static string iconEndDate = "(//*[@data-testid='CalendarIcon']/..)[2]";
        public static string btnSave = "//*[text()='Save']";
        public static string btnCancel = "//*[text()='Cancel']";
        public static string imgQRCode = "//*[@alt='QRCode']";
        public static string messageClassNameRequired = "//*[text()='Class name is required']";
        public static string messageTextbookRequired = "//*[text()='Textbook is required']";
        public static string popupCalendar = "//*[contains(@class,'MuiDateCalendar-root')]";
    }
}
