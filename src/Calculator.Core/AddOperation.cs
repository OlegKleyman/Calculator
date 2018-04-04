namespace Calculator.Core
{
    public class AddOperation : Operation
    {
        public AddOperation(int value) : base(value)
        {
        }

        public override int Execute(int value)
        {
            return Value + value;
        }
    }
}