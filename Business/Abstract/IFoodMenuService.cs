using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IFoodMenuService
    {
        Task<Pagination<FoodMenuForReturnDto>> GetListAsync(FoodMenuParams queryParams);
        Task<FoodMenuForDetailDto> GetDetailAsync(int announceId);
        Task<FoodMenuForReturnDto> Create(FoodMenuForCreationDto creationDto);
        Task<FoodMenuForReturnDto> Update(FoodMenuForCreationDto updateDto);
        Task<FoodMenuForReturnDto> Publish(FoodMenuForCreationDto updateDto);
        Task<FoodMenuForReturnDto> Delete(int Id);
    }
}