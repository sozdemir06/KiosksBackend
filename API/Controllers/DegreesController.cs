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
    public class DegreesController : ControllerBase
    {
        private readonly IDegreeService degreeService;
        public DegreesController(IDegreeService degreeService)
        {
            this.degreeService = degreeService;

        }

       [HttpGet]
        public async Task<ActionResult<Pagination<DegreeForReturnDto>>> List([FromQuery]DegreeParams vehicleBrandParams)
        {
            return await degreeService.GetListAsync(vehicleBrandParams);
        }

        [HttpPost]
        public async Task<ActionResult<DegreeForReturnDto>> Create(DegreeForCreationDto createDto)
        {
            return await degreeService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<DegreeForReturnDto>> Update(DegreeForCreationDto updateDto)
        {
            return await degreeService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<DegreeForReturnDto>> Delete(int itemId)
        {
            return await degreeService.Delete(itemId);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<DegreeForReturnDto>>> GetByCategory(int categoryId)
        {
            return await degreeService.GetListWithoutPaging(categoryId);
        }
    }
}