using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalTelemedicina.DomainService;
using PortalTelemedicina.DomainService.Interfaces;
using PortalTelemedicina.Repository;
using PortalTelemedicina.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTelemedicina.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class ProductController : Controller
    {
        private IProductDomainService _domainService;

        public ProductController(ApplicationContext context)
        {
            _domainService = new ProductDomainService(context);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<IActionResult> Products(ProductSearchViewModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var products = await _domainService.Get(data.Name, data.Description, data.Price, data.StartDate, data.EndDate, data.OrderBy, data.OrderType);
                var query = from product in products
                            select new ProductSearchResultViewModel
                            {
                                ProductId = product.ProductId,
                                Name = product.Name,
                                Description = product.Description,
                                Price = product.Price,
                                CreationDate = product.CreationDate
                            };

                return new OkObjectResult(query.ToList());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[controller]")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]ProductCreateViewModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _domainService.Create(data.Name, data.Description, data.Price);

                return Ok("Product created.");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("[controller]/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody]ProductUpdateViewModel value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var success = await _domainService.Update(id, value.Name, value.Description, value.Price);

                if (success)
                    return Ok("Product updated.");
                else
                    return NotFound("Product not found.");
            }
            catch
            {
                return BadRequest("Unable to update the product.");
            }
        }
    }
}