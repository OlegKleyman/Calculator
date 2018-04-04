namespace Calculator.Core
{
    public interface ISymbolStream
    {
        bool End { get; }
        Symbol Next();
        Symbol Peek();
    }
}