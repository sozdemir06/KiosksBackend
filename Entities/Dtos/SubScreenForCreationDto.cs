using Core.Entities;

namespace Entities.Dtos
{
    public class SubScreenForCreationDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public bool Status { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ScreenId { get; set; }


        public SubScreenForCreationDto()
        {
            if(Position=="Top")
            {
                Status=true;
            }
        }
        
    }
}