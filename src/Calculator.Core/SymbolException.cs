using System;

namespace Calculator.Core
{
    public class SymbolException : Exception
    {
        public string Symbol { get; }

        public SymbolException(string message, string symbol) : base(message)
        {
            Symbol = symbol;
        }
    }
}