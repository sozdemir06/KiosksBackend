using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.MappingProfile
{
    public class NewsPhotoUrlResolver : IValueResolver<NewsPhoto, NewsPhotoForReturnDto, string>
    {
        private readonly IConfiguration config;
        public NewsPhotoUrlResolver(IConfiguration config)
        {
            this.config = config;

        }
        public string Resolve(NewsPhoto source, NewsPhotoForReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Name))
            {
                return config["ApiUrl"] + source.Name;
            }
            return null;
        }
    }
}