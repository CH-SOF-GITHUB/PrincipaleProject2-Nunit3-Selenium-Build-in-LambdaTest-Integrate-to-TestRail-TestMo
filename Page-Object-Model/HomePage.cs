using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRailTesting.Page_Object_Model
{
    internal class HomePage
    {
        private readonly IWebDriver driver;
        private readonly string homeURL = "https://www.appvizer.fr/";
        private readonly By connexionButton = By.CssSelector("body > header > div > div > a.login.no-link-style");

        // constructor to initialize the driver and web elements
        public HomePage(IWebDriver IDriver)
        {
            this.driver = IDriver;
        }

        /* Code reusability : Methods that interact with specific elements can be reused by several tests without duplication */
        public void ClickOnConnexionBtn()
        {
            driver.FindElement(connexionButton).Click();
            Console.WriteLine("Step: Click on 'Connexion' button - home page");
        }
    }
}
