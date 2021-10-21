using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Oiga.SearchService.Services
{
    public class InputTokenizerService : IInputTokenizerService
    {
        public IEnumerable<string> Tokenize(string input)
        {
            var result = Regex.Replace(input, @"[.,;:?¡¿!-]", string.Empty);
            result = Regex.Replace(result, @"\w+", " ");
            return result.Split(" ");
        }
    }
}
