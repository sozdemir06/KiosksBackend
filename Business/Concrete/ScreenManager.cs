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
using DataAccess.EntitySpecification.ScreenSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class ScreenManager : IScreenService
    {
        private readonly IScreenDal screenDal;
        private readonly IMapper mapper;
        private readonly ISubSCreenDal subSCreenDal;

        public ScreenManager(IScreenDal screenDal, ISubSCreenDal subSCreenDal, IMapper mapper)
        {
            this.subSCreenDal = subSCreenDal;
            this.mapper = mapper;
            this.screenDal = screenDal;

        }

        [SecuredOperation("Sudo,Screens.Create,Screens.All", Priority = 1)]
        [ValidationAspect(typeof(ScreenValidator), Priority = 2)]
        public async Task<ScreenForReturnDto> Create(ScreenForCreationDto createDto)
        {
            var checkByNameFromRepo = await screenDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByNameFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var screenForCreate = mapper.Map<Screen>(createDto);
            var screenToSave = await screenDal.Add(screenForCreate);

            List<SubScreenForCreationDto> subScreen = new List<SubScreenForCreationDto>()
            {
                new SubScreenForCreationDto()
                {
                    Name=screenToSave.Position=="Vertical"?screenToSave.Name+" Üst":screenToSave.Name+" Sol",
                    Position=screenToSave.Position=="Vertical"?"Top":"Left",
                    Height=screenToSave.Position=="Vertical"?30:0,
                    Width=screenToSave.Position=="Vertical"?0:100,
                    ScreenId=screenToSave.Id,
                    Status=true,

                },
                new SubScreenForCreationDto()
                {
                    Name=screenToSave.Name+" Orta",
                    Position=screenToSave.Position=="Vertical"?"VMiddle":"HMiddle",
                    Height=screenToSave.Position=="Vertical"?30:0,
                    Width=screenToSave.Position=="Vertical"?0:30,
                    ScreenId=screenToSave.Id,
                    Status=false

                },
                new SubScreenForCreationDto()
                {
                    Name=screenToSave.Position=="Vertical"?screenToSave.Name+" Alt":screenToSave.Name+" Sağ",
                    Position=screenToSave.Position=="Vertical"?"Bottom":"Right",
                    Height=screenToSave.Position=="Vertical"?30:0,
                    Width=screenToSave.Position=="Vertical"?0:30,
                    ScreenId=screenToSave.Id,
                    Status=false,

                }
            };

            var subScreenForCreate = mapper.Map<List<SubScreen>>(subScreen);
            var subScreenToSave = await subSCreenDal.AddRange(subScreenForCreate);
            var screenForReturn = mapper.Map<Screen, ScreenForReturnDto>(screenToSave);
            return screenForReturn;

        }

        [SecuredOperation("Sudo,Screens.Delete,Screens.All", Priority = 1)]
        public async Task<ScreenForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await screenDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await screenDal.Delete(checkByIdFromRepo);
            var mapForReturn = mapper.Map<Screen, ScreenForReturnDto>(checkByIdFromRepo);
            return mapForReturn;

        }

        //[SecuredOperation("Sudo,Screens.List,Screens.All", Priority = 1)]
        public async Task<List<ScreenForReturnDto>> GetListAsync()
        {

           
            var spec=new ScreenWithSubScreenSpecification();
             var getScreenList = await screenDal.ListEntityWithSpecAsync(spec);

            if (getScreenList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var screenForReturn = mapper.Map<List<Screen>, List<ScreenForReturnDto>>(getScreenList);
            return screenForReturn;
        }

        [SecuredOperation("Sudo,Screens.Update,Screens.All", Priority = 1)]
        [ValidationAspect(typeof(ScreenValidator), Priority = 2)]
        public async Task<ScreenForReturnDto> Update(ScreenForCreationDto updateDto)
        {
            var checkByIdFromRepo = await screenDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            if (updateDto.Position.ToLower() != checkByIdFromRepo.Position.ToLower())
            {
                var subs = await subSCreenDal.GetListAsync(x => x.ScreenId == checkByIdFromRepo.Id);

                if (updateDto.Position.ToLower() == "vertical" && checkByIdFromRepo.Position.ToLower() == "horizontal")
                {
                    foreach (var item in subs)
                    {
                        if (item.Position == "Left")
                        {
                            item.Name = checkByIdFromRepo.Name + " Üst";
                            item.Position = "Top";
                        }

                        if (item.Position == "Middle")
                        {
                            item.Name = checkByIdFromRepo.Name + " Orta";
                            item.Position = "Middle";
                        }

                        if (item.Position == "Right")
                        {
                            item.Name = checkByIdFromRepo.Name + " Alt";
                            item.Position = "Bottom";
                        }

                    }

                    await subSCreenDal.UpdateRange(subs);
                }

                if (updateDto.Position.ToLower() == "horizontal" && checkByIdFromRepo.Position.ToLower() == "vertical")
                {
                    foreach (var item in subs)
                    {
                        if (item.Position == "Top")
                        {
                            item.Name = checkByIdFromRepo.Name + " Sol";
                            item.Position = "Left";

                        }
                        if (item.Position == "Middle")
                        {
                            item.Name = checkByIdFromRepo.Name + " Orta";
                            item.Position = "Middle";

                        }
                        if (item.Position == "Bottom")
                        {
                            item.Name = checkByIdFromRepo.Name + " Sağ";
                            item.Position = "Right";
                        }
                    }
                    await subSCreenDal.UpdateRange(subs);
                }

            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            await screenDal.Update(mapForUpdate);
            return mapper.Map<Screen, ScreenForReturnDto>(mapForUpdate);

        }
    }
}