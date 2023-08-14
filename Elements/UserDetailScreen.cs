namespace HelloBear.Elements
{
    public class UserDetailScreen
    {
        public static string txtFirstName = "//*[@name='firstName']";
        public static string txtLastName = "//*[@name='lastName']";
        public static string txtEmail = "//*[@name='email']";
        public static string txtPhoneNumber = "//*[text()='Phone number']/following-sibling::*/input";
        public static string dropPhoneType = "//*[@id='phoneType']";
        public static string dropdownRole = "//*[@id='roleId']";
        public static string btnSave = "(//*[text()='Save'])[last()]";
        public static string btnCancel = "//*[text()='Cancel']";
        public static string messageFirstNameValidation = "//*[text()='First name is required.']";
        public static string messageLastNameValidation = "//*[text()='Last name is required.']";
        public static string messageEmailvalidation = "//*[text()='Email is required.']";
        public static string messageRoleValidation = "//*[text()='Role is required.']";
        public static string messageInvalidEmail = "//*[text()='Invalid email format.']";
        public static string messageInvalidPhone = "//*[text()='Invalid phone format.']";
        public static string messageSaved = "//*[text()='Saved' and contains(@class,'MuiAlert')]";
        public static string messageDuplicatedMail = "//*[text()='The email already exists.']";
    }
}
