using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRailTesting.Page_Object_Model
{
    internal class RegisterPage
    {
        private readonly IWebDriver driver;
        private readonly string registerURL = "https://www.appvizer.fr/inscription";
        private readonly By prénomField = By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(1) > input");
        private readonly By nomField = By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(2) > input");
        private readonly By emailField = By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(3) > input");
        private readonly By posteField = By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(4) > input");
        private readonly By entrepriseField = By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(5) > input");
        private readonly By checkboxNewsletter = By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(6) > span > input");
        private readonly By continuerButton = By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div.submit-wrap.text-center > button");

        // This element should be display after the registration form is submitted successfully
        // "/html/body/ui-view/div/div/div[2]/div/div/div/div[2]/div/div[3]"
        private readonly By successMsgAfterRegister = By.ClassName("registration-message");
        private bool isDisplayed = false;

        // This property class is used to check when the email is invalid
        private readonly By ngInvalidEmail = By.ClassName("ng-invalid-email");
        private bool isNgInvalidEmailDisplayed = false;


        // Constructor to initialize the driver and web elements
        public RegisterPage(IWebDriver IDriver)
        {
            this.driver = IDriver;
        }

        /* Methods to interact with specific web elements can be used by several tests without duplication*/
        public void OpenTheRegisterPage()
        {
            driver.Navigate().GoToUrl(registerURL);
            Console.WriteLine("Step : Open the registration form page using the URL: " + registerURL);
        }

        public void EnterPrénomValue(string prenom)
        {
            driver.FindElement(prénomField).SendKeys(prenom);
            Console.WriteLine("Step : Fill in the Prénom (First Name) field with a valid value: " + prenom);
        }

        // Get the current value of the Prénom field
        public string GetPrénomValue()
        {
            return driver.FindElement(prénomField).GetAttribute("value");
        }

        public void EnterNomValue(string nom)
        {
            driver.FindElement(nomField).SendKeys(nom);
            Console.WriteLine("Step : Fill in the Nom (Last Name) field with a valid value: " + nom);
        }

        public void EnterEmailValue(string email)
        {
            driver.FindElement(emailField).SendKeys(email);
            Console.WriteLine("Step : Fill in the email field with a valid value: " + email);
        }
        // get tge current value of the email field
        public string GetEmailValue()
        {
            return driver.FindElement(emailField).GetAttribute("value");
        }

        public void EnterPosteValue(string poste)
        {
            driver.FindElement(posteField).SendKeys(poste);
            Console.WriteLine("Step : Fill in the Poste (Position) field with a valid value: " + poste);
        }

        public void EnterEntrepriseValue(string entreprise)
        {
            driver.FindElement(entrepriseField).SendKeys(entreprise);
            Console.WriteLine("Step : Fill in the Entreprise (Company) field with a valid value: " + entreprise);
        }

        public void CheckNewsletterCheckbox()
        {
            driver.FindElement(checkboxNewsletter).Click();
            Console.WriteLine("Step :Check the checkbox 'Je souhaite être informé des actualités du site par email' as optional");
        }

        public void ClickOnContinuerButton()
        {
            driver.FindElement(continuerButton).Click();
            Console.WriteLine("Step : Click on the 'Continuer' button to submit the registration form.");
        }

        public bool VerifyThatRegisterPriseDemandeDisplay()
        {
            if (driver.FindElement(successMsgAfterRegister).Displayed)
            {
                isDisplayed = true;
            }
            else
            {
                isDisplayed = false;
            }
            return isDisplayed;
        }

        public bool VerifyThatNgInvalidEmailClassProperty()
        {
            if (driver.FindElement(ngInvalidEmail).Displayed)
            {
                isNgInvalidEmailDisplayed = true;
                Console.WriteLine("Email is invalid and get the class attribute 'ng-invalid-email' indicate error message display");
            }
            else
            {
                isNgInvalidEmailDisplayed = false;
                Console.WriteLine("No error message display for invalid Email");
            }
            return isNgInvalidEmailDisplayed;
        }
    }
}
