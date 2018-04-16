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

namespace PortalTelemedicina.Tests
{
    public class UserControllerTest : BaseTestController
    {
        private UserController _controller;
        private ApplicationContext _dbContext;

        public UserControllerTest()
        {
            _dbContext = CreateInMemoryContext();

            // ARRANGE
            _dbContext.Users.AddRange(
                new User { UserName = "wsmith", DisplayName = "Will Smith", Email = "wsmith@portaltelemedicina.com.br", Password = "Abc123?!", CreationDate = DateTime.Now },
                new User { UserName = "hspecter", DisplayName = "Havery Specter", Email = "hspecter@portaltelemedicina.com.br", Password = "Zxc123?!", CreationDate = DateTime.Now }
            );
            _dbContext.SaveChanges();
            _controller = new UserController(_dbContext);
        }

        [Fact]
        public async Task When_GetUsers_Success()
        {
            // ACT: check if the action response is OK and check if users found
            var viewModel = new UserSearchViewModel { };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.Users(viewModel);

            // ASSERT: Return type
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            var users = objectResult.Value as List<UserSearchResultViewModel>;

            // ASSERT: Has items
            Assert.NotEmpty(users);
        }
    }
}
