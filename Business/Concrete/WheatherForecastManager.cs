using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using BusinessAspects.AutoFac;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Business.Concrete
{
    public class WheatherForecastManager : IWheatherForeCastService
    {
        private readonly ICityDal cityDal;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        public WheatherForecastManager(ICityDal cityDal, IConfiguration config, IMapper mapper)
        {
            this.mapper = mapper;
            this.config = config;
            this.cityDal = cityDal;

        }

        //[SecuredOperation("Sudo,WheatherForeCast.List", Priority = 1)]
        public async Task<List<WheatherForeCastForReturnDto>> WheatherForeCastsAsync()
        {
            var apiKey = config.GetValue<string>("OpenAPiKey");
            var selectedCities = await cityDal.GetListAsync(x => x.Selected == true);

            var citiesId = selectedCities.Select(x => x.CityId).ToArray();
            var cityId = string.Join(",", citiesId);

            using (HttpClient client = new HttpClient())
            {

                var httResponse = await client.GetAsync($"https://api.openweathermap.org/data/2.5/group?id={cityId}&units=metric&appid={apiKey}");
                if (!httResponse.IsSuccessStatusCode)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = "Hava durumu bilgileri alınamadı.." });
                }

                var content = await httResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<WheatherForeCast>(content);
                var mapForReturn=mapper.Map<List<WheatherForeCastHttpResponseDto>,List<WheatherForeCastForReturnDto>>(result.list);
                return mapForReturn;


             }



        }
    }
}