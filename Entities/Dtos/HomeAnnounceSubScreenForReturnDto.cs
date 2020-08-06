using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Dtos
{
    public class HomeAnnounceSubScreenForReturnDto:IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int HomeAnnounceId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
       
    }
}