namespace HelloBear.Elements
{
    public class UserManagementScreen
    {
        public static string gridData = "//table";
        public static string titleUserList = "//*[text()='User List']";
        public static string txtSearch = "//*[@placeholder='Search']";
        public static string btnAddUser = "//*[text()='New User']";
        public static string btnYes = "//*[@role='dialog']//*[text()='Yes']";
        public static string btnNo = "//*[@role='dialog']//*[text()='No']";
        public static string optionResendInvitation = "//li[text()='Resend invitation']";
        public static string optionResetPassword = "//li[text()='Reset password']";
        public static string optionDelete = "//li[text()='Delete']";
        public static string messageSent = "//*[text()='Sent']";
        public static string loadingBar = "//*[@role='progressbar']";
    }
}
