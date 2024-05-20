using Microsoft.AspNetCore.Mvc;
using OnlineShop.Customers.Controllers.interfaces;
using OnlineShop.Customers.Dto;
using OnlineShop.Customers.Services.interfaces;
using OnlineShop.OrderDetails.Models;
using OnlineShop.System.Exceptions;
using System.Xml.Linq;

namespace OnlineShop.Customers.Controllers
{
    public class ControllerCustomer : ControllerAPICustomer
    {

        IQueryServiceCustomer _queryServiceCustomer;
        ICommandServiceCustomer _commandServiceCustomer;

        public ControllerCustomer(IQueryServiceCustomer queryServiceCustomer, ICommandServiceCustomer commandServiceCustomer)
        {
            _queryServiceCustomer = queryServiceCustomer;
            _commandServiceCustomer = commandServiceCustomer;
        }

        public override async Task<ActionResult<List<DtoCustomerView>>> GetAllAsync()
        {
            try
            {
                var customer = await _queryServiceCustomer.GetAllAsync();
                return Ok(customer);
            } catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoCustomerView>> GetByIdAsync([FromQuery] int id)
        {
            try
            {
                var customer = await _queryServiceCustomer.GetById(id);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoCustomerView>> GetByNameAsync([FromQuery] string name)
        {
            try
            {
                var customer = await _queryServiceCustomer.GetByName(name);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoCustomerView>> CreateCustomer([FromBody] CreateRequestCustomer createRequestCustomer)
        {
            try
            {
                var customer = await _commandServiceCustomer.CreateCustomer(createRequestCustomer);
                return Ok(customer);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoCustomerView>> UpdateCustomer([FromQuery] int id, [FromBody] UpdateRequestCustomer updateRequest)
        {
            try
            {
                var customer = await _commandServiceCustomer.UpdateCustomer(id,updateRequest);
                return Ok(customer);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoCustomerView>> DeleteCustomer([FromQuery] int id)
        {
            try
            {
                var customer = await _commandServiceCustomer.DeleteCustomer(id);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoCustomerView>> AddProductToOrder([FromQuery] int idCurtomer, [FromQuery] string name, [FromQuery] string option, [FromQuery] int quantity)
        {

            try
            {
                var customer = await _commandServiceCustomer.AddProductToOrder(idCurtomer, name,option,quantity);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoCustomerView>> DeleteOrder([FromQuery] int id, [FromQuery] int idOrder)
        {
            try
            {
                var customer = await _commandServiceCustomer.DeleteOrder(id,idOrder);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoCustomerView>> DeleteProductToOrder([FromQuery] int id, [FromQuery] string name, [FromQuery] string option)
        {

            try
            {
                var customer = await _commandServiceCustomer.DeleteProductToOrder(id, name,option);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<SendOrderView>> SaveOrder([FromBody] SendOrderRequest sendOrderRequest)
        {
            try
            {
                var sendOrder = await _commandServiceCustomer.SaveOrder(sendOrderRequest);
                return Ok(sendOrder);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
