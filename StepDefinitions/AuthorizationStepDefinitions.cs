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
    public class AuthorizationStepDefinitions
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private WebHelper webHelper;
        private IConfiguration config;

        public AuthorizationStepDefinitions(ScenarioContext context)
        {
            driver = (EdgeDriver)context["driver"];
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            webHelper = new WebHelper() { driver = driver };
        }

        [When(@"I select the User Management")]
        public void WhenISelectTheUserManagement()
        {
            By element = By.XPath(HomeScreen.menuUserManagement);

            driver.FindElement(element).Click();
        }

        [Then(@"the User Management screen should display")]
        public void ThenTheUserManagementScreenShouldDisplay()
        {
            By gridData = By.XPath(UserManagementScreen.gridData);
            By titleUserList = By.XPath(UserManagementScreen.titleUserList);
            By btnAddUser = By.XPath(UserManagementScreen.btnAddUser);
            By txtSearch = By.XPath(UserManagementScreen.txtSearch);

            wait.Until(drv => drv.FindElement(gridData));

            Assert.IsTrue(webHelper.IsElementPresent(titleUserList), "User Management screen does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(btnAddUser), "Button Add does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(txtSearch), "Search field does not show!");
        }

        [When(@"I select the Class Management")]
        public void WhenISelectTheClassManagement()
        {
            By element = By.XPath(HomeScreen.menuClassManagement);

            driver.FindElement(element).Click();
        }

        [Then(@"the Class Management screen should display")]
        public void ThenTheClassManagementScreenShouldDisplay()
        {
            By titleClassList = By.XPath(ClassManagementScreen.titleClassList);
            By txtSearch = By.XPath(ClassManagementScreen.txtSearch);
            By txtFilterStatus = By.XPath(ClassManagementScreen.txtFilterStatus);

            wait.Until(drv => drv.FindElement(titleClassList));

            Assert.IsTrue(webHelper.IsElementPresent(titleClassList), "Class Management screen does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(txtSearch), "Search field does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(txtFilterStatus), "Filter Status field does not show!");
        }

        [When(@"I select the Content Management")]
        public void WhenISelectTheContentManagement()
        {
            By element = By.XPath(HomeScreen.menuContentManagement);

            driver.FindElement(element).Click();
        }

        [Then(@"the Content Management screen should display")]
        public void ThenTheContentManagementScreenShouldDisplay()
        {
            By btnAddBook = By.XPath(ContentManagementScreen.btnAddBook);
            By gridData = By.XPath(ContentManagementScreen.gridData);

            wait.Until(drv => drv.FindElement(gridData));

            Assert.IsTrue(webHelper.IsElementPresent(btnAddBook), "Content management screen does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(gridData), "Content management screen does not show!");
        }

        [When(@"I select the Reports")]
        public void WhenISelectTheReports()
        {
            By element = By.XPath(HomeScreen.menuReports);

            driver.FindElement(element).Click();
        }

        [Then(@"the Reports screen should display")]
        public void ThenTheReportsScreenShouldDisplay()
        {
            //TODO: Come back when Reports complete
            Assert.IsTrue(true);
        }

        [When(@"I login as an Teacher")]
        public void WhenILoginAsAnTeacher()
        {
            var email = config["teacher:mail"];
            var password = config["teacher:password"];

            By txtEmail = By.XPath(LoginScreen.txtEmail);
            By txtPassword = By.XPath(LoginScreen.txtPassword);
            By btnSignIn = By.XPath(LoginScreen.btnSignIn);

            driver.FindElement(txtEmail).SendKeys(email);
            driver.FindElement(txtPassword).SendKeys(password);
            driver.FindElement(btnSignIn).Click();
        }

        [Then(@"the User Management should not display")]
        public void ThenTheUserManagementShouldNotDisplay()
        {
            By element = By.XPath(HomeScreen.menuUserManagement);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);

            wait.Until(drv => drv.FindElements(element).Count == 0);

            Assert.IsFalse(webHelper.IsElementPresent(element), "User Management still shows!");
        }

        [Then(@"the Content Management should not display")]
        public void ThenTheContentManagementShouldNotDisplay()
        {
            By element = By.XPath(HomeScreen.menuContentManagement);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);

            wait.Until(drv => drv.FindElements(element).Count == 0);

            Assert.IsFalse(webHelper.IsElementPresent(element), "User Management still shows!");
        }

        [Then(@"the Reports should not display")]
        public void ThenTheReportsShouldNotDisplay()
        {
            By element = By.XPath(HomeScreen.menuReports);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);

            wait.Until(drv => drv.FindElements(element).Count == 0);

            Assert.IsFalse(webHelper.IsElementPresent(element), "User Management still shows!");
        }

        [Then(@"the Add Class button should not show")]
        public void ThenTheAddClassButtonShouldNotShow()
        {
            By element = By.XPath(ClassManagementScreen.btnAddClass);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            Assert.IsFalse(webHelper.IsElementPresent(element), "Add Class button is showing!");
        }

    }
}
