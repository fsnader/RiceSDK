using System;
using System.Collections.Generic;
using System.Text;

namespace Rice.SDK.Utils
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
