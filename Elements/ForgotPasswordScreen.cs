namespace HelloBear.Elements
{
    public class ForgotPasswordScreen
    {
        public static string titleForgotPassword = "//*[text()='Forgot password']";
        public static string txtEmail = "//*[@name='email']";
        public static string btnResetPassword = "//button[@type='submit']";
        public static string messageEmailNotExist = "//*[text()='Please check Email and Password']";
        public static string messageEmailRequired = "//*[text()='Email is required.']";
        public static string messageInvalidEmail = "//*[text()='Incorrect format']";
    }
}
