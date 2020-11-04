using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.MappingProfile
{
    public class LogoUrlResolver : IValueResolver<PublicLogo, PublicLogoForReturnDto, string>
    {
        private readonly IConfiguration config;
        public LogoUrlResolver(IConfiguration config)
        {
            this.config = config;

        }
        public string Resolve(PublicLogo source, PublicLogoForReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Name))
            {
                return config["ApiUrl"] + source.Name;
            }
            return null;
        }
    }
}