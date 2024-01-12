using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SearchService.RequestHelper
{
    public class SearchParam
    {
        public string searchTerm { get; set; }
        public int pageSize { get; set; }=4;
        public int pageNumber { get; set; }=1;
        public string Seller { get; set; }  
        public string Winner { get; set; }
        public string OrderBy { get; set; }
        public string FilterBy { get; set; }
    }
}