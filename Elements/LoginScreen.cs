namespace HelloBear.Elements
{
    public class LoginScreen
    {
        public static string txtEmail = "//*[@name='email']";
        public static string txtPassword = "//*[@name='password']";
        public static string chkRememberMe = "//*[@value='remember']";
        public static string btnSignIn = "//button[@type='submit']";
        public static string linkForgotPassword = "//button[text()='Forgot Password?']";
        public static string messageEmailNotExist = "//*[text()='Email does not exists.']";
        public static string messsageUnauthorized = "//*[text()='Please check Email and Password']";
        public static string messageEmailRequired = "//*[text()='Email is required.']";
        public static string messagePasswordRequired = "//*[text()='Password is required.']";
    }
}
