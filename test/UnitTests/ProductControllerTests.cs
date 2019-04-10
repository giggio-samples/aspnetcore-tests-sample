using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SampleApp.Controllers;
using SampleApp.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace UnitTests
{
    public class ProductControllerTests
    {
        [Test]
        public async Task GetAllSucceeds()
        {
            // arrange
            var productsRepositoryMock = new Mock<IProductsRepository>();
            var expectedProducts = new List<Product>();
            productsRepositoryMock.Setup(pr => pr.GetAllAsync()).ReturnsAsync(expectedProducts);
            var productsController = new ProductsController(productsRepositoryMock.Object);
            // act
            var productsRetrieved = await productsController.GetProduct();
            // assert
            ((OkObjectResult)productsRetrieved.Result).Value.Should().BeSameAs(expectedProducts);
        }

        [Test]
        public async Task GetOneSucceeds()
        {
            // arrange
            Product product = Generator.Product;
            var productsController = new ProductsController(Mock.Of<IProductsRepository>(pr => pr.GetAsync(1) == Task.FromResult(product)));
            // act
            var productRetrieved = await productsController.GetProduct(1);
            // assert
            productRetrieved.Value.Should().Be(product);
        }

        [Test]
        public async Task PostSucceedsReturnsCorrectStatusCode()
        {
            // arrange
            var productsRepositoryMock = new Mock<IProductsRepository>();
            Product product = Generator.Product;
            productsRepositoryMock.Setup(pr => pr.CreateAsync(product)).ReturnsAsync(true);
            var productsController = new ProductsController(productsRepositoryMock.Object);
            // act
            var result = await productsController.PostProduct(product);
            // assert
            ((CreatedAtActionResult)result.Result).StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Test]
        public async Task PostSucceedsReturnsProduct()
        {
            // arrange
            var productsRepositoryMock = new Mock<IProductsRepository>();
            Product product = Generator.Product;
            productsRepositoryMock.Setup(pr => pr.CreateAsync(product)).ReturnsAsync(true);
            var productsController = new ProductsController(productsRepositoryMock.Object);
            // act
            var result = await productsController.PostProduct(product);
            // assert
            ((CreatedAtActionResult)result.Result).Value.Should().Be(product);
        }

        [Test]
        public async Task PostSucceedsReturnsCorrectRoute()
        {
            // arrange
            var productsRepositoryMock = new Mock<IProductsRepository>();
            Product product = Generator.Product;
            product.Id = 3;
            productsRepositoryMock.Setup(pr => pr.CreateAsync(product)).ReturnsAsync(true);
            var productsController = new ProductsController(productsRepositoryMock.Object);
            // act
            var result = await productsController.PostProduct(product);
            // assert
            ((CreatedAtActionResult)result.Result).RouteValues.Should().Contain("id", 3);
        }

        [Test]
        public async Task PostSucceedsSavesToRepository()
        {
            // arrange
            var productsRepositoryMock = new Mock<IProductsRepository>();
            Product product = Generator.Product;
            var productsController = new ProductsController(productsRepositoryMock.Object);
            // act
            var result = await productsController.PostProduct(product);
            // assert
            productsRepositoryMock.Verify(pr => pr.CreateAsync(product));
        }

        [Test]
        public async Task PostWithProductWithNegativePriceReturnsBadResult()
        {
            // arrange
            var productsRepositoryMock = new Mock<IProductsRepository>(MockBehavior.Strict);
            Product product = Generator.ProductWithNegativePrice;
            productsRepositoryMock.Setup(pr => pr.GetAsync(1)).ReturnsAsync(product);
            var productsController = new ProductsController(productsRepositoryMock.Object);
            // act
            var result = await productsController.PostProduct(product);
            // assert
            ((StatusCodeResult)result.Result).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}