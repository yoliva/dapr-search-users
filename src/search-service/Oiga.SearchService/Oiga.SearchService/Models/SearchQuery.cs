namespace Oiga.SearchService.Models
{
    public class SearchQuery
    {
        public string ContinuationToken { get; set; }
        public string Query { get; set; }
        public int Limit { get; set; }
    }
}
