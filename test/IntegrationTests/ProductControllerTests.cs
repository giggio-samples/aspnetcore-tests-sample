using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SampleApp.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class ProductControllerGetAllTests : BaseIntegrationTest
    {
        [Test]
        public async Task GetNotFound()
        {
            var repository = serviceProvider.GetService<IProductsRepository>();
            var allProducts = await repository.GetAllAsync();
            foreach (var product in allProducts)
                await repository.DeleteAsync(product.Id);
            var response = await client.GetAsync("/api/products/3");
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

    }

    public class ProductControllerGetOneTests : BaseIntegrationTest
    {
        private IProductsRepository repository;
        private HttpResponseMessage response;
        private Product product;

        [OneTimeSetUp]
        public async Task Setup()
        {
            repository = serviceProvider.GetService<IProductsRepository>();
            var allProducts = await repository.GetAllAsync();
            foreach (var p in allProducts)
                await repository.DeleteAsync(p.Id);
            product = Generator.Product;
            await repository.CreateAsync(product);
            response = await client.GetAsync($"/api/products/{product.Id}");
        }

        [Test]
        public void StatusCodeIsOk() => response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

        [Test]
        public async Task ProductFound()
        {
            var productReturned = await response.Content.ReadAsAsync<Product>();
            productReturned.Should().BeEquivalentTo(product);
        }
    }

    public class ProductControllerPostTests : BaseIntegrationTest
    {
        private IProductsRepository repository;
        private HttpResponseMessage response;
        private Product product;

        [OneTimeSetUp]
        public async Task Setup()
        {
            repository = serviceProvider.GetService<IProductsRepository>();
            var allProducts = await repository.GetAllAsync();
            foreach (var p in allProducts)
                await repository.DeleteAsync(p.Id);
            product = Generator.Product;
            response = await client.PostAsJsonAsync($"/api/products/", product);
        }

        [Test]
        public async Task OnlyOneItemIsInDb() => (await repository.GetAllAsync()).Count().Should().Be(1);

        [Test]
        public void StatusCodeIsOk() => response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);

        [Test]
        public void LocationIsCorrect() => response.Headers.Location.ToString().Should().MatchRegex(@"http://localhost/api/Products/\d+");

        [Test]
        public async Task ProductCreatedWasReturned()
        {
            var productReturned = await response.Content.ReadAsAsync<Product>();
            productReturned.Name.Should().Be(product.Name);
            productReturned.Price.Should().Be(product.Price);
        }
    }
}