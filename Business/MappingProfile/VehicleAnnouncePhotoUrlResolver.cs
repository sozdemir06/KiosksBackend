using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.MappingProfile
{
    public class VehicleAnnouncePhotoUrlResolver : IValueResolver<VehicleAnnouncePhoto, VehicleAnnouncePhotoForReturnDto, string>
    {
        private readonly IConfiguration config;
        public VehicleAnnouncePhotoUrlResolver(IConfiguration config)
        {
            this.config = config;

        }
        public string Resolve(VehicleAnnouncePhoto source, VehicleAnnouncePhotoForReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Name))
            {
                return config["ApiUrl"] + source.Name;
            }
            return null;
        }
    }
}