using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IPublicService
    {
         Task<PublicForReturnDto> GetAllAnnounceForPublicAsync();
         Task<UserForListDto> GetUSerById(int userId);
         Task<UserCamPusAndDepartmentAndDegree> UserCamPusAndDepartmentAndDegreeAsync();
         Task<UserPhotoForReturnDto> UploadProfilePhoto(FileUploadDto uploadDto);
         Task<UserPhotoForReturnDto> MakeMainPhotoAsync(UserPhotoForCreationDto creationDto,int userId);
         Task<UserForListDto> UpdateUserProfileAsync(UserForRegisterDto userForRegisterDto,int userId);
         Task ChangePassword(UserForChangePasswordDto userForChangePasswordDto,int userId);
    }
}