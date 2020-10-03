using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Photos;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.UsersSpecification;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class PublicManager : IPublicService
    {
        private readonly IMapper mapper;

        private readonly IAnnounceDal announceDal;
        private readonly IHomeAnnounceDal homeAnnounceDal;
        private readonly IUserDal userDal;
        private readonly IUploadFile upload;
        private readonly IUserPhotoDal userPhotoDal;
        private readonly IDegreeDal degreeDal;
        private readonly IUserService userService;
        private readonly ICampusDal campusDal;
        private readonly IDepartmentDal departmentDal;
        private readonly IVehicleAnnounceDal vehicleAnnounceDal;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly INewsDal newsDal;
        private readonly IFoodMenuDal foodMenuDal;
        public PublicManager(IMapper mapper,
            INewsDal newsDal,
            IFoodMenuDal foodMenuDal,
            IHomeAnnounceDal homeAnnounceDal, IUserDal userDal, IUploadFile upload, IUserPhotoDal userPhotoDal,
            IDegreeDal degreeDal, IUserService userService,
            ICampusDal campusDal,
            IDepartmentDal departmentDal,
            IVehicleAnnounceDal vehicleAnnounceDal,
            IHttpContextAccessor httpContextAccessor,
            IAnnounceDal announceDal)
        {
            this.vehicleAnnounceDal = vehicleAnnounceDal;
            this.httpContextAccessor = httpContextAccessor;
            this.homeAnnounceDal = homeAnnounceDal;
            this.userDal = userDal;
            this.upload = upload;
            this.userPhotoDal = userPhotoDal;
            this.degreeDal = degreeDal;
            this.userService = userService;
            this.campusDal = campusDal;
            this.departmentDal = departmentDal;
            this.announceDal = announceDal;
            this.mapper = mapper;
            this.newsDal = newsDal;
            this.foodMenuDal = foodMenuDal;

        }

        [ValidationAspect(typeof(ChangePasswordValidator), Priority = 2)]
         [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task ChangePassword(UserForChangePasswordDto userForChangePasswordDto, int userId)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            var user = await userDal.GetAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Blaaaa = Messages.UserNotFound });
            }

            if (!HashingHelper.VerifyPasswordHash(userForChangePasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new RestException(HttpStatusCode.BadRequest, new { CantAccess = "Eski şifreniz hatalı..." });
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePaswordHash(userForChangePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await userDal.Update(user);

        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<UserPhotoForReturnDto> UploadProfilePhoto(FileUploadDto uploadDto)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != uploadDto.AnnounceId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            var checkAnnounceById = await userDal.GetAsync(x => x.Id == uploadDto.AnnounceId);

            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.UserNotFound });
            }

            var uploadFile = await upload.Upload(uploadDto.File, "userprofile");

            var mapForCreate = new UserPhotoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.UserId = uploadDto.AnnounceId;
            mapForCreate.IsConfirm = false;
            mapForCreate.IsMain = false;
            var mapForDb = mapper.Map<UserPhoto>(mapForCreate);
            var createPhoto = await userPhotoDal.Add(mapForDb);
            return mapper.Map<UserPhoto, UserPhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<PublicForReturnDto> GetAllAnnounceForPublicAsync()
        {

            var announceFromRepo = await announceDal.GetAnnounceForPublicAsync();
            var homeAnnounceFromRepo = await homeAnnounceDal.GetHomeAnnouncesForPublicAsync();
            var vehicleAnnounceFromRepo = await vehicleAnnounceDal.GetVehicleAnnouncesForPublicAsync();
            var newsFromRepo = await newsDal.GetNewsForPublicDto();
            var foodsMenuFromRepo = await foodMenuDal.GetFoodMenusForPublicAsync();


            return new PublicForReturnDto()
            {


                Announces = mapper.Map<List<Announce>, List<AnnounceForPublicDto>>(announceFromRepo),
                HomeAnnounces = mapper.Map<List<HomeAnnounce>, List<HomeAnnounceForPublicDto>>(homeAnnounceFromRepo),
                VehicleAnnounces = mapper.Map<List<VehicleAnnounce>, List<VehicleAnnounceForPublicDto>>(vehicleAnnounceFromRepo),
                News = mapper.Map<List<News>, List<NewsForPublicDto>>(newsFromRepo),
                FoodsMenu = mapper.Map<List<FoodMenu>, List<FoodMenuForPublicDto>>(foodsMenuFromRepo),


            };
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<UserForListDto> GetUSerById(int userId)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            var spec = new UserWithPhotoByIdSpecification(userId);
            var user = await userDal.GetEntityWithSpecAsync(spec);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.UserNotFound });
            }

            var mapForReturn = mapper.Map<User, UserForListDto>(user);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(UserValidator), Priority = 2)]
        public async Task<UserForListDto> UpdateUserProfileAsync(UserForRegisterDto userForRegisterDto, int userId)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            var spec = new UserWithPhotoByIdSpecification(userId);
            var user = await userDal.GetEntityWithSpecAsync(spec);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = Messages.UserNotFound });
            }

            var userForUpdate = mapper.Map(userForRegisterDto, user);

            await userDal.Update(userForUpdate);
            var specAfterUpdate = new UserWithPhotoByIdSpecification(userId);
            var userAfterUpdate = await userDal.GetEntityWithSpecAsync(specAfterUpdate);
            var mapForReturn = mapper.Map<User, UserForListDto>(userAfterUpdate);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<UserCamPusAndDepartmentAndDegree> UserCamPusAndDepartmentAndDegreeAsync()
        {
            var campuses = await campusDal.GetListAsync();
            var departments = await departmentDal.GetListAsync();
            var degrees = await degreeDal.GetListAsync();

            var mapForReturn = new UserCamPusAndDepartmentAndDegree()
            {
                Campuses = mapper.Map<List<Campus>, List<CampusForReturnDto>>(campuses),
                Departments = mapper.Map<List<Department>, List<DepartmentForReturnDto>>(departments),
                Degrees = mapper.Map<List<Degree>, List<DegreeForReturnDto>>(degrees)
            };

            if (mapForReturn == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.NotFound });
            }

            return mapForReturn;
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(UserPhotoValidator), Priority = 2)]
        public async Task<UserPhotoForReturnDto> MakeMainPhotoAsync(UserPhotoForCreationDto creationDto, int userId)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }
            
            var checkByIdFromRepo = await userPhotoDal.GetAsync(x => x.Id == creationDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mainPhotoFromRepo=await userPhotoDal.GetAsync(x=>x.IsMain==true && x.UserId==creationDto.UserId);
            if(mainPhotoFromRepo!=null)
            {
                mainPhotoFromRepo.IsMain=false;
                await userPhotoDal.Update(mainPhotoFromRepo);
            }

            var mapForUpdate = mapper.Map(creationDto, checkByIdFromRepo);
            var updatePhoto = await userPhotoDal.Update(mapForUpdate);
            return mapper.Map<UserPhoto, UserPhotoForReturnDto>(updatePhoto);
        }
    }
}