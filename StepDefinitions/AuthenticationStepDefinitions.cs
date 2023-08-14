using HelloBear.Elements;
using HelloBear.Support;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace HelloBear.StepDefinitions
{
    [Binding]
    public class AuthenticationStepDefinitions
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private WebHelper webHelper;
        private IConfiguration config;

        public AuthenticationStepDefinitions(ScenarioContext context)
        {
            driver = (EdgeDriver)context["driver"];
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            webHelper = new WebHelper() { driver = driver };
        }

        [Given(@"I have accessed to the Admin Application")]
        public void GivenIHaveAccessedToTheAdminApplication()
        {
            var url = config["url"];
            driver.Navigate().GoToUrl(url);
        }

        [Then(@"the Login screen should display")]
        public void ThenTheLoginScreenShouldDisplay()
        {
            By txtEmail = By.XPath(LoginScreen.txtEmail);
            By txtPassword = By.XPath(LoginScreen.txtPassword);
            By btnSignIn = By.XPath(LoginScreen.btnSignIn);
            By linkForgotPassword = By.XPath(LoginScreen.linkForgotPassword);

            wait.Until(drv => drv.FindElement(txtEmail));

            Assert.IsTrue(webHelper.IsElementPresent(txtEmail), "Email field does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(txtPassword), "Password field does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(btnSignIn), "Button login does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(linkForgotPassword), "Link forgot password does not show!");
        }

        [When(@"I login as an Admin")]
        public void WhenILoginAsAnAdmin()
        {
            var email = config["admin:mail"];
            var password = config["admin:password"];

            By txtEmail = By.XPath(LoginScreen.txtEmail);
            By txtPassword = By.XPath(LoginScreen.txtPassword);
            By btnSignIn = By.XPath(LoginScreen.btnSignIn);

            driver.FindElement(txtEmail).SendKeys(email);
            driver.FindElement(txtPassword).SendKeys(password);
            driver.FindElement(btnSignIn).Click();
        }

        [Then(@"the Home screen should display")]
        public void ThenTheHomeScreenShouldDisplay()
        {
            By menuClassManagement = By.XPath(HomeScreen.menuClassManagement);

            wait.Until(drv => drv.FindElement(menuClassManagement));

            Assert.IsTrue(webHelper.IsElementPresent(menuClassManagement), "Class management does not show!");
        }

        [When(@"I input '([^']*)' into Email field")]
        public void WhenIInputIntoEmailField(string input)
        {
            By element = By.XPath(LoginScreen.txtEmail);

            webHelper.ClearElementText(element);

            driver.FindElement(element).SendKeys(input);
        }

        [When(@"I input '([^']*)' into Password field")]
        public void WhenIInputIntoPasswordField(string input)
        {
            By element = By.XPath(LoginScreen.txtPassword);

            webHelper.ClearElementText(element);

            driver.FindElement(element).SendKeys(input);
        }

        [When(@"I click SignIn button")]
        public void WhenIClickSignInButton()
        {
            By element = By.XPath(LoginScreen.btnSignIn);

            driver.FindElement(element).Click();
        }

        [Then(@"the Unauthorized message should display")]
        public void ThenTheUnauthorizedMessageShouldDisplay()
        {
            By element = By.XPath(LoginScreen.messsageUnauthorized);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the validation message for Email field is required should display")]
        public void ThenTheValidationMessageForEmailFieldIsRequiredShouldDisplay()
        {
            By element = By.XPath(LoginScreen.messageEmailRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the validation message for Password field is required should display")]
        public void ThenTheValidationMessageForPasswordFieldIsRequiredShouldDisplay()
        {
            By element = By.XPath(LoginScreen.messagePasswordRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [When(@"I logout the Admin Application")]
        public void WhenILogoutTheAdminApplication()
        {
            By iconAccount = By.XPath(HomeScreen.iconAccount);
            By menuLogout = By.XPath(HomeScreen.menuLogout);

            driver.FindElement(iconAccount).Click();
            driver.FindElement(menuLogout).Click();
        }

    }
}
