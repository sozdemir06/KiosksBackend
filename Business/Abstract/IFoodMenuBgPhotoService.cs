using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IFoodMenuBgPhotoService
    {
        Task<List<FoodMenuBgPhotoForReturnDto>> GetListAsync();
        Task<FoodMenuBgPhotoForReturnDto> Create(FileUploadDto uploadDto);
        Task<FoodMenuBgPhotoForReturnDto> Update(FoodMenuBgPhotoForCreationDto updateDto);
        Task<FoodMenuBgPhotoForReturnDto> SetBackgroundPhoto(FoodMenuBgPhotoForCreationDto updateDto);
        Task<FoodMenuBgPhotoForReturnDto> Delete(int Id);
    }
}