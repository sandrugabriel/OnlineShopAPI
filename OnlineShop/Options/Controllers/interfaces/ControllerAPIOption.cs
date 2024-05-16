using Microsoft.AspNetCore.Mvc;
using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;

namespace OnlineShop.Options.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPIOption : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Option>))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<List<Option>>> GetAllAsync();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(Option))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<Option>> GetByIdAsync([FromQuery] int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(Option))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<Option>> GetByNameAsync([FromQuery] string name);


        [HttpPost("CreateOption")]
        [ProducesResponseType(statusCode: 201, type: typeof(Option))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<Option>> CreateOption([FromBody] CreateRequestOption createRequestOption);

        [HttpPut("UpdateOption")]
        [ProducesResponseType(statusCode: 200, type: typeof(Option))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<Option>> UpdateOption([FromQuery] int id, [FromBody] UpdateRequestOption updateRequest);

        [HttpDelete("DeleteOption")]
        [ProducesResponseType(statusCode: 200, type: typeof(Option))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<Option>> DeleteOption([FromQuery] int id);


    }
}
