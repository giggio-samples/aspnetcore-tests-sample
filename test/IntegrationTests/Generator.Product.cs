using Bogus;
using SampleApp.Models;
using System;

namespace IntegrationTests
{
    public static partial class Generator
    {
        public static Faker<Product> Product { get; } =
            new Faker<Product>("pt_BR")
            .StrictMode(true)
            .RuleFor(c => c.Id, f => 0)
            .RuleFor(c => c.Name, f => f.Commerce.Product())
            .RuleFor(c => c.Price, f => Math.Round(f.Random.Decimal(3, 20), 2));
    }
}
