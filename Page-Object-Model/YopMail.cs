using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace TestRailTesting.Page_Object_Model
{
    internal class YopMail
    {
        // declare a new WebDriver variable
        private readonly IWebDriver driver;

        // Define the URL for YopMail
        private readonly string yopMailURL = "https://yopmail.com/fr/";
        // POPup 
        //private readonly By iframeConsent = By.CssSelector("iframe#ifmail");
        //private readonly By consentButton = By.XPath("//button[text()='Consent']");
        protected readonly By consentButtonToClose = By.XPath("//div[@class='fc-consent-root']//button[@aria-label='Consent']//p[@class='fc-button-label' and text()='Consent']");
        //
        // declare a web elements variables
        private readonly By emailYopMailInput = By.Id("login");
        // declare a web element describe the email og registration in the inbox
        private readonly By inboxLabel = By.ClassName("bname");
        // declare the first iframe element
        private readonly By iframeInbox = By.Name("ifinbox");
        //protected readonly By bodyInbox = By.ClassName("bodyinbox");
        // declare the second iframe element
        // recaptcha appears and must be closed to access the inbox
        private readonly By iframeRecaptcha = By.TagName("iframe");

        public YopMail(IWebDriver IDriver)
        {
            this.driver = IDriver;
        }

        // set the methods to verify the email exists in the inbox
        public void OpenYopMailPage()
        {
            driver.Navigate().GoToUrl(yopMailURL);
            Console.WriteLine("Step : Open the YopMail page using the URL: " + yopMailURL);
        }

        /*public void CloseConsentPopup()
        {
            // Switch to the iframe containing the consent popup
            driver.SwitchTo().Frame(driver.FindElement(iframeConsent));
            // Click the consent button
            driver.FindElement(consentButton).Click();
            // Switch back to the default content
            driver.SwitchTo().DefaultContent();
            Console.WriteLine("Step : Close the consent popup on YopMail page.");
        }*/

        public void ClickConsentButton()
        {
            driver.FindElement(consentButtonToClose).Click();
            Console.WriteLine("Step : Click the consent button to close the popup on YopMail page.");
        }

        public void EnterEmailOfregistration(string email)
        {
            driver.FindElement(emailYopMailInput).SendKeys(email + Keys.Enter);
            Console.WriteLine("Step : Enter the email of registration in the YopMail: " + email);
        }

        public string GetInboxLabel()
        {
            return driver.FindElement(inboxLabel).Text;
        }

        public void GoToIframe()
        {
            IWebElement iframe = driver.FindElement(iframeInbox);
            driver.SwitchTo().Frame(iframe);
            driver.SwitchTo().DefaultContent(); // Switch back to the default content after switching to the iframe
            //Assert.IsTrue(driver.PageSource.Contains("Bienvenue "+ prenom), "The inbox is not displayed correctly.");
        }

        /*public void CloseCaptcha()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Attendre que l'iframe soit présente et visible
            IWebElement captchaFrame = wait.Until(ExpectedConditions.ElementIsVisible(iframeRecaptcha));
            driver.SwitchTo().Frame(captchaFrame);
            Console.WriteLine("Step : Switch to the reCAPTCHA iframe.");
            // Attendre que le bouton soit cliquable
            IWebElement closeButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#recaptcha-anchor")));
            closeButton.Click();
            // Revenir au contexte principal
            driver.SwitchTo().DefaultContent();
        }*/

        public void CloseCaptcha()
        {
            IWebElement captchaFrame = driver.FindElement(iframeRecaptcha);
            driver.SwitchTo().Frame(captchaFrame);       
            Console.WriteLine("Step : Switch to the reCAPTCHA iframe.");
            // Wait for the captcha to load and then close it
            IWebElement iFrame_checkbox = driver.FindElement(By.ClassName("recaptcha-checkbox-border"));
            iFrame_checkbox.Click();
        }
    }
}
