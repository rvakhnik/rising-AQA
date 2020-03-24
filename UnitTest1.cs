using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SampleTestProject
{
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            // Surely no one writes tests such way

            var driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
            driver.Url = "https://www.tut.by/";

            var inputElement = driver.FindElement(By.CssSelector("#search_from_str"));
            inputElement.Click();

            // Waiting for tabs to appear
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(dr => ExpectedConditions.ElementIsVisible(By.CssSelector(".b-search-tabs li")));

            var tabName = "Байнет";
            var tabElements = driver.FindElements(By.CssSelector(".b-search-tabs li"));
            var tabToSelect = tabElements.FirstOrDefault(el => el.Text.Trim() == tabName);
            Assert.IsNotNull(tabToSelect, $"Can't find tab {tabName}");
            tabToSelect.Click();
            
            inputElement.Clear();
            inputElement.SendKeys("котята");

            var searchButton = driver.FindElement(By.CssSelector("input[name=\"search\"]"));
            searchButton.Click();
            
            var resultCards = driver.FindElements(By.CssSelector(".b-results-list>li:not(.m-market)")).Where(el => el.Displayed && !string.IsNullOrEmpty(el.Text.Trim())).ToList();
            var titles = resultCards.Select(el => el.FindElement(By.CssSelector("h3")).Text.Trim());
            Console.WriteLine(string.Join("\n", titles));

            var links = resultCards.Select(el => el.FindElement(By.CssSelector(".b-url__a")).GetAttribute("href"));
            Console.WriteLine(string.Join("\n", links));

            driver.Quit();

        }
    }
}
