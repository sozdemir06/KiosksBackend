using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.MappingProfile
{
    public class HomeAnnouncePhotoUrlResolver : IValueResolver<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto, string>
    {
        private readonly IConfiguration config;
        public HomeAnnouncePhotoUrlResolver(IConfiguration config)
        {
            this.config = config;

        }

        public string Resolve(HomeAnnouncePhoto source, HomeAnnouncePhotoForReturnDto destination, string destMember, ResolutionContext context)
        {
             if(!string.IsNullOrEmpty(source.Name))
            {
                return config["ApiUrl"]+source.Name;
            }
            return null;
        }
    }
}