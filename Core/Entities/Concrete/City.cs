namespace Core.Entities.Concrete
{
    public class City:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public int CityId { get; set; }
        
    }
}