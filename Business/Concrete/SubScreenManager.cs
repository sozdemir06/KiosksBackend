using System.Collections.Generic;
using System.Linq;
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
    public class SubScreenManager : ISubScreenService
    {
        private readonly ISubSCreenDal subScreenDal;
        private readonly IMapper mapper;
        private readonly IScreenDal screenDal;
        public SubScreenManager(ISubSCreenDal subScreenDal, IMapper mapper, IScreenDal screenDal)
        {
            this.screenDal = screenDal;
            this.mapper = mapper;
            this.subScreenDal = subScreenDal;


        }

        [SecuredOperation("Sudo,SubScreens.Create", Priority = 1)]
        [ValidationAspect(typeof(SubScreenValidator), Priority = 2)]
        public async Task<SubScreenForReturnDto> Create(SubScreenForCreationDto createDto)
        {
            var checkByNameFormRepo = await subScreenDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByNameFormRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var getByScreenId = await subScreenDal.GetListAsync(x => x.ScreenId == createDto.ScreenId);
            if (getByScreenId.Count() >= 3)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { ExceedScreenSize = "En fazla 3 alt ekran olmalı..." });
            }

            if(createDto.Height>100 || createDto.Width>100)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { ExceedScreenSize = "Alt Ekranın Genişlik/Yükseklik'lerinin toplamı en fazla 100 olmalı" });
            }

            var mapForCreate = mapper.Map<SubScreen>(createDto);
            var saveToDb = await subScreenDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<SubScreen, SubScreenForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,SubScreens.Delete", Priority = 1)]
        public async Task<SubScreenForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await subScreenDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await subScreenDal.Delete(checkByIdFromRepo);
            return mapper.Map<SubScreen, SubScreenForReturnDto>(checkByIdFromRepo);

        }

        public async Task<List<SubScreenForReturnDto>> GetByScreenId(int screenId)
        {
            var subScreensByScreenId = await subScreenDal.GetListAsync(x => x.ScreenId == screenId);
            if (subScreensByScreenId == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<SubScreen>, List<SubScreenForReturnDto>>(subScreensByScreenId);
        }

        [SecuredOperation("Sudo,SubScreens.List", Priority = 1)]
        public async Task<List<SubScreenForReturnDto>> GetListAsync()
        {
            var subScrens = await subScreenDal.GetListAsync();
            if (subScrens == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<SubScreen>, List<SubScreenForReturnDto>>(subScrens);
        }

        [SecuredOperation("Sudo,SubScreens.Update", Priority = 1)]
        [ValidationAspect(typeof(SubScreenValidator), Priority = 2)]
        public async Task<SubScreenForReturnDto> Update(SubScreenForCreationDto updateDto)
        {
            var checkById = await subScreenDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }            
           
            if(updateDto.Height>100 || updateDto.Width>100)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { ExceedScreenSize = "Alt Ekranın Genişlik/Yükseklik'lerinin toplamı en fazla 100 olmalı" });
            }
            

            var mapForUpdate = mapper.Map(updateDto, checkById);
            var updateToDb = await subScreenDal.Update(mapForUpdate);
            return mapper.Map<SubScreen, SubScreenForReturnDto>(mapForUpdate);
        }
    }
}