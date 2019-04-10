using AcceptanceTests.Pages;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AcceptanceTests.Tests
{
    public class SmokeTest : BaseAcceptanceTest
    {
        [Test]
        public void HomeWithoutPageObject() // don't do this
        {
            (DriverManager.Driver as IJavaScriptExecutor).ExecuteAsyncScript("var callback = arguments[arguments.length - 1];ngNavigate('/').then(function () { return ngNavigate('/'); }).then(callback, callback);");
            DriverManager.Driver.FindElement(By.Id("title")).Displayed.Should().BeTrue();
        }

        [Test]
        public void Home()
        {
            var homePage = new HomePage();
            homePage.NgNavigate();
            homePage.HasTitle().Should().BeTrue();
        }
    }
}
