namespace HelloBear.Elements
{
    public class UnitDetailScreen
    {
        public static string txtName = "//*[@name='name']";
        public static string txtLanguageFocus = "//*[@name='languageFocus']";
        public static string txtNumber = "//*[@name='number']";
        public static string txtPhonics = "//*[@name='phonics']";
        public static string txtDescription = "(//*[@name='description'])[last()]";
        public static string txtSearch = "//*[@placeholder='Search']";
        public static string txtBookName = "//*[@name='textBookName']";
        public static string txtUnitName = "//*[@name='lessonName']";
        public static string txtContentName = "//*[@name='name']";
        public static string txtType = "//*[@name='type']";
        public static string txtPageNumber = "//*[@name='pageNumber']";
        public static string txtYouTubeLink = "//*[@name='youtubeLink']";
        public static string txtWordWallLink = "//*[@name='wordwallNetLink']";
        public static string messageContentNameRequired = "//*[text()='Content Name is required.']";
        public static string messageNameRequired = "//*[text()='Name is required.']";
        public static string messageNumberRequired = "//*[text()='Number is required.']";
        public static string messageLanguageFocusRequired = "//*[text()='Language Focus is required.']";
        public static string messagePhonicsRequired = "//*[text()='Phonics is required.']";
        public static string messageYouTubeLinkRequired = "//*[text()='Youtube Link is required.']";
        public static string messagePageNumberRequired = "//*[text()='Page number must be greater than or equal to 1.']";
        public static string messagePageImageRequired = "//*[text()='Page Image is required.']";
        public static string messsageSaved = "//*[text()='Saved']";
        public static string messageDownloadSuccessfully = "//*[contains(text(),'Download succes')]";
        public static string btnSave = "//*[text()='Save']";
        public static string btnCancel = "//*[text()='Cancel']";
        public static string btnAdd = "//*[text()='Add']";
        public static string btnExportQRCode = "//*[text()='Export Qr Code']";
        public static string btnUploadPageImage = "//*[@id='pageImageFile']";
        public static string gridData = "//table//tbody//tr";
        public static string imgPageThumbnail = "//img";

        public class PushAndListenSetupScreen
        {
            public static string txtName = "//*[@id='rectangle-form']//*[@name='name']";
            public static string txtAudio = "//*[@id='rectangle-form']//*[@name='audioFileUrl']";
            public static string titlePushAndListen = "//*[text()='Push & Listen Setup']";
            public static string imgPushAndListen = "//*[@id='draw-image']";
            public static string gridConfiguration = "//table//tbody//tr";
            public static string btnSave = "(//*[text()='Save'])[last()]";
            public static string divBoundingBox = "//*[@id='draw-image']/following-sibling::div";
            public static string optionEdit = "//li[text()='Edit']";
            public static string optionDelete = "//li[text()='Delete']";
        }
    }
}
