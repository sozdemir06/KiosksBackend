using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface INumberOfRoomService
    {
         Task<List<NumberOfRoomForReturnDto>> GetListAsync();
         Task<NumberOfRoomForReturnDto> Create(NumberOfRoomForCreateOrUpdateDto createDto);
         Task<NumberOfRoomForReturnDto> Update(NumberOfRoomForCreateOrUpdateDto updateDto);
         Task<NumberOfRoomForReturnDto> Delete(int Id);
    }
}