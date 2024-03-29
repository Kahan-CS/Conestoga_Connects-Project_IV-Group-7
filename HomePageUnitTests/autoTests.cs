using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Bunit;
using OpenQA.Selenium.Edge;

namespace HomePageUnitTests
{
    public class AutoTests : TestContext
    {
        IWebDriver driver;
        public void Setup()
        {
            driver = new EdgeDriver();
        }

        [Fact] 
        public void LoginButtonNavigatesToHome()
        {
            Setup(); 
            // Arrange
            driver.Navigate().GoToUrl("https://localhost:7052/login");

            Thread.Sleep(6000);
            // Act
            var loginBtn = driver.FindElement(By.Id("loginbtn")); 
            loginBtn.Click();

            // Assert that the new URL is the home page

            Assert.Equal("https://localhost:7052/home", driver.Url);
            TearDown();
        }

        [Fact]
        public void RegisterLinkNavigatesToRegister()
        {
            Setup();
            // Arrange
            driver.Navigate().GoToUrl("https://localhost:7052/login");

            Thread.Sleep(5000);
            // Act
            var registerBtn = driver.FindElement(By.Id("signup"));
            registerBtn.Click();

            // Assert that the new URL is the register page
            Assert.Equal("https://localhost:7052/signup", driver.Url);
            TearDown();
        }

        [Fact]
        public void LoginButtonNavigatesToHomeAndHomeLogsOut()
        {
            Setup();
            // Arrange
            driver.Navigate().GoToUrl("https://localhost:7052/login");

            Thread.Sleep(5000);
            // Act
            var loginBtn = driver.FindElement(By.Id("loginbtn"));
            loginBtn.Click();

            // Assert that the new URL is the home page

            Assert.Equal("https://localhost:7052/home", driver.Url);
            Thread.Sleep(50);

            // Act
            var logoutBtn = driver.FindElement(By.Id("logout"));
            logoutBtn.Click();

            // Assert that the new URL is the login page
            Assert.Equal("https://localhost:7052/login", driver.Url);

            TearDown();
        }

        [Fact]
        public void LoginButtonNavigatesToHomeThenConnections()
        {
            Setup();
            // Arrange
            driver.Navigate().GoToUrl("https://localhost:7052/login");

            Thread.Sleep(5000);
            // Act
            var loginBtn = driver.FindElement(By.Id("loginbtn"));
            loginBtn.Click();

            // Assert that the new URL is the home page

            Assert.Equal("https://localhost:7052/home", driver.Url);

            Thread.Sleep(50);

            // Act
            var connectionsBtn = driver.FindElement(By.Id("connections"));

            connectionsBtn.Click();

            // Assert that the new URL is the connections page
            Assert.Equal("https://localhost:7052/connections", driver.Url);
            TearDown();
        }

        public void TearDown()
        {
            driver.Quit();
        }
    }
}
