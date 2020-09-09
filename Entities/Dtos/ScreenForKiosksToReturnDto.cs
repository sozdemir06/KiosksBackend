using System.Collections.Generic;
using Core.Entities;

namespace Entities.Dtos
{
    public class ScreenForKiosksToReturnDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public bool IsFull { get; set; }
        public List<SubScreenForReturnDto> SubScreens { get; set; }
        public ScreenHeaderForReturnDto ScreenHeaders { get; set; }
        public ScreenFooterForReturnDto ScreenFooters { get; set; }
        public List<ScreenHeaderPhotoForReturnDto> ScreenHeaderPhotos { get; set; }
    }
}