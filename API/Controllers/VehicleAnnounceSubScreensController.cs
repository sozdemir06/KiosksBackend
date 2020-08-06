using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAnnounceSubScreensController : ControllerBase
    {
        private readonly IVehicleAnnounceSubScreenService vehicleAnnouncesubScreenService;
        public VehicleAnnounceSubScreensController(IVehicleAnnounceSubScreenService vehicleAnnouncesubScreenService)
        {
            this.vehicleAnnouncesubScreenService = vehicleAnnouncesubScreenService;

        }

        [HttpPost]
        public async Task<ActionResult<VehicleAnnounceSubScreenForReturnDto>> Create(VehicleAnnounceSubScreenForCreationDto creationDto)
        {
            return await vehicleAnnouncesubScreenService.Create(creationDto);
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<VehicleAnnounceSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await vehicleAnnouncesubScreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<VehicleAnnounceSubScreenForReturnDto>> Delete(int Id)
        {
            return await vehicleAnnouncesubScreenService.Delete(Id);
        }
    }
}