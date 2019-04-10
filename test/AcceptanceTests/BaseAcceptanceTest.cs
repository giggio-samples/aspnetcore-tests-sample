using AcceptanceTests.Pages;
using Lambda3.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SampleApp;
using System;
using System.Net.Http;

namespace AcceptanceTests
{
    public class BaseAcceptanceTest
    {
        public static WebApplicationFactory<Startup> WebAppFactory { get; set; }
    }

    public abstract class BaseAcceptanceTest<TPage>
        where TPage : Page, new()
    {
        private IServiceScope scope;
        protected IServiceProvider serviceProvider;
        protected HttpClient client;

        public TPage Page { get; protected set; } = new TPage();

        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
            client = BaseAcceptanceTest.WebAppFactory.CreateClient();
            scope = BaseAcceptanceTest.WebAppFactory.Host.Services.CreateScope();
            serviceProvider = scope.ServiceProvider;
        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
            scope?.Dispose();
            client?.Dispose();
        }
    }
}
