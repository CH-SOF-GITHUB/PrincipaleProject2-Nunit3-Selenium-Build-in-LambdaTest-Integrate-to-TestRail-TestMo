using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TestRailTesting.Page_Object_Model;

namespace TestRailTesting.Base
{
    internal class BaseTest
    {
        // define how  to get Name of Test Methods
        private TestContext TestContext { get; set; }
        // define a new WebDriver variable
        protected IWebDriver IDriver;

        // Declare an instance of classe register-page to use its methods and properties
        protected RegisterPage registerPage;
        // Declare an instance of class YopMail to use its methods and properties
        protected YopMail yopMailPage;

        /*
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setting up the test environment...");
            // Update your lambdatest credentials
            int randomId = new Random().Next(1, 200);
            string TestName = TestContext.CurrentContext.Test.Name;
            ChromeOptions capabilities = new ChromeOptions();
            capabilities.BrowserVersion = "137.0.7151.120";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>();
            ltOptions.Add("username", "chakerbensaid1");
            ltOptions.Add("accessKey", "fpv3VyRpnh2XPvfT8nRggyloxttB1tCR51xpi4L4t4gfrv8lGp");
            ltOptions.Add("platformName", "Windows 10");
            ltOptions.Add("project", "Demo Nunit LT");
            ltOptions.Add("network", true);
            ltOptions.Add("visual", true);
            ltOptions.Add("console", true);
            ltOptions.Add("terminal", true);
            ltOptions.Add("devicelog", true);
            ltOptions.Add("build", $"TestMo-LambdaTest-Selenium-C#-build-{randomId}-Test-{TestName}");
            ltOptions.Add("w3c", true);
            ltOptions.Add("plugin", "c#-nunit");
            capabilities.AddAdditionalOption("LT:Options", ltOptions);
            IDriver = new RemoteWebDriver(new Uri("https://hub.lambdatest.com/wd/hub/"), capabilities);
            IDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10); // Set page load timeout
            IDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Console.WriteLine("Selenium webdriver initialization can be done here !");
            Console.WriteLine($"Capabilities parameters {capabilities.BrowserName} : {capabilities.BrowserVersion} : {OSPlatform.Windows}");
            // Initialize the RegisterPage instance with the current driver
            registerPage = new RegisterPage(IDriver);
            // Initialize the YopMail instance with the current driver
            yopMailPage = new YopMail(IDriver);
        }*/


        [SetUp]
        public void Setup()
        {
            IDriver = new ChromeDriver();
            IDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(25); // Set page load timeout
            IDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            IDriver.Manage().Window.Maximize(); // Maximize the browser window
            // Initialize the RegisterPage instance with the current driver
            registerPage = new RegisterPage(IDriver);
            // Initialize the YopMail instance with the current driver
            yopMailPage = new YopMail(IDriver);
        }

        public void TakeResult_screenshot()
        {
            if (IDriver == null)
            {
                Console.WriteLine("WebDriver is not initialized. Cannot take screenshot.");
                return;
            }
            var takeScreenshot = (ITakesScreenshot)IDriver;
            var filepath = $"C:\\Users\\chaker\\source\\repos\\TestRail\\TestRailTesting\\ScreenshotsPassed\\{TestContext.CurrentContext.Test.Name}--{DateTime.Now:yyyyMMdd_HHmmss}.png";
            try
            {
                takeScreenshot.GetScreenshot().SaveAsFile(filepath);
                TestContext.AddTestAttachment(filepath, "Screenshot for passed test case");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la prise de la capture d'écran : {ex.Message}");
            }
        }

        public void Take_screen_shot_for_failed_case(string ScreenShotOutputPath)
        {
            var RuningCaseName = TestContext.CurrentContext.Test.FullName;
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
            {
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(ScreenShotOutputPath);
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }

                    var shortCaseName = RuningCaseName.Substring(RuningCaseName.LastIndexOf('.') + 1);

                    var screenshot = ((ITakesScreenshot)IDriver).GetScreenshot();
                    var screenshotDirectoryPath = Path.Combine(dirInfo.FullName);
                    if (!Directory.Exists(screenshotDirectoryPath))
                    {
                        Directory.CreateDirectory(screenshotDirectoryPath);
                    }
                    var existingFile = dirInfo.GetFiles(shortCaseName + "_*.png", SearchOption.TopDirectoryOnly);
                    string filePath = Path.Combine(dirInfo.FullName, string.Format("{0}_[{1}].png", shortCaseName, existingFile.Length + 1));
                    screenshot.SaveAsFile(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /*
        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("Tearing down the test environment...");
            bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            try
            {
                // Log the result to LambdaTest
                ((IJavaScriptExecutor)IDriver).ExecuteScript("lambda-status=" + (passed ? "passed" : "failed"));
                Take_screen_shot_for_failed_case("C:\\Users\\chaker\\source\\repos\\TestRail\\TestRailTesting\\ScreenshotsFailure\\");
            }
            finally
            {
                if (IDriver != null)
                {
                    IDriver.Quit();
                    IDriver.Dispose();
                }
                Console.WriteLine("Selenium webdriver quit !");
            }
        }
        */
        // Teardown 2 method
        [TearDown]
        public void Teardown()
        {
            if (IDriver != null)
            {
                IDriver.Quit();
                IDriver.Dispose();
                Console.WriteLine("Selenium webdriver quit !");
            }
            else
            {
                Console.WriteLine("WebDriver is not initialized. Cannot quit.");
            }
        }
    }
}
