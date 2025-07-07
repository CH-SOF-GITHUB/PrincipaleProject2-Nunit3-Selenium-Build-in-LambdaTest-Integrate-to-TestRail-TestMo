using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRailTesting.Base;

namespace TestRailTesting.trc_automated_tests
{
    [TestFixture(Author = "Chaker Ben Said", Description = "automate des tests aléatoires")]
    internal class Adhoc : BaseTest
    {
        // set the URL of demo site web for reCAPTCHA
        string demoURL = "https://www.demos.bellatrix.solutions/contact-form/";
        [Test(Description = "how User handle a ReCaptcha and click in Recaptcha checkbox and bypass the Recpatcha")]
        public void HandleRecaptchaClick()
        {
            IDriver.Navigate().GoToUrl("https://www.google.com/recaptcha/api2/demo");
            Thread.Sleep(3000);
            // get the api_key of recaptcha
            var data_sitekey = IDriver.FindElement(By.ClassName("g-recaptcha")).GetAttribute("data-sitekey");
            Console.WriteLine($"The recaptcha data-sitekey : {data_sitekey}");
            // Switch to reCAPTCHA iframe
            IWebElement repatchaIframe = IDriver.FindElement(By.TagName("iframe"));
            IDriver.SwitchTo().Frame(repatchaIframe);
            Console.WriteLine("User Switched to reCAPTCHA iframe.");
            // Click in sumbit button
            IWebElement recaptchaCheckbox = IDriver.FindElement(By.ClassName("recaptcha-checkbox-border"));
            recaptchaCheckbox.Click();
            Console.WriteLine("User clicked on reCAPTCHA checkbox.");
            Thread.Sleep(3000);
        }

        [Test(Description = "how to handle reCaptcha click in Bellatrix demo site")]
        public void Bellatrix_HandleRecaptchaClick()
        {
            IDriver.Navigate().GoToUrl(demoURL);
            Thread.Sleep(5000);
            // define the elements of Contatc Form
            IWebElement FirstNameField = IDriver.FindElement(By.CssSelector("#wpforms-3347-field_1"));
            FirstNameField.SendKeys("Name");
            IWebElement EmailField = IDriver.FindElement(By.CssSelector("#wpforms-3347-field_2"));
            EmailField.SendKeys("contactPurchase@yahoo.com");
            IWebElement AccessPassBronze = IDriver.FindElement(By.Id("wpforms-3347-field_3_1"));
            AccessPassBronze.Click();
            IWebElement AttendSession1 = IDriver.FindElement(By.Id("wpforms-3347-field_4_1"));
            AttendSession1.Click();
            IWebElement StayOverNightYes = IDriver.FindElement(By.Id("wpforms-3347-field_5_1"));
            StayOverNightYes.Click();
            // Switch to reCAPTCHA iframe
            IWebElement repatchaIframe = IDriver.FindElement(By.TagName("iframe"));
            IDriver.SwitchTo().Frame(repatchaIframe);
            Console.WriteLine("User Switched to reCAPTCHA iframe.");
            // Click in sumbit button
            IWebElement recaptchaCheckbox = IDriver.FindElement(By.ClassName("recaptcha-checkbox-border"));
            recaptchaCheckbox.Click();
            Console.WriteLine("User clicked on reCAPTCHA checkbox.");
            Thread.Sleep(5000);
            Console.WriteLine("User bypass the reCAPTCHA successfully.");
        }

        [Test(Description = "Verify the parameters of the test case.")]
        public void VerifyParameters()
        {
            string baseUrl = TestContext.Parameters["webAppUrl"];
            Console.WriteLine($"Base URL: {baseUrl}");
        }
    }
}
