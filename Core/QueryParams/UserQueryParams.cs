namespace Core.QueryParams
{
    public class UserQueryParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;

        }
        public bool? Status { get; set; }
        public string StatusActive { get; set; }
        public string StatusPassive { get; set; }
        public string Sort { get; set; }

        private string _search;
        public string Search { 
            get=>_search; 
            set=>_search=value.ToLower();
            
        }

    }
}