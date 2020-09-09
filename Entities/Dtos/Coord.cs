using Core.Entities;

namespace Entities.Dtos
{
    public class Coord:IDto
    {
        public double lon { get; set; }   
        public double lat { get; set; }   
    }
}