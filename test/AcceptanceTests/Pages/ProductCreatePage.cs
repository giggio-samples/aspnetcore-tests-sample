using OpenQA.Selenium;

namespace AcceptanceTests.Pages
{
    public class ProductCreatePage : Page
    {
        public override string Path => "/product/create";

        public void Submit() => Driver.WaitElementEnabled(By.Id("productCreate")).Click();
    }
}
