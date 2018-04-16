using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalTelemedicina.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PortalTelemedicina.Tests.Extensions
{
    public class BaseTestController
    {
        /// <summary>
        /// Ensure that the ModelState.IsValid will be test when called from the test project.
        /// </summary>
        public void IncludeViewStateValidation(object viewModel, Controller controller)
        {
            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(viewModel, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }

        /// <summary>
        /// Create a new InMemory context to mock repository data.
        /// </summary>
        /// <returns>New context.</returns>
        public ApplicationContext CreateInMemoryContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseInMemoryDatabase("InMemoryDB");
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
