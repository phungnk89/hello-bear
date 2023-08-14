namespace HelloBear.Elements
{
    public class HomeScreen
    {
        public static string menuUserManagement = "//*[@href='/users']";
        public static string menuClassManagement = "//*[@href='/classes']";
        public static string menuContentManagement = "//*[@href='/textbooks']";
        public static string menuReports = "//*[@href='/reports']";
        public static string menuLogout = "//*[text()='Log out']";
        public static string iconAccount = "//*[@aria-controls='menu-appbar']";
        public static string lblCurrentUser = "(//*[@aria-label='account of current user']/preceding-sibling::*)[last()]";
    }
}
