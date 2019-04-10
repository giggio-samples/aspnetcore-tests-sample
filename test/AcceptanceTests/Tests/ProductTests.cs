using AcceptanceTests.Pages;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SampleApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AcceptanceTests.Tests
{
    public class ProductCreateTest : BaseAcceptanceTest<ProductCreatePage>
    {
        private IProductsRepository repository;

        [OneTimeSetUp]
        public async Task CreateProduct()
        {
            repository = serviceProvider.GetService<IProductsRepository>();
            var allProducts = await repository.GetAllAsync();
            foreach (var product in allProducts)
                await repository.DeleteAsync(product.Id);
            Page.NgNavigate();
            Page.Type(new { name = "Foo", price = "23" });
            Page.Submit();
            System.Threading.Thread.Sleep(5000); // todo: use a clever wait to wait for Angular
        }

        [Test]
        public void UrlIsCorrect() => Page.CurrentUrl.Should().Be("http://localhost:7200/product");


        [Test]
        public async Task ProductCreatedOnDb()
        {
            var product = (await repository.GetAllAsync()).First();
            product.Name.Should().Be("Foo");
        }

        [Test]
        public async Task ThereIsOnlyOneProductAfterCreating() => (await repository.GetAllAsync()).Count().Should().Be(1);
    }
}
