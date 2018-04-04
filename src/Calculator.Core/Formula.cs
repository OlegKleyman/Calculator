using System.Collections;
using System.Collections.Generic;

namespace Calculator.Core
{
    public class Formula : IFormula
    {
        public IEnumerable<Operation> Operations { get; }
        public int? CurrentValue { get; }
    }
}