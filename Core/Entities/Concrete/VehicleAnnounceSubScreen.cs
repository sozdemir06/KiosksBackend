namespace Core.Entities.Concrete
{
    public class VehicleAnnounceSubScreen : IEntity
    {
        public int Id { get; set; }
        public SubScreen SubScreen { get; set; }
        public int SubScreenId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
        public VehicleAnnounce VehicleAnnounce { get; set; }
        public int VehicleAnnounceId { get; set; }
        public Screen Screen { get; set; }
        public int ScreenId { get; set; }
    }
}