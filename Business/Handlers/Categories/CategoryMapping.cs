using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Handlers.Categories
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category,CategoryForListDto>();
        }
    }
}