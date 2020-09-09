using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRateService exchangeRateService;
        public ExchangeRatesController(IExchangeRateService exchangeRateService)
        {
            this.exchangeRateService = exchangeRateService;

        }

        [HttpGet]
        public async Task<List<ExchangeRate>> Rates()
        {
            return await exchangeRateService.GetExChangeRateAsync();
        }
    }
}