using System.Collections.Generic;

namespace Entities.Dtos
{
    public class WheatherForeCastForReturnDto
    {
         public string Icon { get; set; }
         public double Temp { get; set; }
         public int Pressure { get; set; }
         public int Humidity { get; set; }
         public double TempMin { get; set; }
         public double TempMax { get; set; }
         public string CityName { get; set; }
         public string WheatherImage { get; set; }
    }
}