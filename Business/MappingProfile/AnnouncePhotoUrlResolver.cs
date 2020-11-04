using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.MappingProfile
{
    public class AnnouncePhotoUrlResolver : IValueResolver<AnnouncePhoto, AnnouncePhotoForReturnDto, string>
    {
        private readonly IConfiguration config;
        public AnnouncePhotoUrlResolver(IConfiguration config)
        {
            this.config = config;

        }
        public string Resolve(AnnouncePhoto source, AnnouncePhotoForReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.Name))
            {
                return config["ApiUrl"]+source.Name;
            }
            return null;
        }
    }
}