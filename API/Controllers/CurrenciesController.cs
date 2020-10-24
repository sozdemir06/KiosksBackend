using System.Collections.Generic;
using System.Threading.Tasks;
using API.Hubs;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyService currencyService;
        private readonly IExchangeRateService exchangeRateService;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        public CurrenciesController(ICurrencyService currencyService,
        IExchangeRateService exchangeRateService,
         IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
            this.currencyService = currencyService;
            this.exchangeRateService = exchangeRateService;
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
            var currency= await currencyService.Update(updateDto);
            var onlineScreensConnectionId=await onlineScreenService.GetAllOnlineScreenConnectionId();
            if(onlineScreensConnectionId!=null && onlineScreensConnectionId.Length!=0)
            {
                var currencyForKisosk=await exchangeRateService.GetExChangeRateAsync();
                if(currencyForKisosk!=null)
                {
                    await kiosksHub.Clients.Clients(onlineScreensConnectionId).SendAsync("ReceiveExchangeRate",currencyForKisosk);
                }
            }
            return currency;
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<CurrencyForReturnDto>> Delete(int itemId)
        {
            return await currencyService.Delete(itemId);
        }
    }
}