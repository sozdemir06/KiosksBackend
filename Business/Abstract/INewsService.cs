using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface INewsService
    {
        Task<Pagination<NewsForReturnDto>> GetListAsync(NewsParams queryParams);
        Task<NewsForDetailDto> GetDetailAsync(int announceId);
        Task<NewsForReturnDto> Create(NewsForCreationDto creationDto);
        Task<NewsForReturnDto> Update(NewsForCreationDto updateDto);
        Task<NewsForReturnDto> Publish(NewsForCreationDto updateDto);
        Task<NewsForReturnDto> Delete(int Id);
    }
}