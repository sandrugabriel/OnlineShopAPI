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
        [ProducesResponseType(statusCode: 200, type: typeof(List<DtoProductView>))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<List<DtoProductView>>> GetProducts();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoProductView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> GetById([FromQuery] int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoProductView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> GetByName([FromQuery] string name);

        [HttpPost("CreateProduct")]
        [ProducesResponseType(statusCode: 201, type: typeof(DtoProductView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> CreateProduct(CreateRequestProduct request);

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoProductView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> UpdateProduct([FromQuery] int id, UpdateRequestProduct request);

        [HttpDelete("DeleteProduct")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoProductView))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> DeleteProduct([FromQuery] int id);


        [HttpPost("AddOption")]
        [ProducesResponseType(statusCode: 201, type: typeof(DtoProductView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> AddOption([FromQuery] int id, [FromQuery] string name);

        [HttpDelete("DeleteOption")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoProductView))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<DtoProductView>> DeleteOption([FromQuery] int id, [FromQuery] string name);
    }
}
