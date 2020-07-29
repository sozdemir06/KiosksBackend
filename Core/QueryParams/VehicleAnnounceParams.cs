namespace Core.QueryParams
{
    public class VehicleAnnounceParams
    {
         private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;

        }
        public int? ScreenId { get; set; }
        public int? SubScreenId { get; set; }
        public bool? Reject { get; set; }
        public bool? IsPublish { get; set; }
        public bool? IsNew { get; set; }
        public string Sort { get; set; }

        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();

        }
    }
}