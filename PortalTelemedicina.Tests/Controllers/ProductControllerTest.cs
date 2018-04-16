using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.Controllers;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using PortalTelemedicina.Tests.Extensions;
using PortalTelemedicina.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PortalTelemedicina.Tests.Controllers
{
    public class ProductControllerTest : BaseTestController
    {
        private ProductController _controller;
        private ApplicationContext _dbContext;

        public ProductControllerTest()
        {
            _dbContext = CreateInMemoryContext();

            // ARRANGE
            _dbContext.Products.AddRange(
                new Product { Name = "Prod 1", Description = "Desc 1", Price = 1300, CreationDate = DateTime.Now },
                new Product { Name = "Prod 2", Description = "Desc 2", Price = 1400, CreationDate = DateTime.Now },
                new Product { Name = "Prod 3", Description = "Desc 3", Price = 1500, CreationDate = DateTime.Now }
            );
            _dbContext.SaveChanges();
            _controller = new ProductController(_dbContext);
        }

        [Fact]
        public async Task When_GetProduct_Success()
        {
            // ACT: check if the action response is OK and check if the product found
            var viewModel = new ProductSearchViewModel { Name = "Prod 1" };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Products(viewModel);

            // ASSERT: Return type
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            var products = objectResult.Value as List<ProductSearchResultViewModel>;

            // ASSERT: Has items
            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task When_CreateProduct_Success()
        {
            // ACT
            var viewModel = new ProductCreateViewModel { Name = "Prod 4", Description = "Desc 4", Price = 62 };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Create(viewModel);

            // ASSERT
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_CreateProduct_Fails_FieldValidation()
        {
            // ACT
            var viewModel = new ProductCreateViewModel { };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Create(viewModel);

            // ASSERT
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task When_UpdateProduct_Success()
        {
            // ACT
            var viewModel = new ProductUpdateViewModel { Name = "Prod 4", Description = "Desc 4", Price = 62 };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Update(1, viewModel);

            // ASSERT: Return type
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_UpdateProduct_Fails_FieldValidation()
        {
            // ACT
            var viewModel = new ProductUpdateViewModel { };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Update(1, viewModel);

            // ASSERT: Return type
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task When_UpdateProduct_Fails_ProductNotFound()
        {
            // ACT
            var viewModel = new ProductUpdateViewModel { };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Update(999, viewModel);

            // ASSERT: Return type
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
