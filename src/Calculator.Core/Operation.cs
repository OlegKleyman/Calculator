namespace Calculator.Core
{
    public abstract class Operation
    {
        protected Operation(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public abstract int Execute(int value);
    }
}