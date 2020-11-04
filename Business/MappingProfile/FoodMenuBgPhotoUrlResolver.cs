using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.MappingProfile
{
    public class FoodMenuBgPhotoUrlResolver : IValueResolver<FoodMenuBgPhoto, FoodMenuBgPhotoForReturnDto, string>
    {
        private readonly IConfiguration config;
        public FoodMenuBgPhotoUrlResolver(IConfiguration config)
        {
            this.config = config;

        }
        public string Resolve(FoodMenuBgPhoto source, FoodMenuBgPhotoForReturnDto destination, string destMember, ResolutionContext context)
        {
             if(!string.IsNullOrEmpty(source.Name))
            {
                return config["ApiUrl"]+source.Name;
            }
            return null;
        }
    }
}