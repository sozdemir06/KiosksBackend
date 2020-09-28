using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAnnouncePhotosController : ControllerBase
    {
        private readonly IVehicleAnnouncePhotoService vehicleAnnouncePhotoService;
        public VehicleAnnouncePhotosController(IVehicleAnnouncePhotoService vehicleAnnouncePhotoService)
        {
            this.vehicleAnnouncePhotoService = vehicleAnnouncePhotoService;

        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<VehicleAnnouncePhotoForReturnDto>>> List(int announceId)
        {
            return await vehicleAnnouncePhotoService.GetListAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleAnnouncePhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            return await vehicleAnnouncePhotoService.Create(uploadDto);
        }

        [HttpPost("createforuser")]
        public async Task<ActionResult<VehicleAnnouncePhotoForReturnDto>> CreateForUser([FromForm] FileUploadDto uploadDto)
        {
            return await vehicleAnnouncePhotoService.CreateForPublicAsync(uploadDto);
        }

        [HttpPut]
        public async Task<VehicleAnnouncePhotoForReturnDto> Update(VehicleAnnouncePhotoForCreationDto creationDto)
        {
            return await vehicleAnnouncePhotoService.Update(creationDto);
        }

        [HttpDelete("{photoId}")]
        public async Task<VehicleAnnouncePhotoForReturnDto> Delete(int photoId)
        {
            return await vehicleAnnouncePhotoService.Delete(photoId);
        }


    }
}