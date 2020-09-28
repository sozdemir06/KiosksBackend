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
    public class PublicUserAnnounceController : ControllerBase
    {
        private readonly IPublicUserAnnounceService publicAnnounceservice;
        public PublicUserAnnounceController(IPublicUserAnnounceService publicAnnounceservice)
        {
            this.publicAnnounceservice = publicAnnounceservice;

        }

        [HttpGet("announces/{userId}")]
        public async Task<ActionResult<Pagination<AnnounceForUserDto>>> Announces([FromQuery] AnnounceParams queryParams, int userId)
        {
            return await publicAnnounceservice.GetAnnounceByUserIdAsync(queryParams, userId);
        }

        [HttpGet("homeannounces/{userId}")]
        public async Task<ActionResult<Pagination<HomeAnnounceForUserDto>>> HomeAnnounces([FromQuery] HomeAnnounceParams queryParams, int userId)
        {
            return await publicAnnounceservice.GetHomeAnnounceByUserIdAsync(queryParams, userId);
        }

        [HttpGet("vehicleannounces/{userId}")]
        public async Task<ActionResult<Pagination<VehicleAnnounceForUserDto>>> VehicleAnnounces([FromQuery] VehicleAnnounceParams queryParams, int userId)
        {
            return await publicAnnounceservice.GetVehicleAnnounceByUserIdAsync(queryParams, userId);
        }

       
    }
}