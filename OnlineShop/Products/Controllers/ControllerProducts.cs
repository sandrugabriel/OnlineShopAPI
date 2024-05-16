using Microsoft.AspNetCore.Mvc;
using OnlineShop.Products.Controllers.interfaces;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Service.interfaces;
using OnlineShop.System.Exceptions;

namespace OnlineShop.Products.Controllers
{
    public class ControllerProducts : ControllerAPIProduct
    {


        private IQueryServiceProduct _queryService;
        private ICommandServiceProduct _commandService;

        public ControllerProducts(IQueryServiceProduct queryService, ICommandServiceProduct commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        public override async Task<ActionResult<List<DtoProductView>>> GetProducts()
        {
            try
            {
                var products = await _queryService.GetAllAsync();

                return Ok(products);

            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoProductView>> GetByName([FromQuery] string name)
        {

            try
            {
                var product = await _queryService.GetByNameAsync(name);
                return Ok(product);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<DtoProductView>> GetById([FromQuery] int id)
        {

            try
            {
                var product = await _queryService.GetByIdAsync(id);
                return Ok(product);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<DtoProductView>> CreateProduct(CreateRequestProduct request)
        {
            try
            {
                var product = await _commandService.Create(request);
                return Ok(product);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoProductView>> UpdateProduct([FromQuery] int id, UpdateRequestProduct request)
        {
            try
            {
                var product = await _commandService.Update(id, request);
                return Ok(product);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoProductView>> DeleteProduct([FromQuery] int id)
        {
            try
            {
                var product = await _commandService.Delete(id);
                return Ok(product);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoProductView>> AddOption([FromQuery] int id, [FromQuery] string name)
        {
            try
            {
                var product = await _commandService.AddOption(id,name);
                return Ok(product);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<DtoProductView>> DeleteOption([FromQuery] int id, [FromQuery] string name)
        {
            try
            {
                var product = await _commandService.DeleteOption(id,name);
                return Ok(product);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
