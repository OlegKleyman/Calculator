using System;
using System.Collections;
using System.Collections.Generic;

namespace Calculator.Core
{
    public class Formula : IFormula
    {
        private readonly IEnumerable<Symbol> _symbols;

        public Formula(IEnumerable<Symbol> symbols)
        {
            _symbols = symbols ?? throw new ArgumentNullException(nameof(symbols));
        }

        public IEnumerable<Operation> Operations
        {
            get
            {
                var nextOperation = Operator.None;

                foreach (var symbol in _symbols)
                {
                    if (symbol is IntegerSymbol integer)
                    {
                        yield return nextOperation == Operator.Add || nextOperation == Operator.None
                            ? (Operation) new AddOperation(integer.Value)
                            : new SubtractOperation(integer.Value);
                    }
                    else if (symbol is OperatorSymbol op)
                    {
                        nextOperation = op.Operator;
                    }
                    else
                    {
                        throw new InvalidOperationException($"{symbol.GetType().Name} is not supported");
                    }
                }
            }
        }
    }
}