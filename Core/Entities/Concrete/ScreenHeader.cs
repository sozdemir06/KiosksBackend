using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    public class ScreenHeader:IEntity
    {
        public int Id { get; set; }
        public string HeaderText { get; set; }
        public Screen Screen { get; set; }
        public int ScreenId { get; set; }
        
    }
}