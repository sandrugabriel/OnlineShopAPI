using Microsoft.AspNetCore.Mvc;
using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;
using OnlineShop.Orders.Dto;

namespace OnlineShop.OrderDetails.Controllers.interfaces
{

    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPIOrderDetail : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<OrderDetail>))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<List<DtoOrderDetailView>>> GetOrderDetails();


        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(OrderDetail))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<DtoOrderDetailView>> GetById([FromQuery] int idOrder);


    }
}
