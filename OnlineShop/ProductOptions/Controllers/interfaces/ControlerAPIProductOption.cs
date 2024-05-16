using Microsoft.AspNetCore.Mvc;
using OnlineShop.Options.Models;
using OnlineShop.ProductOptions.Model;
using System.Runtime.Intrinsics;

namespace OnlineShop.ProductOptions.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControlerAPIProductOption : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<ProductOption>))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<List<ProductOption>>> GetAllAsync();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(ProductOption))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<ProductOption>> GetByIdAsync([FromQuery] int id);


    }
}
