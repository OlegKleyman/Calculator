namespace Calculator.Core
{
    public class SubtractOperation : Operation
    {
        public SubtractOperation(int value) : base(value)
        {
        }

        public override int Execute(int value)
        {
            return value - Value;
        }
    }
}