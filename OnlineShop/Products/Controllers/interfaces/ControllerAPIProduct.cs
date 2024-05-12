using Microsoft.AspNetCore.Mvc;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;

namespace OnlineShop.Products.Controllers.interfaces
{

    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPIProduct : ControllerBase
    {
        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Product>))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<List<DtoProductView>>> GetProducts();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(Product))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> GetById([FromQuery] int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(Product))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> GetByName([FromQuery] string name);

        [HttpPost("CreateProduct")]
        [ProducesResponseType(statusCode: 201, type: typeof(Product))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> CreateProduct(CreateRequestProduct request);

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(statusCode: 200, type: typeof(Product))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> UpdateProduct([FromQuery] int id, UpdateRequestProduct request);

        [HttpDelete("DeleteProduct")]
        [ProducesResponseType(statusCode: 200, type: typeof(Product))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> DeleteProduct([FromQuery] int id);
    }
}
