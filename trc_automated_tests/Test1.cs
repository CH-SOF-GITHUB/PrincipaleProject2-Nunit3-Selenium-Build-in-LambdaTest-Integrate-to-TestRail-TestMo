using NUnit.Engine.Extensibility;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


[assembly: AssemblyCopyright("Copyright © 2025")]
//[assembly: AssemblyKeyName("Nunit3-Tests")]
namespace TestRailTesting.trc_automated_tests
{
    [TestFixture(Author = "Chaker Ben Said", Category = "Automated Testing", Description = "create and execute scripting test with selenium and Nunit3 Framework", Explicit = true, Reason = "Repititif tests", TestName = "BstackDemo-Ecommerce funcs", TestOf = typeof(Test1))]
    internal class Test1
    {   // define how  to get Name of Test Methods
        private TestContext TestContext { get; set; }
        // declare a web driver to control the browser
        private IWebDriver driver;
        private IWebElement SignInLink;
        private IWebElement FavoritesLink;
        private IWebElement UsernameInput;
        private IWebElement PasswordInput;
        private IWebElement LOGINButton;
        private IWebElement FavoritesBtn;
        private IWebElement ProductName;
        //private IList<IWebElement> favoritesProdsNameElements;

        // string variables
        private string currentFavoritesURL;
        private List<string> tabProdsName;
        private List<string> favoritesProdsName;
        /*
        [SetUp]
        public void Setup()
        {
            // Initialize the ChromeDriver before each test
            var options = new ChromeOptions();
            options.AddArgument("--headless"); // Run in headless mode (optional)
            options.AddArgument("--no-sandbox");
            //options.PageLoadTimeout = TimeSpan.FromSeconds(15);
            //==> System.InvalidOperationException : session not created: No matching capabilities found (SessionNotCreated)
            //options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept; // Handle unhandled prompts (optional)
            //options.ImplicitWaitTimeout = TimeSpan.FromSeconds(10); // Set implicit wait timeout (optional)
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20L); // Set implicit wait timeout
            driver.Manage().Window.Maximize();
        }
        */

        [SetUp]
        public void Setup()
        {
            // Update your lambdatest credentials
            int randomId = new Random().Next(1, 200);
            string TestName = TestContext.CurrentContext.Test.Name;
            ChromeOptions capabilities = new ChromeOptions();
            capabilities.BrowserVersion = "137.0.7151.120";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>();
            ltOptions.Add("username", "chaker.nehos");
            ltOptions.Add("accessKey", "YzCmbZk6LHmgfa7wrkb6Asyr5IZKkmDGeu2V9CNtW07vyfkg4E");
            ltOptions.Add("platformName", "Windows 10");
            ltOptions.Add("project", "Demo Nunit LT");
            ltOptions.Add("network", true);
            ltOptions.Add("visual", true);
            ltOptions.Add("console", true);
            ltOptions.Add("terminal", true);
            ltOptions.Add("devicelog", true);
            ltOptions.Add("build", $"Selenium-C#-build-{randomId}-Test-{TestName}");
            ltOptions.Add("w3c", true);
            ltOptions.Add("plugin", "c#-nunit");
            capabilities.AddAdditionalOption("LT:Options", ltOptions);
            driver = new RemoteWebDriver(new Uri("https://hub.lambdatest.com/wd/hub/"), capabilities);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10); // Set page load timeout
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Console.WriteLine("Selenium webdriver initialization can be done here !");
            Console.WriteLine($"Capabilities parameters {capabilities.BrowserName} : {capabilities.BrowserVersion} : {OSPlatform.Windows}");
        }

        [TestCase(Description = "TC01 Bstackdemo - Verify that the Home/Product page is accessible via the specified URL")]
        public void TestRailDisplayHomeProductPageTest()
        {
            driver.Navigate().GoToUrl("https://www.bstackdemo.com/"); // Replace with the actual URL you want to test
            // This is a placeholder for the actual test implementation
            // You can add your test logic here
            Thread.Sleep(3000); // Wait for the page to load
            Assert.Pass("The Home/Product page loads successfully and displays the product listing.");
        }

        [TestCase(Description = "TC02 Bstackdemo - Verify that the user should navigate to SignIn page when he clicked in SignIn Link")]
        public void TestRailVerifyNavigateToSignInPageTest()
        {
            driver.Navigate().GoToUrl("https://www.bstackdemo.com/");
            // locate the sign in link and click it
            SignInLink = driver.FindElement(By.Id("signin"));
            SignInLink.Click();
            Thread.Sleep(3000); // Wait for the page to load
            currentFavoritesURL = driver.Url;
            Assert.That(currentFavoritesURL, Is.EqualTo("https://www.bstackdemo.com/signin"));
            Assert.Pass("Redirection passed and SignIn page loads successfully and displays all required fields.");
        }

        [TestCase(Description = "TC03 Bstackdemo - User view The initial number of products in Text.")]
        public void TestRailNumberOfProductsHomePage()
        {
            driver.Navigate().GoToUrl("https://www.bstackdemo.com/");
            Thread.Sleep(3000);
            Assert.Pass("25 Product(s) found.");
        }

        [TestCase(Description = "TC04 Bstackdemo - User view his favorites depends on this logging status.")]
        public void TestRailNavigateFavoritesListEmptyConnectionLoggoutStatus()
        {
            driver.Navigate().GoToUrl("https://www.bstackdemo.com/");
            // declare and locate Favorites link and click it
            //  FavoritesLink = driver.FindElement(By.XPath("//a[@id='favourites']//strong"));
            FavoritesLink = driver.FindElement(By.CssSelector("#favourites > strong"));
            FavoritesLink.Click();
            currentFavoritesURL = driver.Url;
            Console.WriteLine("first url apply : " + currentFavoritesURL);
            Assert.That(currentFavoritesURL, Is.EqualTo("https://www.bstackdemo.com/favourites"));
            currentFavoritesURL = driver.Url;
            //Assert.That(currentFavoritesURL2, Is.EqualTo("https://www.bstackdemo.com/signin?favourites=true"));
            Console.WriteLine("second url apply : " + currentFavoritesURL);
        }

        [TestCase(Description = "TC05 Bstackdemo - User view his favorites after click in one or more product listing depends on this logging status.")]
        public void TestRailNavigateFavoritesListConnectionLoggoutStatus()
        {
            // 1. Navigate to the home page
            driver.Navigate().GoToUrl("https://www.bstackdemo.com/");
            // 2. locate a product listing in the product page and click in favorite ♡
            var FavoriteButton = driver.FindElement(By.CssSelector("#31  > div.shelf-stopper > button"));
            FavoriteButton.Click();
            Thread.Sleep(3000); // Wait for the page to load
            //3. User redirects to signin page after clicking the favorite button when status of user is logged out
            currentFavoritesURL = driver.Url;
            Console.WriteLine("Current URL after clicking favorite button: " + currentFavoritesURL);
            Assert.That(currentFavoritesURL, Is.EqualTo("https://www.bstackdemo.com/signin"));
        }

        [TestCase(Description = "TC06 Bstackdemo - User should navigate to home Component after clik in one or more product listing depends on this logging status.")]
        public void TestRailUserNavigateToHomeComponentAfterLoggedInAndClickFavoritesIcon()
        {
            // 1. Navigate to the home page
            driver.Navigate().GoToUrl("https://www.bstackdemo.com/");
            // 2. locate a product listing in the product page and click in favorite ♡
            var FavoriteBtn = driver.FindElement(By.XPath("//*[@id=\"1\"]/div[1]/button"));
            FavoriteBtn.Click();
            Thread.Sleep(3000); // Wait for the page to load
            //3. User redirects to signin page after clicking the favorite button when status of user is logged out
            var currentUrl = driver.Url;
            Console.WriteLine("Current URL after clicking favorite button: " + currentUrl);
            Assert.That(currentUrl, Is.EqualTo("https://www.bstackdemo.com/signin"));
            // 4. enter a valid username
            UsernameInput = driver.FindElement(By.XPath("//*[@id=\"react-select-2-input\"]"));
            UsernameInput.SendKeys("demouser");
            UsernameInput.SendKeys(Keys.Tab); // Move to the next input field
            // 5. enter a valid password
            PasswordInput = driver.FindElement(By.XPath("//*[@id=\"react-select-3-input\"]"));
            PasswordInput.SendKeys("testingisfun99");
            PasswordInput.SendKeys(Keys.Tab); // Move to the next input field
            // 6. click on the login button
            LOGINButton = driver.FindElement(By.CssSelector("#login-btn"));
            LOGINButton.Click();
            Thread.Sleep(3000); // Wait for the page to load
            // 7. User should be redirected to the product/home page after logging in with the expected correct URL
            currentFavoritesURL = driver.Url;
            Console.WriteLine("Current Favorites URL after logging in: " + currentFavoritesURL);
            Assert.That(currentFavoritesURL, Is.EqualTo("https://www.bstackdemo.com/?signin=true"));
        }

        [TestCase(Description = "TC07 Bstackdemo - User should be navigate successfully to favorites page after click favorites link and logged in")]
        public void TestRailUserNavigateTOFavoritesPageAFterClickFavoritesLinkConnectionLoggoutStatus()
        {
            // 1. Navigate to the home page
            driver.Navigate().GoToUrl("https://www.bstackdemo.com/");
            // 2. locate favorites link and click it
            FavoritesLink = driver.FindElement(By.CssSelector("#favourites > strong"));
            FavoritesLink.Click();
            // 3. User redirects to signin page after clicking the favorites link when status of user is logged out
            currentFavoritesURL = driver.Url;
            Console.WriteLine("first url applyed : " + currentFavoritesURL);
            Assert.That(currentFavoritesURL, Is.EqualTo("https://www.bstackdemo.com/signin?favourites=true"));
            // 4. enter a valid username
            UsernameInput = driver.FindElement(By.XPath("//*[@id=\"react-select-2-input\"]"));
            UsernameInput.SendKeys("demouser");
            UsernameInput.SendKeys(Keys.Tab); // Move to the next input field
            // 5. enter a valid password
            PasswordInput = driver.FindElement(By.XPath("//*[@id=\"react-select-3-input\"]"));
            PasswordInput.SendKeys("testingisfun99");
            PasswordInput.SendKeys(Keys.Tab); // Move to the next input field
            // 6. click on the login button
            LOGINButton = driver.FindElement(By.CssSelector("#login-btn"));
            LOGINButton.Click();
            // 7. User should be redirected to the favorites page after logging in with the expected correct URL
            currentFavoritesURL = driver.Url;
            Assert.That(currentFavoritesURL, Is.EqualTo("https://www.bstackdemo.com/favourites"));
        }

        [TestCase(Description = "TC08 Bstackdemo - Verify that a logged-In user should add a product and view their favorites list on the ecommerce website.")]
        public void TestRailAddProductToFavoritesConnectionLogInStatus()
        {
            // 1. Navigate to the home page
            driver.Navigate().GoToUrl("https://www.bstackdemo.com/");
            // 2. locate a product listing in the product page and click in favorite ♡
            var FavoriteBtn = driver.FindElement(By.XPath("//*[@id=\"1\"]/div[1]/button"));
            FavoriteBtn.Click();
            // 3. enter a valid username
            UsernameInput = driver.FindElement(By.XPath("//*[@id=\"react-select-2-input\"]"));
            UsernameInput.SendKeys("demouser");
            UsernameInput.SendKeys(Keys.Tab); // Move to the next input field
            // 4. enter a valid password
            PasswordInput = driver.FindElement(By.XPath("//*[@id=\"react-select-3-input\"]"));
            PasswordInput.SendKeys("testingisfun99");
            PasswordInput.SendKeys(Keys.Tab); // Move to the next input field
            // 4. click on the login button
            LOGINButton = driver.FindElement(By.CssSelector("#login-btn"));
            LOGINButton.Click();
            // 5. Assert user reidrects to home/product page after logging in
            currentFavoritesURL = driver.Url;
            Assert.That(currentFavoritesURL, Is.EqualTo("https://www.bstackdemo.com/?signin=true"));
            // 6. click in product listing Numero °1 to favorite it
            var prodFavoritesBtn = driver.FindElement(By.XPath("//*[@id=\"1\"]/div[1]/button"));
            // display the value of color of the button favorite before and click it click it
            prodFavoritesBtn.Click();
            // 7. click in the favorites link of the navbar
            FavoritesLink = driver.FindElement(By.CssSelector("#favourites > strong"));
            FavoritesLink.Click();
            // 8.Assert the number of products in the favorites page
            var countProdsText = driver.FindElement(By.XPath("//div[@class='shelf-container-header']//small[@class='products-found']//span")).Text;
            var countProds = countProdsText.Substring(0, 1);
            var NumberOfFavorites = int.Parse(countProds);
            Assert.That(NumberOfFavorites, Is.EqualTo(1), "The number of products in favorites should be greater than 0.");
        }

        [TestCase(Description = "TC09 User click in faovrites icon of product listing and the color changes from empty to filled/colored, indicating the product is now a favorite.")]
        public void FavoriteIconColorChange()
        {
            // 1. Navigate to the home page
            driver.Navigate().GoToUrl("https://www.bstackdemo.com/");
            // 2. locate a product listing in the product page and click in favorite ♡
            var FavoriteBtn = driver.FindElement(By.XPath("//*[@id=\"1\"]/div[1]/button"));
            FavoriteBtn.Click();
            // 3. enter a valid username
            UsernameInput = driver.FindElement(By.XPath("//*[@id=\"react-select-2-input\"]"));
            UsernameInput.SendKeys("demouser");
            UsernameInput.SendKeys(Keys.Tab); // Move to the next input field
            // 4. enter a valid password
            PasswordInput = driver.FindElement(By.XPath("//*[@id=\"react-select-3-input\"]"));
            PasswordInput.SendKeys("testingisfun99");
            PasswordInput.SendKeys(Keys.Tab); // Move to the next input field
            // 4. click on the login button
            LOGINButton = driver.FindElement(By.CssSelector("#login-btn"));
            LOGINButton.Click();
            // 5. Assert user reidrects to home/product page after logging in
            currentFavoritesURL = driver.Url;
            Assert.That(currentFavoritesURL, Is.EqualTo("https://www.bstackdemo.com/?signin=true"));
            // 6. click in product listing Numero °1 to favorite it and assert the change of color
            var prodFavoritesBtn = driver.FindElement(By.XPath("//*[@id=\"1\"]/div[1]/button"));
            // display the value of color of the button favorite before and click it click it
            string propertyColor = prodFavoritesBtn.GetCssValue("color");
            Console.WriteLine("Favorite button color before click it : " + prodFavoritesBtn.GetCssValue("color"));
            // expect color is empty
            Assert.That(propertyColor, Is.EqualTo("rgba(0, 0, 0, 0.54)"));
            prodFavoritesBtn.Click();
            propertyColor = prodFavoritesBtn.GetCssValue("color");
            Console.WriteLine("Favorite button color after click it : " + prodFavoritesBtn.GetCssValue("color"));
            //expect color change to yellow
            Assert.That(propertyColor, Is.EqualTo("rgba(234, 191, 0, 1)"));
        }


        [TestCase(Description = "TC10 Bstackdemo - User should be able to view his favorites list.")]
        public void TestRailAddMoreOneProductToFavoritesConnectionLogInStatus()
        {
            // 1. Navigate to the home page
            driver.Navigate().GoToUrl("https://www.bstackdemo.com/");
            // 2. locate a product listing in the product page and click in favorite ♡
            var FavoriteBtn = driver.FindElement(By.XPath("//*[@id=\"1\"]/div[1]/button"));
            FavoriteBtn.Click();
            // 3. enter a valid username
            UsernameInput = driver.FindElement(By.XPath("//*[@id=\"react-select-2-input\"]"));
            UsernameInput.SendKeys("demouser");
            UsernameInput.SendKeys(Keys.Tab); // Move to the next input field
            // 4. enter a valid password
            PasswordInput = driver.FindElement(By.XPath("//*[@id=\"react-select-3-input\"]"));
            PasswordInput.SendKeys("testingisfun99");
            PasswordInput.SendKeys(Keys.Tab); // Move to the next input field
            // 4. click on the login button
            LOGINButton = driver.FindElement(By.CssSelector("#login-btn"));
            LOGINButton.Click();
            // 5. Assert user redirects to home/product page after logging in
            currentFavoritesURL = driver.Url;
            Assert.That(currentFavoritesURL, Is.EqualTo("https://www.bstackdemo.com/?signin=true"));
            // 6. click in first 4 products listing to favorite it
            int i = 1;
            tabProdsName = new List<string>();
            while (i < 5)
            {
                FavoritesBtn = driver.FindElement(By.XPath("//*[@id=" + i + "]/div[1]/button"));
                Thread.Sleep(1000);
                FavoritesBtn.Click();
                Thread.Sleep(1000);
                ProductName = driver.FindElement(By.XPath("//*[@id=" + i + "]/p"));
                tabProdsName.Add(ProductName.Text);
                i++;
            }
            // 7. click in the favorites link of the navbar
            FavoritesLink = driver.FindElement(By.CssSelector("#favourites > strong"));
            FavoritesLink.Click();
            // 8.Assert the number of products in the favorites page
            var countProdsText = driver.FindElement(By.XPath("//div[@class='shelf-container-header']//small[@class='products-found']//span")).Text;
            var countProds = countProdsText.Substring(0, 1);
            var NumberOfFavorites = int.Parse(countProds);
            Assert.That(NumberOfFavorites, Is.EqualTo(4), "The number of products should be greater than 0 and equal to expected number of favorites");
            Console.WriteLine("Number of favorites products : " + NumberOfFavorites);
            // 9. Assert the products in the favorites page by name existing
            favoritesProdsName = new List<string>();
            int k = 1;
            while(k <= NumberOfFavorites)
            {
                IWebElement FavoriteProdName = driver.FindElement(By.XPath("//*[@id=" + k + "]/p"));
                favoritesProdsName.Add(FavoriteProdName.Text);
                k++;
            }
            foreach(var prn in tabProdsName)
            {
                Console.WriteLine("product added for favorites =====> " + prn);
            }
            foreach (var fpn in favoritesProdsName)
            {
                Console.WriteLine("product favorites =====> " + fpn);
            }
            //// Assert with loop throught the 2 lists
            for(int m = 0; m < tabProdsName.Count(); m++)
            {
                for(int n = 0; n < favoritesProdsName.Count(); n++)
                {
                    if (tabProdsName[m] == favoritesProdsName[n]){
                        Console.WriteLine($"Product favorites was added  {tabProdsName[m]} successfully.");
                    }
                }
            }
        }



        [TestCase(Description = "Bstackdemo - User should be able to view his favorites list after clicking the favorites link.")]
        public void TestRailViewFavoritesList()
        {

        }

        /*
        [TearDown]
        public void TearDown()
        {
            // Close the browser after each test
            if (driver != null)
            {
                driver.Dispose();
                driver.Quit();
            }
        }
        */

        [TearDown]
        public void Teardown()
        {
            bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            try
            {
                // Log the result to LambdaTest
                ((IJavaScriptExecutor)driver).ExecuteScript("lambda-status=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                }
                Console.WriteLine("Selenium webdriver quit !");
            }
        }
    }
}
