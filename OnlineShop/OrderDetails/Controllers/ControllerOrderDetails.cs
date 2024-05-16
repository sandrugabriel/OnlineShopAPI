using Microsoft.AspNetCore.Mvc;
using OnlineShop.OrderDetails.Controllers.interfaces;
using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;
using OnlineShop.OrderDetails.Service.interfaces;
using OnlineShop.System.Exceptions;

namespace OnlineShop.OrderDetailDetails.Controllers
{
    public class ControllerOrderDetailDetails : ControllerAPIOrderDetail
    {
        IQueryServiceOrderDetail _queryServiceOrderDetail;

        public ControllerOrderDetailDetails(IQueryServiceOrderDetail queryServiceOrderDetail)
        {
            _queryServiceOrderDetail = queryServiceOrderDetail;
        }

        public override async Task<ActionResult<List<DtoOrderDetailView>>> GetOrderDetails()
        {
            try
            {
                var orderDetail = await _queryServiceOrderDetail.GetAllAsync();
                return Ok(orderDetail);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoOrderDetailView>> GetById([FromQuery] int idOrderDetail)
        {
            try
            {
                OrderDetail orderDetail = await _queryServiceOrderDetail.GetById(idOrderDetail);
                return Ok(orderDetail);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
