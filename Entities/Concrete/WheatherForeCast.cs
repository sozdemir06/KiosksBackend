using System.Collections.Generic;
using Entities.Dtos;

namespace Entities.Concrete
{
    public class WheatherForeCast
    {
        public int cnt { get; set; }
        public List<WheatherForeCastHttpResponseDto> list { get; set; }
    }
}