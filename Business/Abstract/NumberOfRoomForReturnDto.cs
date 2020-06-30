using Core.Entities;

namespace Business.Abstract
{
    public class NumberOfRoomForReturnDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}