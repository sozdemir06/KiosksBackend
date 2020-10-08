namespace Core.Entities.Concrete
{
    public class FoodMenuPhoto:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsConfirm { get; set; }
        public bool UnConfirm { get; set; }
        public FoodMenu FoodMenu { get; set; }
        public int FoodMenuId { get; set; }
    }
}