using HelloBear.Elements;
using HelloBear.Models;
using HelloBear.Support;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.IO.Compression;
using System.Security.Principal;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HelloBear.StepDefinitions
{
    [Binding]
    public class ContentManagementStepDefinitions
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private WebHelper webHelper;
        private static string bookName = string.Empty;
        private static string unitName = string.Empty;
        private static string zipFilePath = string.Empty;
        private static string extractedPath = string.Empty;

        public ContentManagementStepDefinitions(ScenarioContext context)
        {
            driver = (EdgeDriver)context["driver"];
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            webHelper = new WebHelper() { driver = driver };
        }

        [When(@"I select Add Book button")]
        public void WhenISelectAddBookButton()
        {
            By element = By.XPath(ContentManagementScreen.btnAddBook);

            driver.FindElement(element).Click();
        }

        [Then(@"the Add Book screen should display")]
        public void ThenTheAddBookScreenShouldDisplay()
        {
            By element = By.XPath(ContentDetailScreen.txtName);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Add Book screen does not show!");
        }

        [When(@"I add a new book as below")]
        public void WhenIAddANewBookAsBelow(Table table)
        {
            var dataset = table.CreateSet<BookDetail>();

            foreach (var data in dataset)
            {
                By txtName = By.XPath(ContentDetailScreen.txtName);
                By txtShortName = By.XPath(ContentDetailScreen.txtShortName);
                By txtDescription = By.XPath(ContentDetailScreen.txtDescription);
                By txtThumbnail = By.XPath(ContentDetailScreen.txtThumbnail);

                bookName = data.Name;

                driver.FindElement(txtName).SendKeys(data.Name);
                driver.FindElement(txtShortName).SendKeys(data.ShortName);
                driver.FindElement(txtDescription).SendKeys(data.Description);
                driver.FindElement(txtThumbnail).SendKeys(webHelper.GetMaterialPath("thumbnail.png"));
            }
        }

        [Then(@"the Saved message should display")]
        public void ThenTheSavedMessageShouldDisplay()
        {
            By element = By.XPath(ContentDetailScreen.messageSaved);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Message Saved does not show!");
        }

        [When(@"I search for the book '([^']*)'")]
        public void WhenISearchForTheBook(string input)
        {
            By element = By.XPath(ContentManagementScreen.txtSearch);

            webHelper.ClearElementText(element);

            driver.FindElement(element).SendKeys(input);
        }

        [Then(@"the book '([^']*)' should display")]
        public void ThenTheBookShouldDisplay(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Book does not show!");
        }

        [Then(@"the Book thumbnail should display")]
        public void ThenTheBookThumbnailShouldDisplay()
        {
            By element = By.XPath(ContentDetailScreen.imgThumbnail);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Book thumbnail does not show!");
        }

        [When(@"I edit the book '([^']*)'")]
        public void WhenIEditTheBook(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            driver.FindElement(element).Click();
        }

        [Then(@"the book detail screen should display as below")]
        public void ThenTheBookDetailScreenShouldDisplayAsBelow(Table table)
        {
            var dataset = table.CreateSet<BookDetail>();

            foreach (var data in dataset)
            {
                By txtName = By.XPath(ContentDetailScreen.txtName);
                By txtShortName = By.XPath(ContentDetailScreen.txtShortName);
                By txtDescription = By.XPath(ContentDetailScreen.txtDescription);
                By imgThumbnail = By.XPath(ContentDetailScreen.imgThumbnail);

                wait.Until(drv => drv.FindElement(txtShortName));
                wait.Until(drv => webHelper.GetElementText(txtName).Contains(data.Name));

                Assert.That(webHelper.GetElementText(txtName), Is.EqualTo(data.Name), "Book name mismatch!");
                Assert.That(webHelper.GetElementText(txtShortName), Is.EqualTo(data.ShortName), "Short name mismatch!");
                Assert.That(webHelper.GetElementText(txtDescription), Is.EqualTo(data.Description), "Description mismatch!");
                Assert.That(driver.FindElement(imgThumbnail).GetAttribute("src"), Is.Not.Null.Or.Empty, "Thumbnail does not show!");
            }
        }

        [Then(@"the Short Name field should be disabled")]
        public void ThenTheShortNameFieldShouldBeDisabled()
        {
            By element = By.XPath(ContentDetailScreen.txtShortName);

            Assert.That(driver.FindElement(element).GetAttribute("class").Contains("disabled"), Is.True, "Short name is not disabled!");
        }

        [When(@"I input '([^']*)' into Book Name field")]
        public void WhenIInputIntoBookNameField(string input)
        {
            By element = By.XPath(ContentDetailScreen.txtName);

            driver.FindElement(element).SendKeys(input);
        }

        [When(@"I input '([^']*)' into Book Description field")]
        public void WhenIInputIntoBookDescriptionField(string input)
        {
            By element = By.XPath(ContentDetailScreen.txtDescription);

            driver.FindElement(element).SendKeys(input);
        }

        [When(@"I delete the book '([^']*)'")]
        public void WhenIDeleteTheBook(string input)
        {
            By menuOption = By.XPath("//td[text()='" + input + "']/following-sibling::*//*[@id='menu-button']");
            By btnDelete = By.XPath(ContentManagementScreen.btnDelete);

            driver.FindElement(menuOption).Click();
            driver.FindElement(btnDelete).Click();
        }

        [Then(@"the book '([^']*)' should be removed")]
        public void ThenTheBookShouldBeRemoved(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            wait.Until(drv => drv.FindElements(element).Count == 0);

            Assert.IsFalse(webHelper.IsElementPresent(element), "Book is not removed!");
        }

        [Then(@"the Delete option should not show on the book '([^']*)'")]
        public void ThenTheDeleteOptionShouldNotShowOnTheBook(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']/following-sibling::*//*[@id='menu-button']");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            wait.Until(drv => drv.FindElements(element).Count == 0);

            Assert.IsFalse(webHelper.IsElementPresent(element), "Delete option is available for assigned book!");
        }

        [Then(@"the validation message for Book Name field should display")]
        public void ThenTheValidationMessageForBookNameFieldShouldDisplay()
        {
            By element = By.XPath(ContentDetailScreen.messageNameRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the validation message for Short Name field should display")]
        public void ThenTheValidationMessageForShortNameFieldShouldDisplay()
        {
            By element = By.XPath(ContentDetailScreen.messageShortNameRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [When(@"I input '([^']*)' into Book Short Name field")]
        public void WhenIInputIntoBookShortNameField(string input)
        {
            By element = By.XPath(ContentDetailScreen.txtShortName);

            driver.FindElement(element).SendKeys(input);
        }

        [Then(@"the validation message for Duplicated Short Name should display")]
        public void ThenTheValidationMessageForDuplicatedShortNameShouldDisplay()
        {
            By element = By.XPath(ContentDetailScreen.messageShortNameDuplicated);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the Unit section should display")]
        public void ThenTheUnitSectionShouldDisplay()
        {
            By btnAddUnit = By.XPath(ContentDetailScreen.btnAddUnit);
            By txtSearch = By.XPath(ContentDetailScreen.txtSearch);
            By gridData = By.XPath(ContentDetailScreen.gridData);

            wait.Until(drv => drv.FindElement(btnAddUnit));

            Assert.IsTrue(webHelper.IsElementPresent(btnAddUnit), "Button Add Unit does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(txtSearch), "Search field does not show!");
        }

        [When(@"I add a new unit as below")]
        public void WhenIAddANewUnitAsBelow(Table table)
        {
            var dataset = table.CreateSet<UnitDetail>();

            foreach (var data in dataset)
            {
                By txtName = By.XPath(UnitDetailScreen.txtName);
                By txtLanguageFocus = By.XPath(UnitDetailScreen.txtLanguageFocus);
                By txtNumber = By.XPath(UnitDetailScreen.txtNumber);
                By txtPhonics = By.XPath(UnitDetailScreen.txtPhonics);
                By txtDescription = By.XPath(UnitDetailScreen.txtDescription);
                By messsageSaved = By.XPath(UnitDetailScreen.messsageSaved);
                By btnSave = By.XPath(UnitDetailScreen.btnSave);
                By btnCancel = By.XPath(UnitDetailScreen.btnCancel);
                By btnAddUnit = By.XPath(ContentDetailScreen.btnAddUnit);

                unitName = data.Name;

                wait.Until(drv => drv.FindElement(btnAddUnit));

                driver.FindElement(btnAddUnit).Click();

                wait.Until(drv => drv.FindElement(txtLanguageFocus));

                driver.FindElement(txtName).SendKeys(data.Name);
                driver.FindElement(txtLanguageFocus).SendKeys(data.LanguageFocus);
                driver.FindElement(txtNumber).SendKeys(data.Number);
                driver.FindElement(txtPhonics).SendKeys(data.Phonics);
                driver.FindElement(txtDescription).SendKeys(data.Description);
                driver.FindElement(btnSave).Click();

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                wait.Until(drv => drv.FindElement(messsageSaved));
                wait.Until(drv => drv.FindElements(messsageSaved).Count == 0);

                driver.FindElement(btnCancel).Click();
            }
        }

        [Then(@"the Unit List of the book should display as below")]
        public void ThenTheUnitListOfTheBookShouldDisplayAsBelow(Table table)
        {
            var dataset = table.CreateSet<UnitDetail>();

            foreach (var data in dataset)
            {
                By txtName = By.XPath("(//td[text()='" + data.Number + "']/following-sibling::*)[1]");
                By txtDescription = By.XPath("(//td[text()='" + data.Number + "']/following-sibling::*)[2]");

                wait.Until(drv => drv.FindElement(txtName));

                Assert.That(webHelper.GetElementText(txtName).Contains(data.Name), Is.True, "Name does not match!");
                Assert.That(webHelper.GetElementText(txtDescription).Contains(data.Description), Is.True, "Description does not match!");
            }
        }

        [When(@"I search for the unit '([^']*)'")]
        public void WhenISearchForTheUnit(string input)
        {
            By element = By.XPath(ContentDetailScreen.txtSearch);

            driver.FindElement(element).SendKeys(input);
        }

        [Then(@"the unit '([^']*)' should display")]
        public void ThenTheUnitShouldDisplay(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Unit does not show!");
        }

        [When(@"I select Add Unit button")]
        public void WhenISelectAddUnitButton()
        {
            By element = By.XPath(ContentDetailScreen.btnAddUnit);

            driver.FindElement(element).Click();
        }

        [Then(@"the Add Unit screen should display")]
        public void ThenTheAddUnitScreenShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.txtLanguageFocus);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Add Unit screen does not show!");
        }

        [Then(@"the validation message for the Unit Name should display")]
        public void ThenTheValidationMessageForTheUnitNameShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.messageNameRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the validation message for the Unit Number should display")]
        public void ThenTheValidationMessageForTheUnitNumberShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.messageNumberRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the validation message for the Language Focus should display")]
        public void ThenTheValidationMessageForTheLanguageFocusShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.messageLanguageFocusRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the validation message for the Phonics should display")]
        public void ThenTheValidationMessageForThePhonicsShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.messagePhonicsRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [When(@"I edit the unit '([^']*)'")]
        public void WhenIEditTheUnit(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            driver.FindElement(element).Click();
        }

        [Then(@"unit detail page should display as below")]
        public void ThenUnitDetailPageShouldDisplayAsBelow(Table table)
        {
            var dataset = table.CreateSet<UnitDetail>();

            foreach (var data in dataset)
            {
                By txtName = By.XPath(UnitDetailScreen.txtName);
                By txtLanguageFocus = By.XPath(UnitDetailScreen.txtLanguageFocus);
                By txtNumber = By.XPath(UnitDetailScreen.txtNumber);
                By txtPhonics = By.XPath(UnitDetailScreen.txtPhonics);
                By txtDescription = By.XPath(UnitDetailScreen.txtDescription);

                wait.Until(drv => drv.FindElement(txtLanguageFocus));
                wait.Until(drv => webHelper.GetElementText(txtName).Contains(data.Name));

                Assert.That(webHelper.GetElementText(txtName), Is.EqualTo(data.Name), "Name mismatch!");
                Assert.That(webHelper.GetElementText(txtLanguageFocus), Is.EqualTo(data.LanguageFocus), "Language Focus mismatch!");
                Assert.That(webHelper.GetElementText(txtNumber), Is.EqualTo(data.Number), "Number mismatch!");
                Assert.That(webHelper.GetElementText(txtPhonics), Is.EqualTo(data.Phonics), "Phonics mismatch!");
                Assert.That(webHelper.GetElementText(txtDescription), Is.EqualTo(data.Description), "Description mismatch!");
            }
        }

        [When(@"I change the unit detail as below")]
        public void WhenIChangeTheUnitDetailAsBelow(Table table)
        {
            var dataset = table.CreateSet<UnitDetail>();

            foreach (var data in dataset)
            {
                By txtName = By.XPath(UnitDetailScreen.txtName);
                By txtLanguageFocus = By.XPath(UnitDetailScreen.txtLanguageFocus);
                By txtNumber = By.XPath(UnitDetailScreen.txtNumber);
                By txtPhonics = By.XPath(UnitDetailScreen.txtPhonics);
                By txtDescription = By.XPath(UnitDetailScreen.txtDescription);
                By messsageSaved = By.XPath(UnitDetailScreen.messsageSaved);
                By btnSave = By.XPath(UnitDetailScreen.btnSave);
                By btnCancel = By.XPath(UnitDetailScreen.btnCancel);

                webHelper.ChangeElementText(txtName, data.Name);
                webHelper.ChangeElementText(txtLanguageFocus, data.LanguageFocus);
                webHelper.ChangeElementText(txtNumber, data.Number);
                webHelper.ChangeElementText(txtPhonics, data.Phonics);
                webHelper.ChangeElementText(txtDescription, data.Description);
                driver.FindElement(btnSave).Click();

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                wait.Until(drv => drv.FindElement(messsageSaved));
                wait.Until(drv => drv.FindElements(messsageSaved).Count == 0);

                driver.FindElement(btnCancel).Click();
            }
        }

        [When(@"I add a new content as below")]
        public void WhenIAddANewContentAsBelow(Table table)
        {
            var dataset = table.CreateSet<ContentDetail>();

            foreach (var data in dataset)
            {
                By txtContentName = By.XPath(UnitDetailScreen.txtContentName);
                By txtType = By.XPath(UnitDetailScreen.txtType);
                By txtPageNumber = By.XPath(UnitDetailScreen.txtPageNumber);
                By txtYouTubeLink = By.XPath(UnitDetailScreen.txtYouTubeLink);
                By txtWordWallLink = By.XPath(UnitDetailScreen.txtWordWallLink);
                By txtDescription = By.XPath(UnitDetailScreen.txtDescription);
                By txtBookName = By.XPath(UnitDetailScreen.txtBookName);
                By btnAdd = By.XPath(UnitDetailScreen.btnAdd);
                By btnSave = By.XPath(UnitDetailScreen.btnSave);
                By btnUploadPageImage = By.XPath(UnitDetailScreen.btnUploadPageImage);
                By messsageSaved = By.XPath(UnitDetailScreen.messsageSaved);
                By gridData = By.XPath(UnitDetailScreen.gridData);
                By imgPageThumbnail = By.XPath(UnitDetailScreen.imgPageThumbnail);
                By optionType = data.Type == ContentType.PushAndListen ? By.XPath("//*[text()='Push & Listen']") : By.XPath("//*[text()='" + data.Type + "']");

                wait.Until(drv => drv.FindElement(btnAdd));

                driver.FindElement(btnAdd).Click();

                wait.Until(drv => drv.FindElement(txtBookName));
                wait.Until(drv => string.IsNullOrEmpty(webHelper.GetElementText(txtBookName)) == false);

                driver.FindElement(txtContentName).SendKeys(data.Name);
                driver.FindElement(txtType).Click();
                driver.FindElement(optionType).Click();
                webHelper.ChangeElementText(txtPageNumber, data.PageNumber);
                driver.FindElement(txtDescription).SendKeys(data.Description);
                driver.FindElement(btnUploadPageImage).SendKeys(webHelper.RandomPageImage());

                switch (data.Type)
                {
                    case ContentType.Read:
                    case ContentType.Music:
                    case ContentType.Video:
                        driver.FindElement(txtYouTubeLink).SendKeys(data.Link);
                        break;
                    case ContentType.Game:
                        driver.FindElement(txtWordWallLink).SendKeys(data.Link);
                        break;
                    default:
                        break;
                }

                wait.Until(drv => drv.FindElement(imgPageThumbnail));

                driver.FindElement(btnSave).Click();

                wait.Until(drv => drv.FindElement(gridData));

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                wait.Until(drv => drv.FindElements(messsageSaved).Count == 0);
            }
        }

        [When(@"I select Add Content button")]
        public void WhenISelectAddContentButton()
        {
            By element = By.XPath(UnitDetailScreen.btnAdd);

            driver.FindElement(element).Click();
        }

        [Then(@"the Add Content screen should display")]
        public void ThenTheAddContentScreenShouldDisplay()
        {
            By btnUploadPageImage = By.XPath(UnitDetailScreen.btnUploadPageImage);
            By txtBookName = By.XPath(UnitDetailScreen.txtBookName);

            wait.Until(drv => drv.FindElement(btnUploadPageImage));
            wait.Until(drv => string.IsNullOrEmpty(webHelper.GetElementText(txtBookName)) == false);

            Assert.IsTrue(webHelper.IsElementPresent(btnUploadPageImage), "Add Content screen does not show!");
        }

        [Then(@"the Book Name and Unit Name should be populated correctly")]
        public void ThenTheBookNameAndUnitNameShouldBePopulatedCorrectly()
        {
            By txtBookName = By.XPath(UnitDetailScreen.txtBookName);
            By txtUnitName = By.XPath(UnitDetailScreen.txtUnitName);

            wait.Until(drv => drv.FindElement(txtBookName));

            Assert.That(webHelper.GetElementText(txtBookName), Is.Not.Null.Or.Empty, "Book is empty!");
            Assert.That(webHelper.GetElementText(txtUnitName), Is.Not.Null.Or.Empty, "Unit is empty!");
        }

        [Then(@"the validation message for Content Name field should display")]
        public void ThenTheValidationMessageForContentNameFieldShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.messageContentNameRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the validation message for Page Number field should display")]
        public void ThenTheValidationMessageForPageNumberFieldShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.messagePageNumberRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the validation message for Youtube Link field should display")]
        public void ThenTheValidationMessageForYoutubeLinkFieldShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.messageYouTubeLinkRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [When(@"I edit the content '([^']*)'")]
        public void WhenIEditTheContent(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            driver.FindElement(element).Click();
        }

        [Then(@"content detail screen should display as below")]
        public void ThenContentDetailScreenShouldDisplayAsBelow(Table table)
        {
            var dataset = table.CreateSet<ContentDetail>();

            foreach (var data in dataset)
            {
                By txtContentName = By.XPath(UnitDetailScreen.txtContentName);
                By txtType = By.XPath(UnitDetailScreen.txtType);
                By txtPageNumber = By.XPath(UnitDetailScreen.txtPageNumber);
                By txtDescription = By.XPath(UnitDetailScreen.txtDescription);
                By imgPageThumbnail = By.XPath(UnitDetailScreen.imgPageThumbnail);

                wait.Until(drv => drv.FindElement(imgPageThumbnail));

                Assert.IsTrue(webHelper.IsElementPresent(txtContentName), "Name mismatch!");
                Assert.IsTrue(webHelper.IsElementPresent(txtType), "Type mismatch!");
                Assert.IsTrue(webHelper.IsElementPresent(txtPageNumber), "Page Number mismatch!");
                Assert.IsTrue(webHelper.IsElementPresent(txtDescription), "Description mismatch!");
                Assert.IsTrue(webHelper.IsElementPresent(imgPageThumbnail), "Image does not show!");
            }
        }

        [Then(@"the validation message for Page Image should display")]
        public void ThenTheValidationMessageForPageImageShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.messagePageImageRequired);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Validation message does not show!");
        }

        [Then(@"the Push & Listen configuration section should display")]
        public void ThenThePushListenConfigurationSectionShouldDisplay()
        {
            By titlePushAndListen = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.titlePushAndListen);
            By imgPushAndListen = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.imgPushAndListen);

            wait.Until(drv => drv.FindElement(imgPushAndListen));

            Assert.IsTrue(webHelper.IsElementPresent(titlePushAndListen), "Configuration section does not show!");
            Assert.IsTrue(webHelper.IsElementPresent(imgPushAndListen), "Image section does not show!");
            Assert.That(driver.FindElement(imgPushAndListen).GetAttribute("src"), Is.Not.Null.Or.Empty, "Image does not show!");
        }

        [When(@"I configure the Push & Listen on the image as below")]
        public void WhenIConfigureThePushListenOnTheImageAsBelow(Table table)
        {
            var width = driver.Manage().Window.Size.Width;
            var height = driver.Manage().Window.Size.Height;

            var x = width / 2 - 150;
            var y = height / 3;

            Thread.Sleep(3000);

            var dataset = table.CreateSet<PushAndListenDetail>();

            foreach (var data in dataset)
            {
                By imgPushAndListen = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.imgPushAndListen);
                By txtName = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.txtName);
                By txtAudio = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.txtAudio);
                By btnSave = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.btnSave);
                By messsageSaved = By.XPath(UnitDetailScreen.messsageSaved);

                x += 200;

                webHelper.ScrollToElement(imgPushAndListen);

                webHelper.DrawRectangle(x, y);

                wait.Until(drv => drv.FindElement(txtName));

                driver.FindElement(txtName).SendKeys(data.Name);
                driver.FindElement(txtAudio).SendKeys(data.Audio);
                driver.FindElement(btnSave).Click();

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                wait.Until(drv => drv.FindElements(messsageSaved).Count == 0);
            }
        }

        [Then(@"the grid configuration asset should display")]
        public void ThenTheGridConfigurationAssetShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.gridConfiguration);

            wait.Until(drv => drv.FindElement(element));

            Assert.That(driver.FindElements(element).Count(), Is.GreaterThan(0), "Grid configuration asset does not show!");
        }

        [Then(@"the bounding box should display on the image")]
        public void ThenTheBoundingBoxShouldDisplayOnTheImage()
        {
            By element = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.divBoundingBox);

            wait.Until(drv => drv.FindElement(element));

            Assert.That(driver.FindElements(element).Count, Is.GreaterThan(0), "Bounding box does not show!");
        }

        [When(@"I edit the configuration asset '([^']*)'")]
        public void WhenIEditTheConfigurationAsset(string input)
        {
            By menuOption = By.XPath("//td[text()='" + input + "']/following-sibling::*//*[@id='menu-button']");
            By optionEdit = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.optionEdit);

            driver.FindElement(menuOption).Click();
            driver.FindElement(optionEdit).Click();
        }

        [Then(@"the configuration popup should display")]
        public void ThenTheConfigurationPopupShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.txtName);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Popup does not show!");
        }

        [When(@"I change the configuration asset as below")]
        public void WhenIChangeTheConfigurationAssetAsBelow(Table table)
        {
            var dataset = table.CreateSet<PushAndListenDetail>();

            foreach (var data in dataset)
            {
                By txtName = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.txtName);
                By txtAudio = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.txtAudio);

                webHelper.ChangeElementText(txtName, data.Name);
                webHelper.ChangeElementText(txtAudio, data.Audio);
            }
        }

        [Then(@"the configuration asset '([^']*)' should display on the grid")]
        public void ThenTheConfigurationAssetShouldDisplayOnTheGrid(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "New configuration does not show!");
        }

        [When(@"I delete the configuration asset '([^']*)'")]
        public void WhenIDeleteTheConfigurationAsset(string input)
        {
            By menuOption = By.XPath("//td[text()='" + input + "']/following-sibling::*//*[@id='menu-button']");
            By optionDelete = By.XPath(UnitDetailScreen.PushAndListenSetupScreen.optionDelete);

            driver.FindElement(menuOption).Click();
            driver.FindElement(optionDelete).Click();
        }

        [Then(@"the configuration asset '([^']*)' should be removed")]
        public void ThenTheConfigurationAssetShouldBeRemoved(string input)
        {
            By element = By.XPath("//td[text()='" + input + "']");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            wait.Until(drv => drv.FindElements(element).Count == 0);

            Assert.IsFalse(webHelper.IsElementPresent(element), "Configuration still exists!");
        }

        [When(@"I click Export QR Code button")]
        public void WhenIClickExportQRCodeButton()
        {
            By element = By.XPath(UnitDetailScreen.btnExportQRCode);

            driver.FindElement(element).Click();
        }

        [Then(@"the zip file for the QR code should be downloaded")]
        public void ThenTheZipFileForTheQRCodeShouldBeDownloaded()
        {
            Thread.Sleep(5000);

            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            var fileName = $"{bookName}_{unitName.Replace(" ", "")}.zip";

            var foundFile = Directory.GetFiles(dir, "*.zip")
                .FirstOrDefault(file => file.Contains(fileName));

            if (!string.IsNullOrEmpty(foundFile))
            {
                Assert.IsTrue(foundFile.EndsWith(fileName));
                zipFilePath = foundFile;
            }
            else
            {
                throw new AssertionException("Failed to download QR code!");
            }
        }

        [Then(@"the Download Successfully message should display")]
        public void ThenTheDownloadSuccessfullyMessageShouldDisplay()
        {
            By element = By.XPath(UnitDetailScreen.messageDownloadSuccessfully);

            wait.Until(drv => drv.FindElement(element));

            Assert.IsTrue(webHelper.IsElementPresent(element), "Toast message does not show!");
        }

        [When(@"I extract the QR code zip file")]
        public void WhenIExtractTheQRCodeZipFile()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            extractedPath = dir + @"\extracted";

            try
            {
                Directory.CreateDirectory(extractedPath);

                ZipFile.ExtractToDirectory(zipFilePath, extractedPath);

                Console.WriteLine("Zip file extracted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            File.Delete(zipFilePath);
        }

        [Then(@"the list of QR code should display as below")]
        public void ThenTheListOfQRCodeShouldDisplayAsBelow(Table table)
        {
            var files = Directory.GetFiles(extractedPath, "*.png");
            var dataset = table.CreateSet<ContentDetail>();
            var fileDictionary = files.ToDictionary(file => Path.GetFileName(file));

            foreach (var data in dataset)
            {
                var type = data.Type.ToString().Contains("Push") ? "PushAndListen" : data.Type.ToString();
                var fileQRCode = $"{data.Name.Replace(" ", "")}_{type}.png";

                if (fileDictionary.TryGetValue(fileQRCode, out var file))
                {
                    Assert.IsTrue(file.EndsWith(fileQRCode));
                    File.Delete(file);
                    fileDictionary.Remove(fileQRCode);
                }
            }

            foreach (var file in fileDictionary.Values)
            {
                File.Delete(file);
            }

            Directory.Delete(extractedPath);
        }

    }
}
