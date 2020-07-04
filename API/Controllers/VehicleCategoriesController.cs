using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleCategoriesController : ControllerBase
    {
        private readonly IVehicleCategoryService vehicleCategoryService;
        public VehicleCategoriesController(IVehicleCategoryService vehicleCategoryService)
        {
            this.vehicleCategoryService = vehicleCategoryService;

        }


        [HttpGet]
        public async Task<ActionResult<List<VehicleCategoryForReturnDto>>> List()
        {
            return await vehicleCategoryService.GetListAsync();
        }

         [HttpPost]
        public async Task<ActionResult<VehicleCategoryForReturnDto>> Create(VehicleCategoryForCreationDto createDto)
        {
            return await vehicleCategoryService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<VehicleCategoryForReturnDto>> Update(VehicleCategoryForCreationDto updateDto)
        {
            return await vehicleCategoryService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<VehicleCategoryForReturnDto>> Delete(int itemId)
        {
            return await vehicleCategoryService.Delete(itemId);
        }
    }
}