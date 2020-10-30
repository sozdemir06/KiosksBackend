using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IPublicFooterTextService
    {
        Task<PublicFooterTextForReturnDto> GetFooterTextAsync();
        Task<PublicFooterTextForReturnDto> Create(PublicFooterTextForCreationDto creationDto);
       
    }
}