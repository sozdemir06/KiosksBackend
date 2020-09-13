using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using Core.Entities.Concrete;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class LiveTvListManager : ILiveTvListService
    {
        private readonly IMapper mapper;
        private readonly ILiveTvListDal liveTvListDal;

        public LiveTvListManager(ILiveTvListDal liveTvListDal, IMapper mapper)
        {
            this.liveTvListDal = liveTvListDal;
            this.mapper = mapper;

        }
        [SecuredOperation("Sudo,LiveTvList.Create,LiveTvBroadCasts.All", Priority = 1)]
        [ValidationAspect(typeof(LiveTvListValidator), Priority = 2)]
        public async Task<LiveTvListForReturnDto> Create(LiveTvListForCreationDto createDto)
        {
            var checkByName = await liveTvListDal.GetAsync(x => x.TvName.ToLower() == createDto.TvName.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<LiveTvList>(createDto);
            var saveToDb = await liveTvListDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<LiveTvList, LiveTvListForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,LiveTvList.Delete,LiveTvBroadCasts.All", Priority = 1)]
        public async Task<LiveTvListForReturnDto> Delete(int Id)
        {
            var checkFromDb = await liveTvListDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await liveTvListDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<LiveTvList, LiveTvListForReturnDto>(checkFromDb);
            return mapForReturn;
        }


        [SecuredOperation("Sudo,LiveTvList.List,LiveTvBroadCasts.All", Priority = 1)]
        public async Task<List<LiveTvListForReturnDto>> GetListAsync()
        {
            var buildingsAgeList = await liveTvListDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<LiveTvList>, List<LiveTvListForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,LiveTvList.Create,LiveTvBroadCasts.All", Priority = 1)]
        [ValidationAspect(typeof(LiveTvListValidator), Priority = 2)]
        public async Task<LiveTvListForReturnDto> Update(LiveTvListForCreationDto updateDto)
        {
             var checkById = await liveTvListDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await liveTvListDal.Update(mapForUpdate);
            return mapper.Map<LiveTvList, LiveTvListForReturnDto>(mapForUpdate);
        }
    }
}