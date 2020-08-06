using System.Threading.Tasks;
using Business.Abstract;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAnnounceController : ControllerBase
    {
        private readonly IVehicleAnnounceService vehicleAnnounceService;
        public VehicleAnnounceController(IVehicleAnnounceService vehicleAnnounceService)
        {
            this.vehicleAnnounceService = vehicleAnnounceService;

        }

        [HttpGet]
        public async Task<Pagination<VehicleAnnounceForReturnDto>> List([FromQuery]VehicleAnnounceParams queryParams)
        {
           return await vehicleAnnounceService.GetListAsync(queryParams); 
        }

        [HttpPost]
        public async Task<ActionResult<VehicleAnnounceForReturnDto>> Create(VehicleAnnounceForCreationDto creationDto)
        {
            return await vehicleAnnounceService.Create(creationDto);
        }

        [HttpPut]
        public async Task<ActionResult<VehicleAnnounceForReturnDto>> Update(VehicleAnnounceForCreationDto creationDto)
        {
            return await vehicleAnnounceService.Update(creationDto);
        }

        [HttpPut("publish")]
        public async Task<ActionResult<VehicleAnnounceForReturnDto>> Publish(VehicleAnnounceForCreationDto creationDto)
        {
            return await vehicleAnnounceService.Publish(creationDto);
        }

        [HttpGet("detail/{announceId}")]
        public async Task<ActionResult<VehicleAnnounceForDetailDto>> Detail(int announceId)
        {
            return await vehicleAnnounceService.GetDetailAsync(announceId);
        }
    }
}