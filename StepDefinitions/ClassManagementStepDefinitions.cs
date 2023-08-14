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
    public class ClassManagementStepDefinitions
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private WebHelper webHelper;
        private static DateTime classEndDate;

        public ClassManagementStepDefinitions(ScenarioContext context)
        {
            driver = (EdgeDriver)context["driver"];
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            webHelper = new WebHelper() { driver = driver };
        }

        [When(@"I select Add Class button")]
        public void WhenISelectAddClassButton()
        {
            By element = By.XPath(ClassManagementScreen.btnAddClass);

            wait.Until(drv => drv.FindElement(element).GetAttribute("disabled") == null);

            driver.FindElement(element).Click();
        }

        [Then(@"the Add Class screen should display")]
        public void ThenTheAddClassScreenShouldDisplay()
        {
            By element = By.XPath(ClassDetailScreen.txtClassCode);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Add Class screen does not show!");
        }

        [Then(@"the Main Teacher should be populated as current user")]
        public void ThenTheMainTeacherShouldBePopulatedAsCurrentUser()
        {
            By lblCurrentUser = By.XPath(HomeScreen.lblCurrentUser);
            By txtMainTeacher = By.XPath(ClassDetailScreen.txtMainTeacher);

            wait.Until(drv => drv.FindElement(txtMainTeacher));

            var currentUser = driver.FindElement(lblCurrentUser).Text;

            Assert.IsTrue(webHelper.ValidateElementText(txtMainTeacher, currentUser), "Main teacher is not populated!");
        }

        [When(@"I add a new class as below")]
        public void WhenIAddANewClassAsBelow(Table table)
        {
            var dataset = table.CreateSet<ClassDetail>();

            foreach (var data in dataset)
            {
                By txtClassName = By.XPath(ClassDetailScreen.txtClassName);
                By txtSecondaryTeacher = By.XPath(ClassDetailScreen.txtSecondaryTeacher);
                By txtStatus = By.XPath(ClassDetailScreen.txtStatus);
                By optionStatus = By.XPath("//li[text()='" + data.Status + "']");
                By txtTextBook = By.XPath(ClassDetailScreen.txtTextBook);
                By optionTextBook = By.XPath("//*[text()='" + data.TextBook + "']");
                By iconStartDate = By.XPath(ClassDetailScreen.iconStartDate);
                By iconEndDate = By.XPath(ClassDetailScreen.iconEndDate);
                By popupCalendar = By.XPath(ClassDetailScreen.popupCalendar);

                driver.FindElement(txtStatus).Click();
                driver.FindElement(optionStatus).Click();

                if (!string.IsNullOrEmpty(data.SecondaryTeacher))
                {
                    var teacherList = data.SecondaryTeacher.Split(',');

                    foreach (var teacher in teacherList)
                    {
                        By optionTeacher = By.XPath("//li[contains(text(),'" + teacher + "')]");
                        driver.FindElement(txtSecondaryTeacher).Click();
                        driver.FindElement(optionTeacher).Click();
                    }
                }

                driver.FindElement(txtClassName).SendKeys(data.ClassName);

                driver.FindElement(txtTextBook).Click();
                driver.FindElement(optionTextBook).Click();

                var startDate = DateTime.UtcNow;
                var endDate = startDate.AddMonths(1);

                By startMonth = By.XPath("//*[text()='" + startDate.ToString("MMM") + "']");
                By startYear = By.XPath("//*[text()='" + startDate.ToString("yyyy") + "']");
                By endMonth = By.XPath("//*[text()='" + endDate.ToString("MMM") + "']");
                By endYear = By.XPath("//*[text()='" + endDate.ToString("yyyy") + "']");



                driver.FindElement(iconStartDate).Click();
                wait.Until(drv => drv.FindElement(popupCalendar));
                Thread.Sleep(200);
                webHelper.ClickByJavascript(startMonth);
                Thread.Sleep(200);
                webHelper.ClickByJavascript(startYear);
                driver.FindElement(iconEndDate).Click();
                wait.Until(drv => drv.FindElement(popupCalendar));
                Thread.Sleep(200);
                webHelper.ClickByJavascript(endMonth);
                Thread.Sleep(200);
                webHelper.ClickByJavascript(endYear);

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

                wait.Until(drv => drv.FindElements(popupCalendar).Count == 0);
            }
        }

        [Then(@"the Community Link field should display")]
        public void ThenTheCommunityLinkFieldShouldDisplay()
        {
            By element = By.XPath(ClassDetailScreen.txtCommunityLink);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Community Link does not show!");

            //TODO: Add verify href
        }

        [Then(@"the Class URL field should display")]
        public void ThenTheClassURLFieldShouldDisplay()
        {
            By element = By.XPath(ClassDetailScreen.txtURLLink);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Class URL does not show!");

            //TODO: Add verify href
        }

        [Then(@"the QR Code should display")]
        public void ThenTheQRCodeShouldDisplay()
        {
            By element = By.XPath(ClassDetailScreen.imgQRCode);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "QR Code does not show!");
            Assert.That(driver.FindElement(element).GetAttribute("src"), Is.Not.Null.Or.Empty, "QR Code is empty!");
        }

        [Then(@"the Class Code field should auto generated")]
        public void ThenTheClassCodeFieldShouldAutoGenerated()
        {
            By txtClassCode = By.XPath(ClassDetailScreen.txtClassCode);
            By imgQRCode = By.XPath(ClassDetailScreen.imgQRCode);

            wait.Until(drv => drv.FindElement(imgQRCode));
            wait.Until(drv => webHelper.GetElementText(txtClassCode) != string.Empty);

            var classCode = webHelper.GetElementText(txtClassCode).Split('-');

            Assert.That(webHelper.GetElementText(txtClassCode), Is.Not.Null.Or.Empty, "Class Code is empty!");
            Assert.That(classCode.Length, Is.EqualTo(3), "Class Code is not correct!");
        }

        [When(@"I click Cancel button")]
        public void WhenIClickCancelButton()
        {
            By element = By.XPath(ClassDetailScreen.btnCancel);

            driver.FindElement(element).Click();
        }

        [When(@"I search for the class '([^']*)'")]
        public void WhenISearchForTheClass(string input)
        {
            By element = By.XPath(ClassManagementScreen.txtSearch);

            driver.FindElement(element).SendKeys(input);
        }

        [Then(@"the new class '([^']*)' should display")]
        public void ThenTheNewClassShouldDisplay(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "New class does not show!");
        }

        [Then(@"the validation message for Class Name field should display")]
        public void ThenTheValidationMessageForClassNameFieldShouldDisplay()
        {
            By element = By.XPath(ClassDetailScreen.messageClassNameRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation mesasge does not show!");
        }

        [Then(@"the validation message for Textbook field should display")]
        public void ThenTheValidationMessageForTextbookFieldShouldDisplay()
        {
            By element = By.XPath(ClassDetailScreen.messageTextbookRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation mesasge does not show!");
        }

        [When(@"I edit the class '([^']*)'")]
        public void WhenIEditTheClass(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            driver.FindElement(element).Click();
        }

        [Then(@"the class detail should be shown as below")]
        public void ThenTheClassDetailShouldBeShownAsBelow(Table table)
        {
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddMonths(1);
            var dataset = table.CreateSet<ClassDetail>();

            foreach (var data in dataset)
            {
                By txtClassName = By.XPath(ClassDetailScreen.txtClassName);
                By txtSecondaryTeacher = By.XPath("//*[text()='" + data.SecondaryTeacher + "']");
                By txtStatus = By.XPath(ClassDetailScreen.txtStatus);
                By txtTextBook = By.XPath(ClassDetailScreen.txtTextBook);
                By txtStartDate = By.XPath(ClassDetailScreen.txtStartDate);
                By txtEndDate = By.XPath(ClassDetailScreen.txtEndDate);

                wait.Until(drv => drv.FindElement(txtClassName));

                data.StartDate = string.IsNullOrEmpty(data.StartDate) ? startDate.ToString("MMMM yyyy") : data.StartDate;
                data.EndDate = string.IsNullOrEmpty(data.EndDate) ? endDate.ToString("MMMM yyyy") : data.EndDate;

                if (!string.IsNullOrEmpty(data.SecondaryTeacher))
                {
                    var teacherList = data.SecondaryTeacher.Split(',');

                    foreach (var teacher in teacherList)
                    {
                        By teacherElement = By.XPath("//*[text()='" + teacher + "']");

                        Assert.IsTrue(webHelper.IsElementPresent(teacherElement), String.Format("Teacher {0} does not show!", teacher));
                    }
                }

                Assert.That(webHelper.GetElementText(txtClassName), Is.EqualTo(data.ClassName), "Class Name mismatch!");
                Assert.That(webHelper.GetElementText(txtStatus), Is.EqualTo(data.Status), "Status mismatch!");
                Assert.That(webHelper.GetElementText(txtTextBook), Is.EqualTo(data.TextBook), "Textbook mismatch!");
                Assert.That(webHelper.GetElementText(txtStartDate), Is.EqualTo(data.StartDate), "Start date mismatch!");
                Assert.That(webHelper.GetElementText(txtEndDate), Is.EqualTo(data.EndDate), "End date mismatch!");
            }
        }

        [When(@"I make some changes of the class as below")]
        public void WhenIMakeSomeChangesOfTheClassAsBelow(Table table)
        {
            var dataset = table.CreateSet<ClassDetail>();

            foreach (var data in dataset)
            {
                By txtClassName = By.XPath(ClassDetailScreen.txtClassName);
                By txtSecondaryTeacher = By.XPath(ClassDetailScreen.txtSecondaryTeacher);
                By txtStatus = By.XPath(ClassDetailScreen.txtStatus);
                By optionStatus = By.XPath("//li[text()='" + data.Status + "']");
                By txtTextBook = By.XPath(ClassDetailScreen.txtTextBook);
                By optionTextBook = By.XPath("//*[text()='" + data.TextBook + "']");
                By iconEndDate = By.XPath(ClassDetailScreen.iconEndDate);
                By popupCalendar = By.XPath(ClassDetailScreen.popupCalendar);

                driver.FindElement(txtStatus).Click();
                driver.FindElement(optionStatus).Click();

                if (!string.IsNullOrEmpty(data.SecondaryTeacher))
                {
                    var teacherList = data.SecondaryTeacher.Split(',');

                    foreach (var teacher in teacherList)
                    {
                        By currentTeacher = By.XPath("//*[contains(text(),'" + teacher + "')]");
                        if (webHelper.IsElementPresent(currentTeacher))
                        {
                            continue;
                        }
                        else
                        {
                            By optionTeacher = By.XPath("//li[contains(text(),'" + teacher + "')]");
                            driver.FindElement(txtSecondaryTeacher).Click();
                            driver.FindElement(optionTeacher).Click();
                        }
                    }
                }

                webHelper.ClearElementText(txtClassName);
                driver.FindElement(txtClassName).SendKeys(data.ClassName);
                driver.FindElement(txtTextBook).Click();
                driver.FindElement(optionTextBook).Click();

                classEndDate = DateTime.UtcNow.AddMonths(2);

                By endMonth = By.XPath("//*[contains(@class,'MuiDateCalendar-root')]//*[text()='" + classEndDate.ToString("MMM") + "']");
                By endYear = By.XPath("//*[contains(@class,'MuiDateCalendar-root')]//*[text()='" + classEndDate.ToString("yyyy") + "']");

                driver.FindElement(iconEndDate).Click();
                Thread.Sleep(200);
                webHelper.ClickByJavascript(endMonth);
                Thread.Sleep(200);
                webHelper.ClickByJavascript(endYear);

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

                wait.Until(drv => drv.FindElements(popupCalendar).Count == 0);
            }
        }

        [Then(@"the class detail should be updated as below")]
        public void ThenTheClassDetailShouldBeUpdatedAsBelow(Table table)
        {
            var dataset = table.CreateSet<ClassDetail>();

            foreach (var data in dataset)
            {
                By endDate = By.XPath("(//td[text()='" + data.ClassName + "']/following-sibling::*)[3]");
                By book = By.XPath("(//td[text()='" + data.ClassName + "']/following-sibling::*)[4]");
                By status = By.XPath("(//td[text()='" + data.ClassName + "']/following-sibling::*)[5]");

                Assert.That(webHelper.GetElementText(endDate), Is.EqualTo(classEndDate.ToString("MM-yyyy")), "End date mismatch!");
                Assert.That(webHelper.GetElementText(book), Is.EqualTo(data.TextBook), "Book mismatch!");
                Assert.That(webHelper.GetElementText(status), Is.EqualTo(data.Status), "Status mismatch!");
            }
        }

    }
}
