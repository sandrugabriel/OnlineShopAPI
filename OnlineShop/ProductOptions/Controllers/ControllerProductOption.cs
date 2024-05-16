using Microsoft.AspNetCore.Mvc;
using OnlineShop.Options.Models;
using OnlineShop.Options.Service.interfaces;
using OnlineShop.ProductOptions.Controllers.interfaces;
using OnlineShop.ProductOptions.Model;
using OnlineShop.System.Exceptions;

namespace OnlineShop.ProductOptions.Controllers
{
    public class ControllerProductOption : ControlerAPIProductOption
    {

        IQueryServiceOption _queryServiceOption;
        ICommandServiceOption _commandServiceOption;

        public ControllerProductOption(IQueryServiceOption queryServiceOption, ICommandServiceOption commandServiceOption)
        {
            _queryServiceOption = queryServiceOption;
            _commandServiceOption = commandServiceOption;
        }

        public override async Task<ActionResult<List<ProductOption>>> GetAllAsync()
        {
            try
            {
                var customer = await _queryServiceOption.GetAllAsync();
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ProductOption>> GetByIdAsync([FromQuery] int id)
        {
            try
            {
                var customer = await _queryServiceOption.GetByIdAsync(id);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
