using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTesting
{
    class HarnessScraper
    {
        class SeliniumDemo
        {
            IWebDriver driver;
            string filePath = @"C:\Users\Peter\Documents\GitHub\SeleniumTesting\SeleniumTesting\files\file.txt";
            [SetUp]
            public void startBrowser()
            {
                driver = new ChromeDriver(@"C:\Users\Peter\Documents\GitHub\SeleniumTesting\SeleniumTesting\files");
                
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            [Test]
            public void Harness()
            {
                harnessLogin();

                while(true)
                {
                    List<string> lines = new List<string>();
                    string line = "";                    
                    for(int row = 1; row <= 20; row++)
                    {
                        for (int column = 2; column <= 7; column++)
                        {
                            string xPath = "//*[@id=\"classlisting\"]/tbody/tr["+row+"]/td["+column+"]";
                            if (elementExistsXPath(xPath))
                            {
                                line += driver.FindElement(By.XPath(xPath)).Text;
                            }
                            line += ",";
                        }
                        if (line.Contains("Enrolled") && !line.Contains("{Bridge Run}"))
                        {
                            lines.Add(line);
                        }
                        line = "";
                    }

                    using (StreamWriter sw = new StreamWriter(filePath, true))
                    {
                        foreach(string s in lines)
                        {
                            sw.WriteLine(s);
                        }                        
                    }
                    if (elementExists("next"))
                    {
                        driver.FindElement(By.ClassName("next")).Click();
                    }
                    else
                    {
                        break;
                    }                       
                }    
                
                
            }

            [TearDown]
            public void closeBrowser()
            {
                driver.Close();
            }


            private void harnessLogin()
            {
                string userName = "";
                string password = "";
                driver.Url = "https://www.harnesscycle.com/reserve/index.cfm?action=Account.classes";
                driver.FindElement(By.XPath("//*[@id=\"supernav\"]/a[1]")).Click(); //Top Nav Login
                driver.FindElement(By.XPath("//*[@id=\"username\"]")).SendKeys(userName);
                driver.FindElement(By.XPath("//*[@id=\"password\"]")).SendKeys(password);
                driver.FindElement(By.XPath("//*[@id=\"signinbox\"]/form/div[4]/button")).Click(); //Sign in
            }

            private Boolean elementExists(string className)
            {
                try
                {
                    driver.FindElement(By.ClassName(className));
                } catch(NoSuchElementException e)
                {
                    return false;
                }
                return true;
            }

            private Boolean elementExistsXPath(string xPath)
            {
                try
                {
                    driver.FindElement(By.XPath(xPath));
                }
                catch (NoSuchElementException e)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
