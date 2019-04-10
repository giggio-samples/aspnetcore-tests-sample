using OpenQA.Selenium;

namespace AcceptanceTests.Pages
{
    public class HomePage : Page
    {
        public override string Path => "/";

        public bool HasTitle() => Driver.FindElement(By.Id("title")).Displayed;
    }
}
