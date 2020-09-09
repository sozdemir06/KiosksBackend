using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyService currencyService;
        public CurrenciesController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;

        }

        [HttpGet]
        public async Task<ActionResult<List<CurrencyForReturnDto>>> List()
        {
            return await currencyService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CurrencyForReturnDto>> Create(CurrencyForCreationDto createDto)
        {
            return await currencyService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<CurrencyForReturnDto>> Update(CurrencyForCreationDto updateDto)
        {
            return await currencyService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<CurrencyForReturnDto>> Delete(int itemId)
        {
            return await currencyService.Delete(itemId);
        }
    }
}