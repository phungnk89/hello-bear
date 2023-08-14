using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.Security.Principal;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace HelloBear.Support
{
    [Binding]
    public sealed class Hooks
    {
        private static ExtentReports extentReports;
        private static ExtentTest featureNode;
        private static ExtentTest scenarioNode;
        private DatabaseHelper databaseHelper;
        private readonly ISpecFlowOutputHelper helper;
        private IWebDriver _driver;
        private IConfiguration _configuration;
        private string imagePath = string.Empty;

        public Hooks(ISpecFlowOutputHelper helper)
        {
            this.helper = helper;
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            databaseHelper = new DatabaseHelper() { configuration = _configuration };
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var baseDirectoryPath = AppContext.BaseDirectory;
            File.Delete(baseDirectoryPath + "\\TestExecution.json");
            var images = Directory.GetFiles(baseDirectoryPath, "*.png");

            foreach (var image in images)
            {
                File.Delete(image);
            }

            var reportPath = baseDirectoryPath + @"\Reports\";
            var htmlReportFolder = DateTime.Now.ToString("MM-dd-yyyy_hh-mm");

            extentReports = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(reportPath + "\\Reports_" + htmlReportFolder + "\\", ViewStyle.SPA);
            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = "Test Report";
            htmlReporter.AnalysisStrategy = AnalysisStrategy.BDD;
            extentReports.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extentReports.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            featureNode = extentReports.CreateTest<Feature>(context.FeatureInfo.Title);
            featureNode.Info(context.FeatureInfo.Description);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext context)
        {
            scenarioNode = featureNode.CreateNode<Scenario>(context.ScenarioInfo.Title);

            new DriverManager().SetUpDriver(new EdgeConfig());

            EdgeOptions options = new EdgeOptions { PageLoadStrategy = PageLoadStrategy.Normal };

            options.AddArguments("--window-size=1920,1080");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-web-security");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--inprivate");
            options.AddArguments("--disable-blink-features=AutomationControlled");
            options.AddArguments("disable-infobars");
            options.AddArguments("start-maximized");
            //options.EnableMobileEmulation("Galaxy S5");

            if (bool.Parse(_configuration["headless"]))
            {
                string userName = WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                string dir = string.Format(@"c:\Users\{0}\Downloads", userName);

                options.AddArguments("--headless");
                options.AddUserProfilePreference("download.default_directory", dir);
                options.AddUserProfilePreference("download.prompt_for_download", true);
                options.AddUserProfilePreference("download.directory_upgrade", true);
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("safebrowsing.enabled", false);
                options.AddUserProfilePreference("safebrowsing.disable_download_protection", true);
                options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", true);
            }

            _driver = new EdgeDriver(EdgeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(30));

            context["driver"] = _driver;
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext context)
        {
            imagePath = string.Empty;
            _driver.Quit();
            databaseHelper.CleanUp();
            context.Remove("driver");
        }

        [BeforeStep]
        public void BeforeStep()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
                .Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {
            var stepType = context.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepTitle = context.StepContext.StepInfo.Text;
            var table = context.StepContext.StepInfo.Table;
            ExtentTest stepNode;
            var webHelper = new WebHelper() { driver = _driver };
            var azureHelper = new AzureBlobHelper();

            switch (stepType)
            {
                case "Given":
                    stepNode = scenarioNode.CreateNode<Given>(stepTitle);
                    break;
                case "When":
                    stepNode = scenarioNode.CreateNode<When>(stepTitle);
                    break;
                case "Then":
                    stepNode = scenarioNode.CreateNode<Then>(stepTitle);
                    break;
                default:
                    stepNode = scenarioNode.CreateNode<And>(stepTitle);
                    break;
            }

            if (context.TestError != null)
            {
                webHelper.CaptureScreenshotAndSave(context);

                imagePath = azureHelper.GetBlobgUrl(context);

                if (!string.IsNullOrEmpty(imagePath))
                {
                    helper.AddAttachment(imagePath);
                }

                var screenshot = webHelper.CaptureScreenShot();
                var imageBuilder = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build();

                stepNode.Fail(context.TestError.Message, imageBuilder);
            }

            if (table != null)
            {
                var headerRow = table.Rows.First();
                var headers = headerRow.Keys.ToList();

                stepNode.Info(string.Join('|', headers));
                foreach (var row in table.Rows)
                {
                    var data = row.Values.ToList();
                    stepNode.Info(string.Join('|', data));
                }
            }
        }
    }
}