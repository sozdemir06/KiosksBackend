using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.MappingProfile
{
    public class FoodMenuPhotoUrlResolver : IValueResolver<FoodMenuPhoto, FoodMenuPhotoForReturnDto, string>
    {
        private readonly IConfiguration config;
        public FoodMenuPhotoUrlResolver(IConfiguration config)
        {
            this.config = config;

        }
        public string Resolve(FoodMenuPhoto source, FoodMenuPhotoForReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Name))
            {
                return config["ApiUrl"] + source.Name;
            }
            return null;
        }
    }
}