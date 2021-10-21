using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Oiga.SearchService.Services
{
    public class InputTokenizerService : IInputTokenizerService
    {
        public IEnumerable<string> Tokenize(string input)
        {
            return Regex.Replace(input, @"", string.Empty).Split(" ");
        }
    }
}
