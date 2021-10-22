using Newtonsoft.Json;
using System;
using System.Text;

namespace Oiga.Common.Pagination
{
    public static class Helpers
    {
        public static ContinuationToken Decode(string continuationToken)
        {
            return JsonConvert.DeserializeObject<ContinuationToken>(Encoding.UTF8.GetString(Convert.FromBase64String(continuationToken)));
        }

        public static string Encode(int limit, int skipCount)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new ContinuationToken { Limit = limit, SkipCount = skipCount })));
        }
    }
}
