using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubScreensController : ControllerBase
    {
        private readonly ISubScreenService subScreenService;
        public SubScreensController(ISubScreenService subScreenService)
        {
            this.subScreenService = subScreenService;

        }


        [HttpGet]
        public async Task<ActionResult<List<SubScreenForReturnDto>>> List()
        {
            return await subScreenService.GetListAsync();
        }

      
        [HttpPost]
        public async Task<ActionResult<SubScreenForReturnDto>> Create(SubScreenForCreationDto createDto)
        {
            return await subScreenService.Create(createDto);
        }


    
        [HttpPut]
        public async Task<ActionResult<SubScreenForReturnDto>> Update(SubScreenForCreationDto updateDto)
        {
            return await subScreenService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<SubScreenForReturnDto>> Delete(int itemId)
        {
            return await subScreenService.Delete(itemId);
        }

        [HttpGet("{screenId}")]
        public async  Task<ActionResult<List<SubScreenForReturnDto>>> GetSubScreenListByScreenId(int screenId)
        {
            return await subScreenService.GetByScreenId(screenId);
        }
    }
}