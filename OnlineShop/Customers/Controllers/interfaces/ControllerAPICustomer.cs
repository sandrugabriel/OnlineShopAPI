using Microsoft.AspNetCore.Mvc;
using OnlineShop.Customers.Dto;

namespace OnlineShop.Customers.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPICustomer : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200,type:typeof(List<DtoCustomerView>))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<List<DtoCustomerView>>> GetAllAsync();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoCustomerView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoCustomerView>> GetByIdAsync([FromQuery]int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoCustomerView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoCustomerView>> GetByNameAsync([FromQuery] string name);


        [HttpPost("CreateCustomer")]
        [ProducesResponseType(statusCode: 201, type: typeof(DtoCustomerView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoCustomerView>> CreateCustomer([FromBody] CreateRequestCustomer createRequestCustomer);

        [HttpPut("UpdateCustomer")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoCustomerView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<DtoCustomerView>> UpdateCustomer([FromQuery] int id, [FromBody]UpdateRequestCustomer updateRequest);

        [HttpDelete("DeleteCustomer")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoCustomerView))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<DtoCustomerView>> DeleteCustomer([FromQuery] int id);

        [HttpPost("AddProductToOrder")]
        [ProducesResponseType(statusCode: 201, type: typeof(DtoCustomerView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoCustomerView>> AddProductToOrder([FromQuery]int idCurtomer, [FromQuery] string name, [FromQuery] string option, [FromQuery] int quantity);

        [HttpDelete("DeleteOrder")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoCustomerView))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<DtoCustomerView>> DeleteOrder([FromQuery] int id, [FromQuery] int idOrder);

        [HttpDelete("DeleteProductToOrder")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoCustomerView))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<DtoCustomerView>> DeleteProductToOrder([FromQuery] int id, [FromQuery] string name, [FromQuery]string option);


    }
}
