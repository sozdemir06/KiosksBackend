using System.Collections.Generic;
using Core.Entities;

namespace Entities.Dtos
{
    public class ScreenHeaderForReturnDto:IDto
    {
        public int Id { get; set; }
        public string HeaderText { get; set; }
        public int ScreenId { get; set; }
        
    }
}