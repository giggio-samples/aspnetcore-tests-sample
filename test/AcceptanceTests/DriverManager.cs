using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AcceptanceTests
{
    public static class DriverManager
    {
        public static IWebDriver Driver { get; private set; }

        public static void Start()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            if (!Debugger.IsAttached)
            {
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--window-size=1920,1080");
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    chromeOptions.AddArgument("--disable-gpu"); // until not needed, see https://bugs.chromium.org/p/chromium/issues/detail?id=737678
            }
            Driver = new ChromeDriver(Environment.CurrentDirectory, chromeOptions, TimeSpan.FromSeconds(120));
            Driver.Manage().Window.Maximize();
        }

        public static void Stop() => Driver?.Quit();
    }
}
