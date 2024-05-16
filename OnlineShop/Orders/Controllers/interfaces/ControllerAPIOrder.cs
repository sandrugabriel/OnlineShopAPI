using Microsoft.AspNetCore.Mvc;
using OnlineShop.Orders.Dto;

namespace OnlineShop.Orders.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPIOrder : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode:200,type:typeof(List<DtoOrderView>))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<List<DtoOrderView>>> GetOrders();


        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(DtoOrderView))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoOrderView>> GetById([FromQuery]int idOrder);



    }
}
