using Core.Entities;

namespace Entities.Dtos
{
    public class FoodMenuBgPhotoForReturnDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsSetBackground { get; set; }
    }
}