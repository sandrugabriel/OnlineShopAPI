using Microsoft.AspNetCore.Mvc;
using OnlineShop.Options.Dto;
using OnlineShop.Options.Controllers.interfaces;
using OnlineShop.System.Exceptions;
using OnlineShop.Options.Service.interfaces;
using OnlineShop.Options.Models;

namespace OnlineShop.Options.Controllers
{
    public class ControllerOption : ControllerAPIOption
    {


        IQueryServiceOption _queryServiceOption;
        ICommandServiceOption _commandServiceOption;

        public ControllerOption(IQueryServiceOption queryServiceOption, ICommandServiceOption commandServiceOption)
        {
            _queryServiceOption = queryServiceOption;
            _commandServiceOption = commandServiceOption;
        }

        public override async Task<ActionResult<List<Option>>> GetAllAsync()
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

        public override async Task<ActionResult<Option>> GetByIdAsync([FromQuery] int id)
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

        public override async Task<ActionResult<Option>> GetByNameAsync([FromQuery] string name)
        {
            try
            {
                var customer = await _queryServiceOption.GetByNameAsync(name);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Option>> CreateOption([FromBody] CreateRequestOption createRequestOption)
        {
            try
            {
                var customer = await _commandServiceOption.CreateOption(createRequestOption);
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

        public override async Task<ActionResult<Option>> UpdateOption([FromQuery] int id, [FromBody] UpdateRequestOption updateRequest)
        {
            try
            {
                var customer = await _commandServiceOption.UpdateOption(id, updateRequest);
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

        public override async Task<ActionResult<Option>> DeleteOption([FromQuery] int id)
        {
            try
            {
                var customer = await _commandServiceOption.DeleteOption(id);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
