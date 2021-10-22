using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Oiga.SearchService.Services
{
    public class InputTokenizerService : IInputTokenizerService
    {
        public IEnumerable<string> Tokenize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return Enumerable.Empty<string>();

            var result = Regex.Replace(input, @"[.,;:?¡¿!-]", string.Empty);
            result = Regex.Replace(result, @"\s+", " ");
            return result.Split(" ");
        }
    }
}
