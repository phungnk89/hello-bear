using HelloBear.Elements;
using HelloBear.Models;
using HelloBear.Support;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HelloBear.StepDefinitions
{
    [Binding]
    public class UserManagementStepDefinitions
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private WebHelper webHelper;
        private static string userEmail = string.Empty;
        private static string userPassword = string.Empty;
        private static string urlInvitation = string.Empty;

        public UserManagementStepDefinitions(ScenarioContext context)
        {
            driver = (EdgeDriver)context["driver"];
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            webHelper = new WebHelper() { driver = driver };
        }

        [When(@"I select Add User button")]
        public void WhenISelectAddUserButton()
        {
            By element = By.XPath(UserManagementScreen.btnAddUser);

            wait.Until(drv => drv.FindElement(element).GetAttribute("disabled") == null);

            webHelper.ClickByJavascript(element);
        }

        [Then(@"the Add User screen should display")]
        public void ThenTheAddUserScreenShouldDisplay()
        {
            By txtFirstName = By.XPath(UserDetailScreen.txtFirstName);
            By txtLastName = By.XPath(UserDetailScreen.txtLastName);
            By txtEmail = By.XPath(UserDetailScreen.txtEmail);
            By dropdownRole = By.XPath(UserDetailScreen.dropdownRole);
            By txtPhoneNumber = By.XPath(UserDetailScreen.txtPhoneNumber);

            wait.Until(drv => drv.FindElement(txtFirstName));

            Assert.IsTrue(webHelper.IsElementPresent(txtFirstName), "First name field does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(txtLastName), "Last name field does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(txtEmail), "Email field does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(dropdownRole), "Role field does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(txtPhoneNumber), "Phone field does not show!");
        }

        [When(@"I add a new user as below")]
        public void WhenIAddANewUserAsBelow(Table table)
        {
            By txtFirstName = By.XPath(UserDetailScreen.txtFirstName);
            By txtLastName = By.XPath(UserDetailScreen.txtLastName);
            By txtEmail = By.XPath(UserDetailScreen.txtEmail);
            By dropdownRole = By.XPath(UserDetailScreen.dropdownRole);
            By dropdownPhoneType = By.XPath(UserDetailScreen.dropPhoneType);
            By txtPhoneNumber = By.XPath(UserDetailScreen.txtPhoneNumber);
            By btnSave = By.XPath(UserDetailScreen.btnSave);

            var dataset = table.CreateSet<UserDetail>();

            foreach (var data in dataset)
            {
                By optionRole = By.XPath("//li[text()='" + data.Role + "']");
                By optionPhoneType = By.XPath("//li[text()='" + data.PhoneType + "']");

                data.Email = string.IsNullOrEmpty(data.Email) ? webHelper.GenerateEmail() : data.Email;

                userEmail = data.Email;

                driver.FindElement(txtFirstName).SendKeys(data.FirstName);
                driver.FindElement(txtLastName).SendKeys(data.LastName);
                driver.FindElement(txtEmail).SendKeys(data.Email);
                driver.FindElement(dropdownRole).Click();
                driver.FindElement(optionRole).Click();

                if (!string.IsNullOrEmpty(data.Phone))
                {
                    driver.FindElement(dropdownPhoneType).Click();
                    driver.FindElement(optionPhoneType).Click();
                    driver.FindElement(txtPhoneNumber).SendKeys(data.Phone);
                }

                driver.FindElement(btnSave).Click();
            }
        }

        [Then(@"the Add User successfully toast message should display")]
        public void ThenTheAddUserSuccessfullyToastMessageShouldDisplay()
        {
            By element = By.XPath(UserDetailScreen.messageSaved);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Successfully message does not show!");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);

            wait.Until(drv => drv.FindElements(element).Count == 0);
        }

        [When(@"I search for the user '([^']*)'")]
        public void WhenISearchForTheUser(string input)
        {
            By element = By.XPath(UserManagementScreen.txtSearch);

            driver.FindElement(element).SendKeys(input);
        }

        [Then(@"the new user '([^']*)' should display")]
        public void ThenTheNewUserShouldDisplay(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "New user does not show!");
        }

        [When(@"I check the user mailbox")]
        public void WhenICheckTheUserMailbox()
        {
            By txtEmail = By.XPath(YOPMailScreen.txtEmail);
            By btnLogin = By.XPath(YOPMailScreen.btnLogin);

            driver.Navigate().GoToUrl("https://yopmail.com/en/");
            driver.FindElement(txtEmail).SendKeys(userEmail);
            driver.FindElement(btnLogin).Click();
        }

        [Then(@"the user should receive invitation mail")]
        public void ThenTheUserShouldReceiveInvitationMail()
        {
            var counter = 0;

            By mailInvitation = By.XPath(YOPMailScreen.mailInvitation);
            By linkInvitation = By.XPath(YOPMailScreen.linkInvitation);
            By frameEmail = By.XPath(YOPMailScreen.frameEmail);
            By frameInbox = By.XPath(YOPMailScreen.frameInbox);
            By btnDelete = By.XPath(YOPMailScreen.btnDelete);

            wait.Until(drv => drv.FindElement(frameInbox));

            Assert.IsTrue(webHelper.IsElementPresent(frameInbox), "Mailbox does not show!");

            while (counter < 5)
            {
                driver.SwitchTo().Frame(driver.FindElement(frameInbox));

                if (webHelper.IsElementPresent(mailInvitation)) break;

                driver.Navigate().Refresh();

                counter++;
            }

            Assert.IsTrue(webHelper.IsElementPresent(mailInvitation), "Invitation email does not exist!");

            driver.FindElement(mailInvitation).Click();

            driver.SwitchTo().DefaultContent();

            driver.SwitchTo().Frame(driver.FindElement(frameEmail));

            Assert.IsTrue(webHelper.IsElementPresent(linkInvitation), "Invitation link does not exist!");

            urlInvitation = driver.FindElement(linkInvitation).GetAttribute("href");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            driver.FindElement(btnDelete).Click();

            wait.Until(drv => drv.FindElements(linkInvitation).Count == 0);
        }

        [When(@"I click on the reset password link in the email")]
        [When(@"I click on the invitation link in the email")]
        public void WhenIClickOnTheInvitationLinkInTheEmail()
        {
            driver.Navigate().GoToUrl(urlInvitation);
        }

        [Then(@"the Reset Password screen should display")]
        public void ThenTheResetPasswordScreenShouldDisplay()
        {
            By titleResetPassword = By.XPath(ResetPasswordScreen.titleResetPassword);
            By txtPassword = By.XPath(ResetPasswordScreen.txtPassword);
            By txtConfirmPassword = By.XPath(ResetPasswordScreen.txtConfirmPassword);

            if (driver.WindowHandles.Count > 1)
            {
                driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
            }

            wait.Until(drv => drv.FindElement(titleResetPassword));

            Assert.IsTrue(webHelper.IsElementPresent(titleResetPassword), "Reset password screen does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(txtPassword), "Password field does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(txtConfirmPassword), "Confirm password field does not show!");
        }

        [When(@"I input '([^']*)' for Password field")]
        public void WhenIInputForPasswordField(string input)
        {
            By element = By.XPath(ResetPasswordScreen.txtPassword);

            driver.FindElement(element).SendKeys(input);

            userPassword = input;
        }

        [When(@"I input '([^']*)' for Confirm Password field")]
        public void WhenIInputForConfirmPasswordField(string input)
        {
            By element = By.XPath(ResetPasswordScreen.txtConfirmPassword);

            driver.FindElement(element).SendKeys(input);
        }

        [When(@"I click Update Password button")]
        public void WhenIClickUpdatePasswordButton()
        {
            By element = By.XPath(ResetPasswordScreen.btnUpdatePassword);

            driver.FindElement(element).Click();
        }

        [Then(@"the Password Updated screen should display")]
        public void ThenThePasswordUpdatedScreenShouldDisplay()
        {
            By element = By.XPath(ResetPasswordScreen.titlePasswordUpdated);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Password updated screen does not show!");
        }

        [When(@"I click Back to Login button")]
        public void WhenIClickBackToLoginButton()
        {
            By element = By.XPath(ResetPasswordScreen.btnBackToLogin);

            driver.FindElement(element).Click();
        }

        [When(@"I login with the new user")]
        public void WhenILoginWithTheNewUser()
        {
            By txtEmail = By.XPath(LoginScreen.txtEmail);
            By txtPassword = By.XPath(LoginScreen.txtPassword);
            By btnSignIn = By.XPath(LoginScreen.btnSignIn);

            driver.FindElement(txtEmail).SendKeys(userEmail);
            driver.FindElement(txtPassword).SendKeys(userPassword);
            driver.FindElement(btnSignIn).Click();
        }

        [When(@"I click Save button")]
        public void WhenIClickSaveButton()
        {
            By element = By.XPath(UserDetailScreen.btnSave);

            driver.FindElement(element).Click();
        }

        [Then(@"the validation for mandatory fields in User Detail screen should display")]
        public void ThenTheValidationForMandatoryFieldsInUserDetailScreenShouldDisplay()
        {
            By messageFirstNameValidation = By.XPath(UserDetailScreen.messageFirstNameValidation);
            By messageLastNameValidation = By.XPath(UserDetailScreen.messageLastNameValidation);
            By messageEmailvalidation = By.XPath(UserDetailScreen.messageEmailvalidation);
            By messageRoleValidation = By.XPath(UserDetailScreen.messageRoleValidation);

            wait.Until(drv => drv.FindElement(messageFirstNameValidation));

            Assert.IsTrue(webHelper.IsElementPresent(messageFirstNameValidation), "Missing validation message!");
            Assert.IsTrue(webHelper.IsElementPresent(messageLastNameValidation), "Missing validation message!");
            Assert.IsTrue(webHelper.IsElementPresent(messageEmailvalidation), "Missing validation message!");
            Assert.IsTrue(webHelper.IsElementPresent(messageRoleValidation), "Missing validation message!");
        }

        [Then(@"the validation for duplicated email in User Detail screen should display")]
        public void ThenTheValidationForDuplicatedEmailInUserDetailScreenShouldDisplay()
        {
            By element = By.XPath(UserDetailScreen.messageDuplicatedMail);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Missing validation message!");
        }

        [When(@"I input '([^']*)' for Email field")]
        public void WhenIInputForEmailField(string input)
        {
            By element = By.XPath(UserDetailScreen.txtEmail);

            driver.FindElement(element).SendKeys(input);
        }

        [Then(@"the validation for invalid email in User Detail screen should display")]
        public void ThenTheValidationForInvalidEmailInUserDetailScreenShouldDisplay()
        {
            By txtEmail = By.XPath(UserDetailScreen.txtEmail);
            By messageInvalidEmail = By.XPath(UserDetailScreen.messageInvalidEmail);

            wait.Until(drv => drv.FindElement(messageInvalidEmail));

            Assert.IsTrue(webHelper.IsElementPresent(messageInvalidEmail), "Missing validation message!");

            webHelper.ClearElementText(txtEmail);
        }

        [When(@"I select the user '([^']*)'")]
        public void WhenISelectTheUser(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            driver.FindElement(element).Click();
        }

        [Then(@"the user detail screen should display with the below information populated")]
        public void ThenTheUserDetailScreenShouldDisplayWithTheBelowInformationPopulated(Table table)
        {
            By txtFirstName = By.XPath(UserDetailScreen.txtFirstName);
            By txtLastName = By.XPath(UserDetailScreen.txtLastName);
            By txtEmail = By.XPath(UserDetailScreen.txtEmail);
            By dropdownRole = By.XPath(UserDetailScreen.dropdownRole);
            By txtPhoneNumber = By.XPath(UserDetailScreen.txtPhoneNumber);

            wait.Until(drv => drv.FindElement(txtEmail));

            var dataset = table.CreateSet<UserDetail>();

            foreach (var data in dataset)
            {
                Assert.IsTrue(webHelper.ValidateElementText(txtFirstName, data.FirstName), "First Name mismatch!");
                Assert.IsTrue(webHelper.ValidateElementText(txtLastName, data.LastName), "Last Name mismatch!");
                Assert.IsTrue(webHelper.ValidateElementText(txtEmail, data.Email), "Email mismatch!");
                Assert.IsTrue(webHelper.ValidateElementText(dropdownRole, data.Role), "Role mismatch!");
                Assert.IsTrue(webHelper.ValidateElementText(txtPhoneNumber, data.Phone), "Phone mismatch!");
            }
        }

        [Then(@"the Email field should be disabled")]
        public void ThenTheEmailFieldShouldBeDisabled()
        {
            By element = By.XPath(UserDetailScreen.txtEmail);

            Assert.IsNotNull(driver.FindElement(element).GetAttribute("disabled"), "Email is not disabled!");
        }

        [When(@"I input '([^']*)' for First Name field")]
        public void WhenIInputForFirstNameField(string input)
        {
            By element = By.XPath(UserDetailScreen.txtFirstName);

            driver.FindElement(element).SendKeys(input);
        }

        [When(@"I input '([^']*)' for Last Name field")]
        public void WhenIInputForLastNameField(string input)
        {
            By element = By.XPath(UserDetailScreen.txtLastName);

            driver.FindElement(element).SendKeys(input);
        }

        [When(@"I input '([^']*)' for Phone Number field")]
        public void WhenIInputForPhoneNumberField(string input)
        {
            By element = By.XPath(UserDetailScreen.txtPhoneNumber);

            driver.FindElement(element).SendKeys(input);
        }

        [When(@"I delete the user '([^']*)'")]
        public void WhenIDeleteTheUser(string input)
        {
            By buttonMenu = By.XPath("//td[text()='" + input + "']/following-sibling::*//*[@id='menu-button']");
            By optionDelete = By.XPath(UserManagementScreen.optionDelete);

            driver.FindElement(buttonMenu).Click();
            driver.FindElement(optionDelete).Click();
        }

        [Then(@"the Confirmation popup should display")]
        [Then(@"the Delete Confirmation popup should display")]
        public void ThenTheDeleteConfirmationPopupShouldDisplay()
        {
            By element = By.XPath(UserManagementScreen.btnYes);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Confirmation popup does not show!");
        }

        [When(@"I select No in the popup")]
        public void WhenISelectNoInThePopup()
        {
            By element = By.XPath(UserManagementScreen.btnNo);

            driver.FindElement(element).Click();
        }

        [Then(@"the Confirmation popup should dismiss")]
        [Then(@"the Delete Confirmation popup should dismiss")]
        public void ThenTheDeleteConfirmationPopupShouldDismiss()
        {
            By element = By.XPath(UserManagementScreen.btnNo);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);

            wait.Until(drv => drv.FindElements(element).Count == 0);

            Assert.IsFalse(webHelper.IsElementPresent(element), "Popup still exists!");
        }

        [When(@"I select Yes in the popup")]
        public void WhenISelectYesInThePopup()
        {
            By element = By.XPath(UserManagementScreen.btnYes);

            driver.FindElement(element).Click();
        }

        [Then(@"the user '([^']*)' should no longer show")]
        public void ThenTheUserShouldNoLongerShow(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);

            wait.Until(drv => drv.FindElements(element).Count == 0);

            Assert.IsFalse(webHelper.IsElementPresent(element), "User still exists!");
        }

        [When(@"I open the option menu of the user '([^']*)'")]
        public void WhenIOpenTheOptionMenuOfTheUser(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']/following-sibling::*//*[@id='menu-button']");

            driver.FindElement(element).Click();
        }

        [Then(@"the Resend Invitation option should not display")]
        public void ThenTheResendInvitationOptionShouldNotDisplay()
        {
            By element = By.XPath(UserManagementScreen.optionResendInvitation);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);

            Assert.IsFalse(webHelper.IsElementPresent(element), "Resend invitiation still shows!");
        }

        [When(@"I resend invitation the user '([^']*)'")]
        public void WhenIResendInvitationTheUser(string input)
        {
            By buttonOption = By.XPath("//td[text()='" + input + "']/following-sibling::*//*[@id='menu-button']");
            By optionResend = By.XPath(UserManagementScreen.optionResendInvitation);

            driver.FindElement(buttonOption).Click();
            driver.FindElement(optionResend).Click();
        }

        [Then(@"the Sent message should display")]
        public void ThenTheSentMessageShouldDisplay()
        {
            By element = By.XPath(UserManagementScreen.messageSent);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Sent message does not show!");
        }

        [When(@"I reset password for user '([^']*)'")]
        public void WhenIResetPasswordForUser(string input)
        {
            By buttonOption = By.XPath("//td[text()='" + input + "']/following-sibling::*//*[@id='menu-button']");
            By optionResetPassword = By.XPath(UserManagementScreen.optionResetPassword);

            driver.FindElement(buttonOption).Click();
            driver.FindElement(optionResetPassword).Click();
        }

        [When(@"I check the mailbox '([^']*)'")]
        public void WhenICheckTheMailbox(string input)
        {
            By txtEmail = By.XPath(YOPMailScreen.txtEmail);
            By btnLogin = By.XPath(YOPMailScreen.btnLogin);

            driver.Navigate().GoToUrl("https://yopmail.com/en/");
            webHelper.ClearElementText(txtEmail);
            driver.FindElement(txtEmail).SendKeys(input);
            driver.FindElement(btnLogin).Click();
        }

        [Then(@"the user should receive reset password mail")]
        public void ThenTheUserShouldReceiveResetPasswordMail()
        {
            var counter = 0;

            By mailReset = By.XPath(YOPMailScreen.mailReset);
            By linkInvitation = By.XPath(YOPMailScreen.linkInvitation);
            By frameEmail = By.XPath(YOPMailScreen.frameEmail);
            By frameInbox = By.XPath(YOPMailScreen.frameInbox);
            By btnDelete = By.XPath(YOPMailScreen.btnDelete);

            wait.Until(drv => drv.FindElement(frameInbox));

            Assert.IsTrue(webHelper.IsElementPresent(frameInbox), "Mailbox does not show!");

            while (counter < 5)
            {
                driver.SwitchTo().Frame(driver.FindElement(frameInbox));

                if (webHelper.IsElementPresent(mailReset)) break;

                driver.Navigate().Refresh();

                counter++;
            }

            Assert.IsTrue(webHelper.IsElementPresent(mailReset), "Reset email does not exist!");

            driver.FindElement(mailReset).Click();

            driver.SwitchTo().DefaultContent();

            driver.SwitchTo().Frame(driver.FindElement(frameEmail));

            Assert.IsTrue(webHelper.IsElementPresent(linkInvitation), "Reset link does not exist!");

            urlInvitation = driver.FindElement(linkInvitation).GetAttribute("href");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            driver.FindElement(btnDelete).Click();

            wait.Until(drv => drv.FindElements(linkInvitation).Count == 0);
        }

        [Then(@"the user should receive password reset successfully mail")]
        public void ThenTheUserShouldReceivePasswordResetSuccessfullyMail()
        {
            var counter = 0;

            By frameInbox = By.XPath(YOPMailScreen.frameInbox);
            By mailSuccessfully = By.XPath(YOPMailScreen.mailSuccessfully);

            while (counter < 5)
            {
                driver.SwitchTo().Frame(driver.FindElement(frameInbox));

                if (webHelper.IsElementPresent(mailSuccessfully)) break;

                driver.Navigate().Refresh();

                counter++;
            }

            Assert.IsTrue(webHelper.IsElementPresent(mailSuccessfully), "Reset successfully email does not exist!");

            webHelper.ClearMailbox();
        }

        [When(@"I select '([^']*)' for Phone Type field")]
        public void WhenISelectForPhoneTypeField(string input)
        {
            By dropPhoneType = By.XPath(UserDetailScreen.dropPhoneType);
            By optionPhoneType = By.XPath("//li[text()='" + input + "']");

            driver.FindElement(dropPhoneType).Click();
            driver.FindElement(optionPhoneType).Click();
        }

    }
}
