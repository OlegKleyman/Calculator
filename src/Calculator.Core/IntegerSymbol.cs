using System.Globalization;

namespace Calculator.Core
{
    public class IntegerSymbol : Symbol
    {
        public int Value { get; }

        public IntegerSymbol(int value) : base(value.ToString(CultureInfo.InvariantCulture))
        {
            Value = value;
        }
    }
}