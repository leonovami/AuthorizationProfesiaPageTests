using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace AuthorizationProfesiaPageTests
{
    public class Tests
    {
        private IWebDriver driver;

        private readonly By _singInButton = By.XPath("//button[@id='login_modal']");
        private readonly By _loginInputButton = By.XPath("//input[@name='login']");
        private const string _login = "mail@gmail.com";
        private readonly By _passwordInputButton = By.XPath("//input[@name='password']");
        private const string _password = "123";
        private readonly By _enterButton = By.XPath("//input[@id='loginModalUserSubmitBtn']");
        private readonly By _noEntryAlert = By.XPath("/html/body/div[4]/div/div/div[2]/div/div/div/div[1]/form[1]/div[2]/div/div");
        private const string _expectedAlert = "Nesprávne prihlasovacie údaje.";

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            driver.Navigate().GoToUrl("https://www.profesia.sk/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            var singIn = driver.FindElement(_singInButton);
            singIn.Click();

            Thread.Sleep(400);

            var login = driver.FindElement(_loginInputButton);
            login.SendKeys(_login);

            Thread.Sleep(400);

            var password = driver.FindElement(_passwordInputButton);
            password.SendKeys(_password);

            var enter = driver.FindElement(_enterButton);
            enter.Click();

            Thread.Sleep(400);
            var actualAlert = driver.FindElement(_noEntryAlert).Text;

            Assert.AreEqual(_expectedAlert, actualAlert, "Missing or incorrect authorization error message");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}