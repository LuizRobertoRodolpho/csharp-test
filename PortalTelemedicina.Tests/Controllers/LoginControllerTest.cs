using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.Controllers;
using PortalTelemedicina.Repository;
using PortalTelemedicina.Repository.Entities;
using PortalTelemedicina.Tests.Extensions;
using PortalTelemedicina.ViewModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PortalTelemedicina.Tests.Controllers
{
    public class LoginControllerTest : BaseTestController
    {
        private LoginController _controller;
        private ApplicationContext _dbContext;

        public LoginControllerTest()
        {
            _dbContext = CreateInMemoryContext();

            // ARRANGE
            _dbContext.Users.AddRange(
                new User { UserName = "wsmith", DisplayName = "Will Smith", Email = "wsmith@portaltelemedicina.com.br", Password = "Abc123?!", CreationDate = DateTime.Now },
                new User { UserName = "hspecter", DisplayName = "Havery Specter", Email = "hspecter@portaltelemedicina.com.br", Password = "Zxc123?!", CreationDate = DateTime.Now }
            );
            _dbContext.SaveChanges();
            _controller = new LoginController(_dbContext, null);
        }

        [Fact]
        public async Task When_SignIn_Success()
        {
            // ACT
            var viewModel = new SignInViewModel { UserName = "wsmith", Password = "Abc123?!" };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.SignIn(viewModel);

            // ASSERT: Return type
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            var bearerToken = objectResult.Value as string;

            // ASSERT: Has bearer token
            Assert.StartsWith("Bearer ", bearerToken);
        }

        [Fact]
        public async Task When_SignIn_Fails_FieldValidation()
        {
            // ACT
            var viewModel = new SignInViewModel { };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.SignIn(viewModel);

            // ASSERT: Return type
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task When_SignIn_Fails_WrongCredentials()
        {
            // ACT
            var viewModel = new SignInViewModel { UserName = "wsmith", Password = "Ret321!!" };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.SignIn(viewModel);

            // ASSERT: Return type
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task When_SignUp_Success()
        {
            // ACT
            var viewModel = new SignUpViewModel { UserName = "lrodolpho", DisplayName = "Luiz Roberto", Password = "Beto123?!", Email = "luiz.roberto@elune.com.br" };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.SignUp(viewModel);

            // ASSERT: Return type
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_SignUp_Fails_FieldValidation()
        {
            // ACT
            var viewModel = new SignUpViewModel { };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.SignUp(viewModel);

            // ASSERT: Return type
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task When_SignUp_Fails_UserNameTaken()
        {
            // ACT
            var viewModel = new SignUpViewModel { UserName = "wsmith", DisplayName = "Luiz Roberto", Password = "Beto123?!", Email = "luiz.roberto@elune.com.br" };

            IncludeViewStateValidation(viewModel, _controller);

            var result = await _controller.SignUp(viewModel);

            // ASSERT: Return type
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
