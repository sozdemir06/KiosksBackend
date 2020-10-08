namespace Core.Entities.Concrete
{
    public class VehicleAnnouncePhoto:IEntity
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsConfirm { get; set; }
        public bool UnConfirm { get; set; }
        public VehicleAnnounce VehicleAnnounce { get; set; }
        public int VehicleAnnounceId { get; set; }
    }
}