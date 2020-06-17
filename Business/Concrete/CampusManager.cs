using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class CampusManager : ICampusService
    {
        private readonly ICampusDal campusDal;
        private readonly IMapper mapper;
        public CampusManager(ICampusDal campusDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.campusDal = campusDal;

        }
        public async Task<List<CampusForListDto>> GetCampusListAsync()
        {
            var campuses = await campusDal.GetListAsync();
            if (campuses == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { CampusNotFound = Messages.CampusListNotFound });
            }

            var campusForReturn=mapper.Map<List<Campus>,List<CampusForListDto>>(campuses);

            return campusForReturn;
        }
    }
}