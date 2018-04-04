using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Core
{
    public class Calculator
    {
        public int Calculate(IEnumerable<Operation> operations)
        {
            if (operations == null) throw new ArgumentNullException(nameof(operations));

            return operations.Aggregate(0, (i, operation) => operation.Execute(i));
        }
    }
}