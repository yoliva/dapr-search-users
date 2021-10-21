using System.Collections.Generic;

namespace Oiga.SearchService.Services
{
    public interface IInputTokenizerService
    {
        IEnumerable<string> Tokenize(string input);
    }
}
