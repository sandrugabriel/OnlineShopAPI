using Microsoft.AspNetCore.Mvc;
using OnlineShop.Options.Models;
using OnlineShop.Options.Service.interfaces;
using OnlineShop.ProductOptions.Controllers.interfaces;
using OnlineShop.ProductOptions.Model;
using OnlineShop.ProductOptions.Service.interfaces;
using OnlineShop.System.Exceptions;

namespace OnlineShop.ProductOptions.Controllers
{
    public class ControllerProductOption : ControlerAPIProductOption
    {

        IQueryServiceProductOption _queryServiceOption;

        public ControllerProductOption(IQueryServiceProductOption queryServiceOption)
        {
            _queryServiceOption = queryServiceOption;
        }

        public override async Task<ActionResult<List<ProductOption>>> GetAllAsync()
        {
            try
            {
                var productOption = await _queryServiceOption.GetAllAsync();
                return Ok(productOption);
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
                var productOption = await _queryServiceOption.GetByIdAsync(id);
                return Ok(productOption);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
