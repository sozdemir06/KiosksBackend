using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IFoodMenuPhotoService
    {
        Task<List<FoodMenuPhotoForReturnDto>> GetListAsync(int announceId);
        Task<FoodMenuPhotoForReturnDto> Create(FileUploadDto uploadDto);
        Task<FoodMenuPhotoForReturnDto> Update(FoodMenuPhotoForCreationDto updateDto);
        Task<FoodMenuPhotoForReturnDto> SetAsBackground(FoodMenuPhotoForCreationDto updateDto);
        Task<FoodMenuPhotoForReturnDto> Delete(int Id);
    }
}