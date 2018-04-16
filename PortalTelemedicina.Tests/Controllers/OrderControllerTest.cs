using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.Controllers;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using PortalTelemedicina.Tests.Extensions;
using PortalTelemedicina.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PortalTelemedicina.Tests.Controllers
{
    public class OrderControllerTest : BaseTestController
    {
        private OrderController _controller;
        private ApplicationContext _dbContext;

        public OrderControllerTest()
        {
            _dbContext = CreateInMemoryContext();

            // ARRANGE
            _dbContext.Users.Add(new User { UserName = "wsmith", DisplayName = "Will Smith", Email = "wsmith@portaltelemedicina.com.br", Password = "Abc123?!", CreationDate = DateTime.Now });
            _dbContext.Products.AddRange(
                new Product { Name = "Prod 1", Description = "Desc 1", Price = 1300, CreationDate = DateTime.Now },
                new Product { Name = "Prod 2", Description = "Desc 2", Price = 700, CreationDate = DateTime.Now });
            _dbContext.Orders.Add(new Order { UserId = 1, CreationDate = DateTime.Now,
                OrderItems = new List<OrderItem> {
                    new OrderItem { OrderId = 1, ProductId = 1, Amount = 10, CurrentPrice = 1299 },
                    new OrderItem { OrderId = 1, ProductId = 2, Amount = 20, CurrentPrice = 699 }
                }
            });
            _dbContext.SaveChanges();
            _controller = new OrderController(_dbContext);
        }

        [Fact]
        public async Task When_GetOrders_Success()
        {
            // ACT: check if the action response is OK and check if orders found
            var viewModel = new OrderSearchViewModel { };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Orders(viewModel);

            // ASSERT: Return type
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            var orders = objectResult.Value as List<OrderResultViewModel>;

            // ASSERT: Result has orders and the first has 2 order items
            Assert.NotEmpty(orders);
            Assert.Equal(2, orders.First().OrderItems.Count);
        }

        [Fact]
        public async Task When_CreateOrders_Success()
        {
            // ACT
            var viewModel = new OrderCreateViewModel
            {
                UserId = 1,
                OrderItems = new List<OrderItemViewModel> {
                    new OrderItemViewModel { ProductId = 1, Amount = 5, CurrentPrice = 50 },
                    new OrderItemViewModel { ProductId = 2, Amount = 15, CurrentPrice = 150 }
                }
            };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Create(viewModel);

            // ASSERT: Return type
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_CreateOrders_Fails_FieldValidation()
        {
            // ACT
            var viewModel = new OrderCreateViewModel { };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Create(viewModel);

            // ASSERT: Return type
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task When_CreateOrders_Fails_MissingProduct()
        {
            // ACT
            var viewModel = new OrderCreateViewModel { UserId = 1 };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Create(viewModel);

            // ASSERT: Return type
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
