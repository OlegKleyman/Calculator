using System;

namespace Calculator.Core
{
    public class SymbolFactory : ISymbolFactory
    {
        public Symbol GetNext(string formula)
        {
            if (formula == null) throw new ArgumentNullException(nameof(formula));
            if(formula.Length == 0) throw new ArgumentException("Argument empty", nameof(formula));

            Symbol symbol;

            if (formula[0] == '+')
            {
                symbol = new OperatorSymbol(Operator.Add);
            }
            else if (formula[0] == '-')
            {
                symbol = new OperatorSymbol(Operator.Subtract);
            }
            else if (formula[0] != ' ')
            {
                symbol = GetNumber(formula);
            }
            else
            {
                throw new ArgumentException("Argument starts with space", nameof(formula));
            }

            return symbol;
        }

        private Symbol GetNumber(string formula)
        {
            var endIndex = formula.IndexOfAny(new[] { ' ', '-', '+' });
            var literal = endIndex == -1 ? formula.Substring(0) : formula.Substring(0, endIndex);

            if (!int.TryParse(literal, out int number))
            {
                throw new SymbolException("Invalid number", literal);
            }

            return new IntegerSymbol(number);
        }
    }
}