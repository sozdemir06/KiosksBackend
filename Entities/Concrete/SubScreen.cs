using Core.Entities;

namespace Entities.Concrete
{
    public class SubScreen : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public bool Status { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int ScreenId { get; set; }
        public Screen Screen { get; set; }

    }
}