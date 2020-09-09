using System.Linq;
using AutoMapper;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.MappingProfile
{
    public class WheatherImageUrlResolver : IValueResolver<WheatherForeCastHttpResponseDto, WheatherForeCastForReturnDto, string>
    {
        private readonly IConfiguration config;
        public WheatherImageUrlResolver(IConfiguration config)
        {
            this.config = config;

        }
        public string Resolve(WheatherForeCastHttpResponseDto source, WheatherForeCastForReturnDto destination, string destMember, ResolutionContext context)
        {
            var sourceIcon=source.weather.Select(x=>x.icon).FirstOrDefault();

            if(!string.IsNullOrEmpty(sourceIcon))
            {
                return config.GetValue<string>("WheatherImageUrl")+sourceIcon+"@2x.png";
            }
            return null;
        }
    }
}