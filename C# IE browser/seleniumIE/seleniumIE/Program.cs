using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;


namespace seleniumIE
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Navigate().GoToUrl("https://google.com");
            driver.FindElement(By.Name("q")).SendKeys("Kohith Reddy");
            driver.Quit();
            driver.Close();
         }
    }
}
