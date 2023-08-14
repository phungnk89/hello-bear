using HelloBear.Elements;
using HelloBear.Support;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace HelloBear.StepDefinitions
{
    [Binding]
    public class ForgotPasswordStepDefinitions
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private WebHelper webHelper;

        public ForgotPasswordStepDefinitions(ScenarioContext context)
        {
            driver = (EdgeDriver)context["driver"];
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            webHelper = new WebHelper() { driver = driver };
        }

        [When(@"I click the Forgot Password link")]
        public void WhenIClickTheForgotPasswordLink()
        {
            By element = By.XPath(LoginScreen.linkForgotPassword);

            driver.FindElement(element).Click();
        }

        [Then(@"the Forgot Password screen should display")]
        public void ThenTheForgotPasswordScreenShouldDisplay()
        {
            By element = By.XPath(ForgotPasswordScreen.titleForgotPassword);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Forgot password screen does not show!");
        }

        [When(@"I click Reset Password button")]
        public void WhenIClickResetPasswordButton()
        {
            By element = By.XPath(ForgotPasswordScreen.btnResetPassword);

            driver.FindElement(element).Click();
        }

        [Then(@"the validation message for invalid email should display")]
        public void ThenTheValidationMessageForInvalidEmailShouldDisplay()
        {
            By element = By.XPath(ForgotPasswordScreen.messageInvalidEmail);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the validation message for not exist email should display")]
        public void ThenTheValidationMessageForNotExistEmailShouldDisplay()
        {
            By element = By.XPath(ForgotPasswordScreen.messageEmailNotExist);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the Request Sent screen should display")]
        public void ThenTheRequestSentScreenShouldDisplay()
        {
            By element = By.XPath(ResetPasswordScreen.titleRequestSent);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Request Sent screen does not show!");
        }

    }
}
