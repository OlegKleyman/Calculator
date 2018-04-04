using System;
using System.Linq;

namespace Calculator.Core
{
    public class Calculator
    {
        public int Calculate(IFormula formula)
        {
            if (formula == null) throw new ArgumentNullException(nameof(formula));
            if(formula.Operations == null) throw new ArgumentException("Operations is null", nameof(formula));

            return formula.Operations
                .Aggregate(0, (i, operation) => 
                    operation.Execute(i, formula.CurrentValue ?? throw new InvalidOperationException()));
        }
    }
}