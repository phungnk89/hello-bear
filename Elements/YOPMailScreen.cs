namespace HelloBear.Elements
{
    public class YOPMailScreen
    {
        public static string txtEmail = "//*[@id='login']";
        public static string btnLogin = "//*[@title='Check Inbox @yopmail.com']";
        public static string btnRefresh = "//*[@id='refresh']";
        public static string btnDelete = "//*[text()='Delete']/..";
        public static string btnMenu = "//*[@class='wmret']/button";
        public static string btnDeleteAll = "//*[@id='delall']";
        public static string mailInvitation = "//button//*[contains(text(), 'Invitation to HELLO YOU!')]";
        public static string mailReset = "//button//*[contains(text(),'Oops! You forgot your password for your HELLO YOU!')]";
        public static string mailSuccessfully = "//button//*[contains(text(),'YOU did it! Your password has been updated successfully.')]";
        public static string messageDelete = "//*[contains(text(),'deleted')]";
        public static string linkInvitation = "(//*[@id='mail']//*[text()='LINK'])[1]/..";
        public static string frameEmail = "//*[@id='ifmail']";
        public static string frameInbox = "//*[@id='ifinbox']";
    }
}
