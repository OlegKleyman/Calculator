namespace Calculator.Core
{
    public abstract class Symbol
    {
        protected Symbol(string rawValue)
        {
            RawValue = rawValue;
        }

        public string RawValue { get; protected set; }
    }
}