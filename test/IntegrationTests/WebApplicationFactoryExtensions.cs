using Lambda3.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using static System.Console;
using SampleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SampleApp;

namespace IntegrationTests
{
    public static class WebApplicationFactoryExtensions
    {
        public static async Task MigrateDbAndSeedAsync<TStartup>(this WebApplicationFactory<TStartup> webApplicationFactory) where TStartup : class
        {
            var services = webApplicationFactory.Host.Services;
            using (var scope = services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<SampleAppContext>();
                var logger = scopedServices.GetRequiredService<ILogger<WebApplicationFactory<Startup>>>();
                await db.Database.EnsureCreatedAsync();
            }
        }

        public static WebApplicationFactory<TStartup> EnsureServerStarted<TStartup>(this WebApplicationFactory<TStartup> webApplicationFactory) where TStartup : class
        {
            webApplicationFactory.CreateDefaultClient();
            return webApplicationFactory;
        }

    }
}
