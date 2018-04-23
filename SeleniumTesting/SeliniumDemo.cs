using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTesting
{
    class SeliniumDemo
    {
        IWebDriver driver;
        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\Peter\\Documents\\GitHub\\SeleniumTesting\\SeleniumTesting\\bin");
        }

        [Test]
        public void test()
        {
            driver.Url = "http://www.google.co.in";
            IWebElement element = driver.FindElement(By.XPath("//*[@id=\"lst-ib\"]"));
            element.SendKeys("Hello World!");
            element = driver.FindElement(By.XPath("//*[@id=\"tsf\"]/div[2]/div[3]/center/input[1]"));
            element.Click();
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
