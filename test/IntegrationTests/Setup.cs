using Bogus;
using Lambda3.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using SampleApp;
using System;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [SetUpFixture]
    public class Setup
    {
        private WebApplicationFactory<Startup> webAppFactory;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            StartApiServer();
            BaseIntegrationTest.WebAppFactory = webAppFactory;
            await webAppFactory.MigrateDbAndSeedAsync();
        }

        private void StartApiServer() => webAppFactory = new WebApplicationFactory<Startup>(inMemory: true).EnsureServerStarted();

        [OneTimeTearDown]
        public void OneTimeTearDown() => webAppFactory?.Dispose();
    }
}

