using Core.Entities;

namespace Entities.Dtos
{
    public class Wind : IDto
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }
}