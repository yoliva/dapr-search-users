namespace Oiga.Common.Pagination
{
    public class ContinuationToken
    {
        public int SkipCount { get; internal set; }
        public int Limit { get; internal set; }
    }
}
