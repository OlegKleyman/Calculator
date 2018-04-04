using System;

namespace Calculator.Core
{
    public class SymbolException : Exception
    {
        public SymbolException(string message, string symbol) : base(message)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}