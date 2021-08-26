using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;

namespace seleniumIE
{
    [TestClass]
    class Program
    {
        [TestMethod]
        static void Main(string[] args)
        {
            IWebDriver driver = new InternetExplorerDriver();
            InternetExplorerOptions capability = new InternetExplorerOptions();
            driver.Navigate().GoToUrl("https://google.com");
            driver.FindElement(By.Name("q")).SendKeys("Kohith Reddy");
            driver.Manage().Window.FullScreen();
            driver.Close();
            driver.Quit();
         }
    }

}
