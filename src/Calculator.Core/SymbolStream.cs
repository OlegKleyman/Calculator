using System;

namespace Calculator.Core
{
    public class SymbolStream : ISymbolStream
    {
        private readonly ISymbolFactory _factory;
        private string _formula;

        public SymbolStream(string formula, ISymbolFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _formula = formula?.Trim();
            End = string.IsNullOrEmpty(_formula);
        }

        public Symbol Next()
        {
            if(End) throw new InvalidOperationException("End of stream has been reached");

            _formula = _formula.Trim();
            var result= _factory.GetNext(_formula);

            if (result == null || _formula.Length <= result.RawValue.Length)
            {
                End = true;
            }
            else
            {
                _formula = _formula.Substring(result.RawValue.Length);
            }

            return result;
        }

        public bool End { get; private set; }

        public Symbol Peek()
        {
            return !End ?_factory.GetNext(_formula.Trim()) : null;
        }
    }
}