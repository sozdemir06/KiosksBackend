using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICurrencyService
    {
        Task<List<CurrencyForReturnDto>> GetListAsync();
        Task<CurrencyForReturnDto> Create(CurrencyForCreationDto createDto);
        Task<CurrencyForReturnDto> Update(CurrencyForCreationDto updateDto);
        Task<CurrencyForReturnDto> Delete(int Id);
    }
}