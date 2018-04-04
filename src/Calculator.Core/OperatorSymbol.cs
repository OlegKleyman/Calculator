namespace Calculator.Core
{
    public class OperatorSymbol : Symbol
    {
        public OperatorSymbol(Operator op) : base(op == Operator.Add ? "+" : "-")
        {
            Operator = op;
        }

        public Operator Operator { get; }
    }
}