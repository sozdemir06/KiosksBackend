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
    public class DegreeManager : IDegreeService
    {
         private readonly IDegreeDal titleDal;
        private readonly IMapper mapper;
        public DegreeManager(IDegreeDal titleDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.titleDal = titleDal;

        }

        public async Task<List<DegreeForListDto>> GetDegreeListAsync()
        {
            
            var titles=await titleDal.GetListAsync();
            if(titles==null)
            {
                 throw new RestException(HttpStatusCode.BadRequest, new { DegreeListNotFound = Messages.TitleListNotFound });
            }

            var titleForReturn=mapper.Map<List<Degree>,List<DegreeForListDto>>(titles);
            return titleForReturn;
        }
    }
}