namespace Core.Entities.Concrete
{
    public class FoodMenuBgPhoto : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsSetBackground { get; set; }

    }
}