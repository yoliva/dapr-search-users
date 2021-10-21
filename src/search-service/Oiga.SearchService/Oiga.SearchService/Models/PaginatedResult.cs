using System.Collections.Generic;

namespace Oiga.SearchService.Models
{
    public class PaginatedResult<T>
    {
        public string ContinuationToken { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
