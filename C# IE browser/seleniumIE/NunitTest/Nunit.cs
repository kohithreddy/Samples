using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.IE;
using NUnit.Framework;

namespace NunitTest
{ 
    class Nunit
    {
        IWebDriver driver;
              
        [SetUp]
        public void startBrowser()
        {
            driver = new InternetExplorerDriver();
        }

        [Test]
        public void kohithtest()
        {
            driver.Url = "http://www.google.co.in";
        }
        [Test]
        public void Karthiktest()
        {
            driver.Url = "http://www.google.co.in";
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }

    }
}
