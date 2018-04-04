namespace Calculator.Core
{
    public class IntegerSymbol : Symbol
    {
        public int Value { get; }

        public IntegerSymbol(int value)
        {
            Value = value;
        }
    }
}