using System;

namespace Calculator.Core
{
    public abstract class Operation
    {
        public abstract int Execute(int sum, int currentValue);

        public abstract int Execute(int value);
    }
}