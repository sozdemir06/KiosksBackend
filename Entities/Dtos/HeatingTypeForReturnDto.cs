using Core.Entities;

namespace Entities.Dtos
{
    public class HeatingTypeForReturnDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}