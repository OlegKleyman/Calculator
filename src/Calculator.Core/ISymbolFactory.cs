namespace Calculator.Core
{
    public interface ISymbolFactory
    {
        Symbol GetNext(string formula);
    }
}