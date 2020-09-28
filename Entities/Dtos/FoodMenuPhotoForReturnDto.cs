using Core.Entities;

namespace Entities.Dtos
{
    public class FoodMenuPhotoForReturnDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsConfirm { get; set; }
        public int FoodMenuId { get; set; }
    }
}