using System.Collections;
using System.Collections.Generic;

namespace Calculator.Core
{
    public interface IFormula
    {
        IEnumerable<Operation> Operations { get; }
    }
}