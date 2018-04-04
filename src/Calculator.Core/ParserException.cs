using System;

namespace Calculator.Core
{
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message)
        {
        }
    }
}