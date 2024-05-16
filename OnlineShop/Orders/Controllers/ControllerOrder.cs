using Microsoft.AspNetCore.Mvc;
using OnlineShop.OrderDetails.Models;
using OnlineShop.Orders.Controllers.interfaces;
using OnlineShop.Orders.Dto;
using OnlineShop.Orders.Service.interfaces;
using OnlineShop.System.Exceptions;

namespace OnlineShop.Orders.Controllers
{
    public class ControllerOrder : ControllerAPIOrder
    {
        IQueryServiceOrder _queryServiceOrder;

        public ControllerOrder(IQueryServiceOrder queryServiceOrder)
        {
            _queryServiceOrder = queryServiceOrder;
        }

        public override async Task<ActionResult<List<DtoOrderView>>> GetOrders()
        {
            try
            {
                var order = await _queryServiceOrder.GetAllAsync();
                return Ok(order);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoOrderView>> GetById([FromQuery] int idOrder)
        {
            try
            {
                DtoOrderView order = await _queryServiceOrder.GetByIdAsync(idOrder);
                return Ok(order);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
