using System.Collections.Generic;

namespace RIceSDK.Utils
{
    public class FilterParameters
    {
        public IEnumerable<string> Clauses { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string OrderBy { get; set; }
        public string Orientation { get; set; }
    }
}
