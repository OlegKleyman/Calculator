using System.Collections.Generic;

namespace Calculator.Core
{
    public interface IParser
    {
        IEnumerable<Operation> Parse(ISymbolStream stream);
    }
}