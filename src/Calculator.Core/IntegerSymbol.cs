using System.Globalization;

namespace Calculator.Core
{
    public class IntegerSymbol : Symbol
    {
        public IntegerSymbol(int value) : base(value.ToString(CultureInfo.InvariantCulture))
        {
            Value = value;
        }

        public int Value { get; }
    }
}