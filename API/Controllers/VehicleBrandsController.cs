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
    public class VehicleBrandsController : ControllerBase
    {
        private readonly IVehicleBrandService vehicleBrandService;
        public VehicleBrandsController(IVehicleBrandService vehicleBrandService)
        {
            this.vehicleBrandService = vehicleBrandService;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<VehicleBrandForReturnDto>>> List([FromQuery]VehicleBrandParams vehicleBrandParams)
        {
            return await vehicleBrandService.GetListAsync(vehicleBrandParams);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleBrandForReturnDto>> Create(VehicleBrandForCreationDto createDto)
        {
            return await vehicleBrandService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<VehicleBrandForReturnDto>> Update(VehicleBrandForCreationDto updateDto)
        {
            return await vehicleBrandService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<VehicleBrandForReturnDto>> Delete(int itemId)
        {
            return await vehicleBrandService.Delete(itemId);
        }
    }
}