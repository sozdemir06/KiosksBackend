using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.MappingProfile
{
    public class ScreenHeaderPhotoUrlResolver : IValueResolver<ScreenHeaderPhoto, ScreenHeaderPhotoForReturnDto, string>
    {
        private readonly IConfiguration config;
        public ScreenHeaderPhotoUrlResolver(IConfiguration config)
        {
            this.config = config;

        }
        public string Resolve(ScreenHeaderPhoto source, ScreenHeaderPhotoForReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.Name))
            {
                return config["ApiUrl"]+source.Name;
            }
            return null;
        }
    }
}