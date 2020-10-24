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
    public class CitiesController : ControllerBase
    {
        private readonly ICityService cityService;
        private readonly IWheatherForeCastService wheatherForeCastService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;
        public CitiesController(ICityService cityService,
        IWheatherForeCastService wheatherForeCastService,
        IHubContext<KiosksHub> kiosksHub, 
        IOnlineScreenService onlineScreenService)
        {
            this.onlineScreenService = onlineScreenService;
            this.cityService = cityService;
            this.wheatherForeCastService = wheatherForeCastService;
            this.kiosksHub = kiosksHub;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityForReturnDto>>> List()
        {
            return await cityService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CityForReturnDto>> Create(CityForCreationDto createDto)
        {
            return await cityService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<CityForReturnDto>> Update(CityForCreationDto updateDto)
        {
            var city = await cityService.Update(updateDto);
            var onlineScreens=await onlineScreenService.GetAllOnlineScreenConnectionId();
            if(onlineScreens!=null && onlineScreens.Length!=0)
            {
                var foreCastsForKiosks=await wheatherForeCastService.WheatherForeCastsAsync();
                if(foreCastsForKiosks!=null)
                {
                    await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveWheatherForeCast",foreCastsForKiosks);
                }
            }
            return city;
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<CityForReturnDto>> Delete(int itemId)
        {
            return await cityService.Delete(itemId);
        }
    }
}