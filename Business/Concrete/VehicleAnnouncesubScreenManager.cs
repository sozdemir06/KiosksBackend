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
using DataAccess.EntitySpecification.HomeAnnounceSubScreenSpecification;
using DataAccess.EntitySpecification.VehicleAnnounceSubScreenSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class VehicleAnnouncesubScreenManager : IVehicleAnnounceSubScreenService
    {
        private readonly IVehicleAnnounceSubScreenDal vehicleAnnounceSubScreenDal;
        private readonly IMapper mapper;
        private readonly ISubSCreenDal subSCreenDal;
        private readonly IVehicleAnnounceDal vehicleAnnounceDal;
        private readonly IScreenDal screenDal;
        public VehicleAnnouncesubScreenManager(IVehicleAnnounceSubScreenDal vehicleAnnounceSubScreenDal,
            IMapper mapper, IScreenDal screenDal,
            ISubSCreenDal subSCreenDal, IVehicleAnnounceDal vehicleAnnounceDal)
        {
            this.screenDal = screenDal;
            this.vehicleAnnounceDal = vehicleAnnounceDal;
            this.subSCreenDal = subSCreenDal;
            this.mapper = mapper;
            this.vehicleAnnounceSubScreenDal = vehicleAnnounceSubScreenDal;

        }

        [SecuredOperation("Sudo,VehicleAnnounceSubScreens.Create,VehicleAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleAnnounceSubScreenValidator), Priority = 2)]
        public async Task<VehicleAnnounceSubScreenForReturnDto> Create(VehicleAnnounceSubScreenForCreationDto creationDto)
        {
            var checkById = await vehicleAnnounceSubScreenDal.GetAsync(x => x.SubScreenId == creationDto.SubScreenId);
            if (checkById != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.SubScreenAlreadyExist });
            }

            var subScreenFromRepo = await subSCreenDal.GetAsync(x => x.Id == creationDto.SubScreenId);
            if (subScreenFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundSubSCreen });

            }

            var checkAnnounceFromRepo = await vehicleAnnounceDal.GetAsync(x => x.Id == creationDto.VehicleAnnounceId);
            if (checkAnnounceFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            var screenFromRepo = await screenDal.GetAsync(x => x.Id == creationDto.ScreenId);
            if (screenFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundScreen });
            }

            var subScreenForReturn = new VehicleAnnounceSubScreen()
            {
                SubScreenId = subScreenFromRepo.Id,
                ScreenId = screenFromRepo.Id,
                VehicleAnnounceId = checkAnnounceFromRepo.Id,
                SubScreenName=subScreenFromRepo.Name,
                SubScreenPosition=subScreenFromRepo.Position

            };

            var createSubScreen = await vehicleAnnounceSubScreenDal.Add(subScreenForReturn);
            var spec = new VehicleAnnounSubScreenWithSubScreenForReturnSpecification(createSubScreen.Id);
            var getFromRepo = await vehicleAnnounceSubScreenDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<VehicleAnnounceSubScreen, VehicleAnnounceSubScreenForReturnDto>(getFromRepo);
        }

        public async Task<VehicleAnnounceSubScreenForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await vehicleAnnounceSubScreenDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await vehicleAnnounceSubScreenDal.Delete(checkByIdFromRepo);
            return mapper.Map<VehicleAnnounceSubScreen, VehicleAnnounceSubScreenForReturnDto>(checkByIdFromRepo);
        }

        public async Task<List<VehicleAnnounceSubScreenForReturnDto>> GetByAnnounceId(int announceId)
        {
            var spec = new VehicleAnnounSubScreenWithSubScreenSpecification(announceId);
            var getVehicleAnnounceSubScreenByAnnounceId = await vehicleAnnounceSubScreenDal.ListEntityWithSpecAsync(spec);
            if (getVehicleAnnounceSubScreenByAnnounceId == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<VehicleAnnounceSubScreen>, List<VehicleAnnounceSubScreenForReturnDto>>(getVehicleAnnounceSubScreenByAnnounceId);
        }

        [SecuredOperation("Sudo,VehicleAnnounceSubScreens.List", Priority = 1)]
        public async Task<List<VehicleAnnounceSubScreenForReturnDto>> GetListAsync()
        {
            var getListFromRepo = await vehicleAnnounceSubScreenDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<VehicleAnnounceSubScreen>, List<VehicleAnnounceSubScreenForReturnDto>>(getListFromRepo);
        }

        public async Task<VehicleAnnounceSubScreenForReturnDto> Update(VehicleAnnounceSubScreenForCreationDto updateDto)
        {
            var checkByIdFromRepo = await vehicleAnnounceSubScreenDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await vehicleAnnounceSubScreenDal.Update(mapForUpdate);
            return mapper.Map<VehicleAnnounceSubScreen, VehicleAnnounceSubScreenForReturnDto>(updatePhoto);
        }
    }
}