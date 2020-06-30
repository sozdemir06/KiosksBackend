using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberOfRoomsController : ControllerBase
    {
        private readonly INumberOfRoomService numberOfRoomService;
        public NumberOfRoomsController(INumberOfRoomService numberOfRoomService)
        {
            this.numberOfRoomService = numberOfRoomService;

        }

        [HttpGet]
        public async Task<ActionResult<List<NumberOfRoomForReturnDto>>> List()
        {
            return await numberOfRoomService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<NumberOfRoomForReturnDto>> Create(NumberOfRoomForCreateOrUpdateDto createDto)
        {
            return await numberOfRoomService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<NumberOfRoomForReturnDto>> Update(NumberOfRoomForCreateOrUpdateDto updateDto)
        {
            return await numberOfRoomService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<NumberOfRoomForReturnDto>> Delete(int itemId)
        {
            return await numberOfRoomService.Delete(itemId);
        }
    }
}