using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.MappingProfile
{
    public class UserPhotoUrlResolver : IValueResolver<UserPhoto, UserPhotoForReturnDto, string>
    {
        private readonly IConfiguration config;
        public UserPhotoUrlResolver(IConfiguration config)
        {
            this.config = config;

        }
        public string Resolve(UserPhoto source, UserPhotoForReturnDto destination, string destMember, ResolutionContext context)
        {
             if(!string.IsNullOrEmpty(source.Name))
            {
                return config["ApiUrl"]+source.Name;
            }
            return null;
        }
    }
}