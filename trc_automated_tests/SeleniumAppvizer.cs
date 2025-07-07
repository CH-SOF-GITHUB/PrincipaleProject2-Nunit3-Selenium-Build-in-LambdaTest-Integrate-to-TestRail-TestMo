using Microsoft.Win32;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Input;
using OpenQA.Selenium.BiDi.Modules.Script;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestRailTesting.Base;
using TestRailTesting.Page_Object_Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestRailTesting.trc_automated_tests
{
    [TestFixture(Author = "Chaker Ben Said", Category = "Automation testing", Description = "write and test appvizer functionalites for regression and repetitifs scenarions with selenium nunit3 and integrate test results to Testmo Management Tool")]
    internal class SeleniumAppvizer : BaseTest
    {
        private IWebElement PrenomField, NomField, EmailField, PosteField, EntrepriseField, CheckboxNotifications, ConnexionButton;

        [TestCase(Description = "Verify that the directs directly to home page and The homepage loads successfully with the correct title.")]
        public void TestMo_AppvizerDisplayHomePage1()
        {
            IDriver.Navigate().GoToUrl("https://www.appvizer.fr/");
            string HomePageTitle = IDriver.Title;
            Assert.That(HomePageTitle, Is.EqualTo("Appvizer | Média & Comparateur de Logiciels pour les Professionnels"));
            Console.WriteLine("Home page loads and displays correctly with a title 'appvizer.fr - 1er Comparateur de logiciels en ligne'");
        }

        [TestCase(Description = "Verify that the directs directly to home page with the correct URL.")]
        public void TestMo_AppvizerDisplayHomePage2()
        {
            IDriver.Navigate().GoToUrl("https://www.appvizer.fr/");
            string HomePageUrl = IDriver.Url;
            Assert.That(HomePageUrl, Is.EqualTo("https://www.appvizer.fr/"));
            Console.WriteLine("Home page loads and displays correctly with a title 'appvizer.fr - 1er Comparateur de logiciels en ligne'");
        }

        [TestCase(Description = "verify that the register page loads successfully and displays with all fields register submit form")]
        public void TestMo_AppvizerDisplayInscriptionPage()
        {
            IDriver.Navigate().GoToUrl("https://www.appvizer.fr/inscription");
            string RegisterPageTitle = IDriver.Title;
            PrenomField = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(1) > input"));
            NomField = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(2) > input"));
            EmailField = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(3) > input"));
            PosteField = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(4) > input"));
            EntrepriseField = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(5) > input"));
            CheckboxNotifications = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div:nth-child(6) > span > input"));
            ConnexionButton = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div.submit-wrap.text-center > button"));
            Assert.Multiple(() =>
            {
                Assert.That(RegisterPageTitle, Is.EqualTo("appvizer.fr - 1er Comparateur de logiciels en ligne"));
                Assert.That(PrenomField.Displayed, Is.True, "Prenom field is not displayed");
                Assert.That(NomField.Displayed, Is.True, "Nom field is not displayed");
                Assert.That(EmailField.Displayed, Is.True, "Email field is not displayed");
                Assert.That(PosteField.Displayed, Is.True, "Poste field is not displayed");
                Assert.That(EntrepriseField.Displayed, Is.True, "Entreprise field is not displayed");
                Assert.That(CheckboxNotifications.Displayed, Is.True, "Checkbox Notifications is not displayed");
                Assert.That(ConnexionButton.Displayed, Is.True, "Connexion button is not displayed");
            });
            TakeResult_screenshot();
            Console.WriteLine("inscription page loads and displays correctly with a register fields form");
        }

        [TestCase(Description = "User successfully submits the registration form with all required fields correctly and pass to the correct response page.")]
        public void TestMo_AppvizerValidRegistrationForSubmission()
        {
            // Step 1: Open the Appvizer registration page using the URL "appvizer.fr/inscription"
            registerPage.OpenTheRegisterPage();
            // Step 2: Fill in the Prénom (First Name) field with a valid value
            registerPage.EnterPrénomValue("Chaker");
            // Step 3: Fill in the Nom (Last Name) field with a valid value
            registerPage.EnterNomValue("Ben Said");
            // Step 4: Fill in the email field with a valid value
            registerPage.EnterEmailValue("chaker.ben_said@yopmail.com");
            // Step 5: Fill in the Poste (Position) field with a valid value
            registerPage.EnterPosteValue("QA Engineer");
            // Step 6: Fill in the Entreprise (Company) field with a valid value
            registerPage.EnterEntrepriseValue("Appvizer");
            // Step 7: Check the checkbox for receiving notifications
            registerPage.CheckNewsletterCheckbox();
            // Step 8: Click on the "Continuer" button to submit the registration form
            registerPage.ClickOnContinuerButton();
            Thread.Sleep(5000); // Wait for the page to load after form submission
            // Step 9: Check successfully sibmitted of register form and a login crednetials emails is
            // sent in the inbox email with the registration email
            bool actualIsDisplayed = registerPage.VerifyThatRegisterPriseDemandeDisplay();
            Assert.IsTrue(actualIsDisplayed, "The registration success message is not displayed as expected after form submission.");
        }


        [TestCase(Description = "User tries to submit the registration form without filling in all fields")]
        public void TestMo_AppvizerInvalidRegistrationForSubmissionAndAllFieldsAreEmpty()
        {
            // Step 1: Open the Appvizer registration page using the URL "appvizer.fr/inscription"
            registerPage.OpenTheRegisterPage();
            // Step 2: Leave the Prénom (First Name) field empty
            registerPage.EnterPrénomValue("");
            // Step 3: Leave the Nom (Last Name) field empty
            registerPage.EnterNomValue("");
            // Step 4: Leave the email field empty
            registerPage.EnterEmailValue("");
            // Step 5: Leave the Poste (Position) field empty
            registerPage.EnterPosteValue("");
            // Step 6: Leave the Entreprise (Company) field empty
            registerPage.EnterEntrepriseValue("");
            // Step 7: Leave the checkbox for receiving notifications unchecked
            // Step 8: Click on the "Continuer" button to submit the registration form
            registerPage.ClickOnContinuerButton();
            // Step 9: The form should not be submitted, and an error message should be displayed for each required field
            Assert.Fail("Tous les champs obligatoires doivent être remplis.");
        }

        [TestCase(Description = "User tries to submit the registration form without filling in required fields")]
        public void TestMo_AppvizerInvalidRegistrationFormSubmissionAndMissingRequiredFields()
        {
            // Step 1: Open the Appvizer registration page using the URL "appvizer.fr/inscription"
            registerPage.OpenTheRegisterPage();
            // Step 2: Leave the required Prénom (First Name) field empty
            registerPage.EnterPrénomValue("");
            // Step 3: Leave the required Nom (Last Name) field empty
            registerPage.EnterNomValue("");
            // Step 4: Leave the required email field empty
            registerPage.EnterEmailValue("");
            // Step 5: Fill in the Poste (Position) field with a valid value
            registerPage.EnterPosteValue("Tester QA");
            // Step 6: Fill in the Entreprise (Company) field with a valid value
            registerPage.EnterEntrepriseValue("Infinity");
            // Step 7: Check the checkbox for receiving notifications
            registerPage.CheckNewsletterCheckbox();
            // Step 8: Click on the "Continuer" button
            registerPage.ClickOnContinuerButton();
            // Step 9: The form should not be submitted, and an error message should be displayed indicating that all required fields need to be filled.
            Assert.Fail("Tous les champs obligatoires doivent être remplis.");
        }

        [TestCase(Description = "User tries to submit the registration form with one or more empty fields")]
        public void TestMo_AppvizerInvalidRegistrationFormSubmissionWithOneOrMoreEmptyFields()
        {
            // Step 1: Open the Appvizer registration page using the URL "appvizer.fr/inscription"
            registerPage.OpenTheRegisterPage();
            // Step 2: Leave the required Prénom (First Name) field empty
            registerPage.EnterPrénomValue("");
            // Step 3: Fill in the required Nom (Last Name) field with a valid value
            registerPage.EnterNomValue("BenSaid");
            // Step 4: Leave the required Email field empty
            registerPage.EnterEmailValue("");
            // Step 5: Fill in the Poste (Position) field with a valid value
            registerPage.EnterPosteValue("Software Tester");
            // Step 6: Leave the Entreprise (Company) field empty
            registerPage.EnterEntrepriseValue("");
            // Step 7: Check the checkbox for receiving notifications
            registerPage.CheckNewsletterCheckbox();
            // Step 8: Click on the "Continuer" button
            registerPage.ClickOnContinuerButton();
            // Step 9: The form should not be submitted, and an error message should be displayed indicating that all required fields need to be filled.
            Assert.Multiple(() =>
            {
                Assert.Fail("Champ Prénom est requis.");
                Assert.Fail("Champ Nom est requis.");
                Assert.Fail("Champ Email est requis.");
            });
        }


        [TestCase(Description = "User tries to submit the registration form with one or more empty required fields.")]
        public void TestMo_AppvizerInvalidRegistrationFormSubmissionMissingOneOrMoreRequiredFields()
        {
            // Step 1: Open the Appvizer registration page using the URL "appvizer.fr/inscription"
            registerPage.OpenTheRegisterPage();
            // Step 2: Leave the required Prénom (First Name) field empty
            registerPage.EnterPrénomValue("");
            // Step 3: Fill in the required Nom (Last Name) field with a valid value
            registerPage.EnterNomValue("Ben Tarek");
            // Step 4: Leave the required Email field empty
            registerPage.EnterEmailValue("");
            // Step 5: Fill in the Poste (Position) field with a valid value
            registerPage.EnterPosteValue("SoftwareTester");
            // Step 6: Leave the Entreprise (Company) field empty
            registerPage.EnterEntrepriseValue("Developement IT");
            // Step 7: Check the checkbox for receiving notifications
            registerPage.CheckNewsletterCheckbox();
            // Step 8: Click on the "Continuer" button
            registerPage.ClickOnContinuerButton();
            // Step 9: The form should not be submitted, and an error message should be displayed indicating that all required fields need to be filled.
            Assert.Multiple(() =>
            {
                Assert.Fail("Champ Prénom est requis.");
                Assert.Fail("Champ Email est requis.");
            });
        }


        [TestCase(Description = "User tries to submit to registration form with invalid email and receives proper error message")]
        public void TestMo_AppvizerProperErrorMessageDisplayedForInvalidEmailField()
        {
            // Step 1: Open the Appvizer registration page using the URL "appvizer.fr/inscription"
            registerPage.OpenTheRegisterPage();
            // Step 2: Fill in the Prénom (First Name) field with a valid value
            registerPage.EnterPrénomValue("Tarek");
            // Step 3: Fill in the Nom (Last Name) field with a valid value
            registerPage.EnterNomValue("Ben Salem");
            // Step 4: Fill in the email field with a valid value
            registerPage.EnterEmailValue("tarek.benSalemyahoo.com");
            // Step 5: Fill in the Poste (Position) field with a valid value
            registerPage.EnterPosteValue("Test Manager");
            // Step 6: Fill in the Entreprise (Company) field with a valid value
            registerPage.EnterEntrepriseValue("Appvizer");
            // Step 7: Check the checkbox for receiving notifications
            registerPage.CheckNewsletterCheckbox();
            // Step 8: Click on the "Continuer" button to submit the registration form
            registerPage.ClickOnContinuerButton();
            Thread.Sleep(3000); // Wait for the page to load after form submission
            // Step 9: The form should not submit and a proper error message  is displayed for invalid email
            bool actualIsNgInvalidEmail = registerPage.VerifyThatNgInvalidEmailClassProperty();
            Assert.That(actualIsNgInvalidEmail, Is.True, "The ng-invalid-email class is not displayed as expected after form submission with an invalid email.");
        }

        // Tested
        [TestCase(Description = "User submits the registration form and receives the first email containing login credentials")]
        public void TestMo_AppvizerReceiveFirstEmailWithLoginCredentials()
        {
            var randomNb = new Random().Next(1, 40);
            // Step 1: Open the Appvizer registration page using the URL "appvizer.fr/inscription"
            registerPage.OpenTheRegisterPage();
            // Step 2: Fill in the Prénom (First Name) field with a valid value
            registerPage.EnterPrénomValue("John");
            string registeredPrénom = registerPage.GetPrénomValue();
            // Step 3: Fill in the Nom (Last Name) field with a valid value
            registerPage.EnterNomValue("Doe");
            // Step 4: Fill in the email field with a valid value
            registerPage.EnterEmailValue("JohnDoe" + randomNb + "@yopmail.com");
            string registeredEmail = registerPage.GetEmailValue();
            // Step 5: Fill in the Poste (Position) field with a valid value
            registerPage.EnterPosteValue("Software Engineer");
            // Step 6: Fill in the Entreprise (Company) field with a valid value
            registerPage.EnterEntrepriseValue("Appvizer");
            // Step 7: Check the checkbox for receiving notifications
            registerPage.CheckNewsletterCheckbox();
            // Step 8: Click on the "Continuer" button to submit the registration form
            registerPage.ClickOnContinuerButton();
            Thread.Sleep(5000);
            // Step 10: Open the Yopmail official page
            yopMailPage.OpenYopMailPage();
            //yopMailPage.ClickConsentButton();
            // Step 11: fill in the email field with a the registration email
            yopMailPage.EnterEmailOfregistration(registeredEmail);
            Console.WriteLine("current URL : " + IDriver.Url + " && current registration email : " + registeredEmail);
            Thread.Sleep(5000);

            // Vérifier si un iframe contenant le reCAPTCHA est présent
            /*try
            {
                IDriver.SwitchTo().Frame(IDriver.FindElement(By.XPath("//iframe[contains(@src, 'recaptcha')]")));
                var recaptchaCheckbox = IDriver.FindElement(By.Id("recaptcha-anchor"));
                recaptchaCheckbox.Click();
                IDriver.SwitchTo().DefaultContent();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("reCAPTCHA checkbox not found or already passed");
            }*/
            string ActualInboxLabel = yopMailPage.GetInboxLabel();
            Assert.That(ActualInboxLabel, Is.EqualTo(registeredEmail.ToLower()));
            Thread.Sleep(5000);
            // Passer dans l'iframe
            var iframe = IDriver.FindElement(By.Id("ifinbox"));
            IDriver.SwitchTo().Frame(iframe);
            // verify that the iframe contains EMail Title
            Assert.That(IDriver.PageSource.Contains($"Bienvenue {registeredPrénom}"));
            Console.WriteLine($"First login Email received with a title 'Bienvenue {registeredPrénom}'");
            // Récupérer les éléments d'email
            /*var emailElements = IDriver.FindElements(By.ClassName("lms"));
            var emailTexts = emailElements.Select(e => e.Text).ToList();
            // Afficher chaque sujet
            foreach (var emailText in emailTexts)
            {
                Console.WriteLine("Email Subject: " + emailText);
            }
            // Revenir au contexte principal
            IDriver.SwitchTo().DefaultContent();
            */
        }

        // Tested
        [TestCase(Description = "User tries to submit the registration form with an email that already exists in the system")]
        public void TestMo_AppvizerRegistrationFormSubmissionWithEmailExists()
        {
            var randomNb = new Random().Next(1, 30);
            // Step 1: Open the Appvizer registration page using the URL "appvizer.fr/inscription"
            registerPage.OpenTheRegisterPage();
            // Step 2: Fill in the Prénom (First Name) field with a valid value
            registerPage.EnterPrénomValue("Chaker");
            // Step 3: Fill in the Nom (Last Name) field with a valid value
            registerPage.EnterNomValue("Ben Said");
            // Step 4: Fill in the email field with a valid value
            registerPage.EnterEmailValue("chaker13BenSaid@yopmail.com");
            // Step 5: Fill in the Poste (Position) field with a valid value
            registerPage.EnterPosteValue("QA Engineer");
            // Step 6: Fill in the Entreprise (Company) field with a valid value
            registerPage.EnterEntrepriseValue("Appvizer");
            // Step 7: Check the checkbox for receiving notifications
            registerPage.CheckNewsletterCheckbox();
            // Step 8: Click on the "Continuer" button to submit the registration form
            registerPage.ClickOnContinuerButton();
            Thread.Sleep(5000); // Wait for the page to load after form submission
            // Step 9: Check submit of register form fails with email already exists
            IWebElement LoginToggleRegister = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > a"));
            Assert.IsTrue(LoginToggleRegister.Displayed, "User not remains in same page of register to validate the failure of submit with email exists");
        }

        // Tested
        [TestCase(Description = "Proper error message displays when User submits the registration form with an email that already exists in the system")]
        public void TestMo_AppvizerProperErrorMessageForEmailExists()
        {
            var randomNb = new Random().Next(1, 30);
            // Step 1: Open the Appvizer registration page using the URL "appvizer.fr/inscription"
            registerPage.OpenTheRegisterPage();
            // Step 2: Fill in the Prénom (First Name) field with a valid value
            registerPage.EnterPrénomValue("Chaker");
            // Step 3: Fill in the Nom (Last Name) field with a valid value
            registerPage.EnterNomValue("Ben Said");
            // Step 4: Fill in the email field with a valid value
            registerPage.EnterEmailValue("chaker13BenSaid@yopmail.com");
            // Step 5: Fill in the Poste (Position) field with a valid value
            registerPage.EnterPosteValue("QA Engineer");
            // Step 6: Fill in the Entreprise (Company) field with a valid value
            registerPage.EnterEntrepriseValue("Appvizer");
            // Step 7: Check the checkbox for receiving notifications
            registerPage.CheckNewsletterCheckbox();
            // Step 8: Click on the "Continuer" button to submit the registration form
            registerPage.ClickOnContinuerButton();
            Thread.Sleep(5000); // Wait for the page to load after form submission
            // Step 9: Check submit of register form fails with email already exists
            IWebElement EmailExistsMsgError = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > form > div.errors > div > span"));
            Assert.Multiple(() =>
            {
                Assert.That(EmailExistsMsgError.Displayed, Is.True, "The error message for existing email is not displayed as expected.");
                Assert.That(EmailExistsMsgError.Text, Is.EqualTo("Adresse email ou mot de passe invalide."), "No Proper error message of existing email matchs the expected");
            });
        }

        [TestCase(Description = "The user verifies the presence of the 'Appvizer Registration' link in the login form page")]
        public void TestMo_AppvizerRegistrationLinkPresent()
        {
            // Step 1: Open the Appvizer login page with the URl "https://www.appvizer.fr/connexion"
            IDriver.Navigate().GoToUrl("https://www.appvizer.fr/connexion");
            // Step 2: Verify that the registration link is present on the login page
            IWebElement RegistrationLink = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > a > span"));
            string RegistrationLinkText = RegistrationLink.Text;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(RegistrationLink.Displayed, "The registration link is not displayed on the login page as expected.");
                Assert.That(RegistrationLinkText, Is.EqualTo("Vous n'avez pas de compte ? S'enregistrer"));
            });
        }

        [TestCase(Description = "The user should direct to the registration page")]
        public void TestMo_AppvizerNavigationOfRegistrationLink()
        {
            // Step 1: Open the Appvizer login page with the URl "https://www.appvizer.fr/connexion"
            IDriver.Navigate().GoToUrl("https://www.appvizer.fr/connexion");
            // Step 2: Locate the registration link and Click it
            IWebElement RegistrationLink = IDriver.FindElement(By.CssSelector("body > ui-view > div > div > div.main-content-no-sticky-header.ng-scope > div > div > div > div.login-box.container-fluid > div > a > span"));
            RegistrationLink.Click();
            // Step 3: Check that the user is redirected to the registration page
            string urlOfRegistration = IDriver.Url;
            Assert.That(urlOfRegistration, Is.EqualTo("https://www.appvizer.fr/inscription"));
        }

        
    }
}
