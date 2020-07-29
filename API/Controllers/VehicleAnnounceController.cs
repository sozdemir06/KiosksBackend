using System.Collections.Generic;
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
    }
}