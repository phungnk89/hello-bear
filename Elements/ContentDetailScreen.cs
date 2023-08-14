namespace HelloBear.Elements
{
    public class ContentDetailScreen
    {
        public static string txtName = "//*[@name='name']";
        public static string txtShortName = "//*[@name='shortName']";
        public static string txtDescription = "//*[@id='description']";
        public static string txtThumbnail = "//*[@name='thumbnailFile']";
        public static string txtSearch = "//*[@placeholder='Search']";
        public static string messageNameRequired = "//*[text()='Name is required.']";
        public static string messageShortNameRequired = "//*[text()='Short Code is Required.']";
        public static string messageFileSize = "//*[text()='File size is not valid!']";
        public static string messageSaved = "//*[text()='Saved']";
        public static string messageShortNameDuplicated = "//*[text()='Short Code text already exists.']";
        public static string btnSave = "//*[text()='Save']";
        public static string btnCancel = "//*[text()='Cancel']";
        public static string btnAddUnit = "//*[text()='New Unit']";
        public static string imgThumbnail = "//img";
        public static string gridData = "//table//tbody//tr";
    }
}
