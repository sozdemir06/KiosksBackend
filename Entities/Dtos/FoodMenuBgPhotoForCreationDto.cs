using Core.Entities;

namespace Entities.Dtos
{
    public class FoodMenuBgPhotoForCreationDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsSetBackground { get; set; }
    }
}