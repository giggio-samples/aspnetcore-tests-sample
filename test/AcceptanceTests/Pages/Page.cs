using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using System;

namespace AcceptanceTests.Pages
{
    public abstract class Page
    {
        public static string BaseUrl { get; set; }

        public abstract string Path { get; }

        public string CurrentUrl => Driver.Url;

        public virtual void Navigate()
        {
            Driver.Url = $"{BaseUrl}{Path}";
            Driver.Navigate();
        }

        public virtual void NgNavigate(params object[] pathParts)
        {
            if (pathParts == null)
                throw new ArgumentNullException(nameof(pathParts));
            NgNavigateAbsolute(string.Format(Path, pathParts));
        }

        public virtual void NgNavigateAbsolute(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path is required.", nameof(path));
            JsDriver.ExecuteAsyncScript($"var callback = arguments[arguments.length - 1];ngNavigate('/').then(function () {{ return ngNavigate('{path}'); }}).then(callback, callback);");
            Wait();
        }

        protected static IWebDriver Driver => DriverManager.Driver;

        protected static IJavaScriptExecutor JsDriver => Driver as IJavaScriptExecutor;

        public IWebDriver Type(object model)
        {
            var modelType = model.GetType();
            var props = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
                Type(By.Id(prop.Name.ToLower()), prop.GetValue(model).ToString());
            return Driver;
        }

        protected IWebElement Type(By by, string text)
        {
            var element = Driver.WaitUntil(x =>
            {
                var els = x.FindElements(by);
                if (els.Count == 0)
                    return null;
                var el = els.First();
                return el.Displayed && el.Enabled ? el : null;
            });
            element.Clear();
            element.SendKeys(text);
            return element;
        }

        public void Wait() => Driver.WaitUntil(driver => driver.FindElement(By.TagName("app-root")).FindElements(By.XPath(".//*")).Any());
    }
}
