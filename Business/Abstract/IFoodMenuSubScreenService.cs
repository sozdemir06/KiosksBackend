using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IFoodMenuSubScreenService
    {
        Task<List<FoodMenuSubScreenForReturnDto>> GetListAsync();
        Task<List<FoodMenuSubScreenForReturnDto>> GetByAnnounceId(int announceId);
        Task<FoodMenuSubScreenForReturnDto> Create(FoodMenuSubScreenForCreationDto creationDto);
        Task<FoodMenuSubScreenForReturnDto> Update(FoodMenuSubScreenForCreationDto updateDto);
        Task<FoodMenuSubScreenForReturnDto> Delete(int Id);
    }
}