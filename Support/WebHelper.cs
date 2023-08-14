using HelloBear.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Text;
using TechTalk.SpecFlow;

namespace HelloBear.Support
{
    public class WebHelper
    {
        public IWebDriver driver { get; set; }

        /// <summary>
        /// IsElementPresent
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// CaptureScreenshotAndSave
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void CaptureScreenshotAndSave(ScenarioContext context)
        {
            var baseDirectoryPath = AppContext.BaseDirectory;
            var path = string.Format(@"{0}{1}.png", baseDirectoryPath, context.ScenarioInfo.Title.Replace(" ", ""));
            var azureHelper = new AzureBlobHelper();

            try
            {
                driver.TakeScreenshot().SaveAsFile(path, ScreenshotImageFormat.Png);

                azureHelper.UploadImageToAzure(path);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// CaptureScreenShot
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string CaptureScreenShot()
        {
            try
            {
                return driver.TakeScreenshot().AsBase64EncodedString;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// ClearElementText
        /// </summary>
        /// <param name="by"></param>
        public void ClearElementText(By by)
        {
            IWebElement element = driver.FindElement(by);

            element.SendKeys(Keys.Control + "A");
            element.SendKeys(Keys.Backspace);
        }

        /// <summary>
        /// GenerateEmail
        /// </summary>
        /// <returns>random email</returns>
        public string GenerateEmail()
        {
            Random random = new Random();
            StringBuilder randomString = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                char randomCharacter = (char)(random.Next('a', 'z') + 1);
                randomString.Append(randomCharacter);
            }

            return string.Format("HB{0}@yopmail.com", randomString.ToString());
        }

        /// <summary>
        /// ValidateElementText
        /// </summary>
        /// <param name="by"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public bool ValidateElementText(By by, string expected)
        {
            return GetElementText(by).Contains(expected);
        }

        /// <summary>
        /// GetElementText
        /// </summary>
        /// <param name="by"></param>
        /// <returns>element text</returns>
        public string GetElementText(By by)
        {
            var element = driver.FindElement(by);

            if (!string.IsNullOrEmpty(element.Text)) return element.Text;

            if (!string.IsNullOrEmpty(element.GetAttribute("value"))) return element.GetAttribute("value");

            return string.Empty;
        }

        /// <summary>
        /// ClickByJavascript
        /// </summary>
        /// <param name="by"></param>
        public void ClickByJavascript(By by)
        {
            var element = driver.FindElement(by);

            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;

            executor.ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// GetMaterialPath
        /// </summary>
        /// <param name="name"></param>
        /// <returns>material full path</returns>
        public string GetMaterialPath(string name)
        {
            var materialPath = AppContext.BaseDirectory + "Materials";
            var files = Directory.GetFiles(materialPath, name);

            return files[0];
        }

        /// <summary>
        /// ClearMailbox
        /// </summary>
        public void ClearMailbox()
        {
            By btnDeleteAll = By.XPath(YOPMailScreen.btnDeleteAll);
            By messageDelete = By.XPath(YOPMailScreen.messageDelete);

            driver.SwitchTo().DefaultContent();
            ClickByJavascript(btnDeleteAll);
            var alert = driver.SwitchTo().Alert();
            alert.Accept();

            driver.SwitchTo().DefaultContent();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(messageDelete));
        }

        /// <summary>
        /// RandomPageImage
        /// </summary>
        /// <returns></returns>
        public string RandomPageImage()
        {
            var materialPath = AppContext.BaseDirectory + "Materials";
            var files = Directory.GetFiles(materialPath);
            var index = new Random().Next(0, files.Length - 1);

            return files[index];
        }

        /// <summary>
        /// ChangeElementText
        /// </summary>
        /// <param name="by"></param>
        /// <param name="input"></param>
        public void ChangeElementText(By by, string input)
        {
            if (GetElementText(by).Contains(input)) return;

            ClearElementText(by);

            driver.FindElement(by).SendKeys(input);
        }

        /// <summary>
        /// ScrollToElement
        /// </summary>
        /// <param name="by"></param>
        public void ScrollToElement(By by)
        {
            var element = driver.FindElement(by);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        /// <summary>
        /// DrawRectangle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawRectangle(int x, int y)
        {
            var act = new Actions(driver);

            act.MoveByOffset(x, y)
                .ClickAndHold()
                .MoveByOffset(100, 100)
                .Release()
                .MoveByOffset(-100, -100)
                .MoveByOffset(-x, -y)
                .Build()
                .Perform();
        }
    }
}
