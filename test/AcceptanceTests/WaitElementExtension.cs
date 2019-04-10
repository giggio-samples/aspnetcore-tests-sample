using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AcceptanceTests
{
    public static class WaitElementExtension
    {
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(20);

        public static IWebElement WaitElementEnabled(this IWebDriver driver, By by, int? waitInSeconds = null) =>
            new WebDriverWait(driver, GetTimeout(waitInSeconds)).Until(x =>
            {
                try
                {
                    var element = x.FindElement(by);
                    return element?.Enabled == true ? element : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });


        public static TResult WaitUntil<TResult>(this IWebDriver driver, Func<IWebDriver, TResult> condition, int? waitInSeconds = null)
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));
            return new WebDriverWait(driver, GetTimeout(waitInSeconds)).Until(d =>
            {
                try
                {
                    return condition.Invoke(d);
                }
                catch (StaleElementReferenceException)
                {
                    return default;
                }
            });
        }

        public static TimeSpan GetTimeout(int? seconds) =>
            seconds != null ? TimeSpan.FromSeconds(seconds.Value) : DefaultTimeout;
    }
}
