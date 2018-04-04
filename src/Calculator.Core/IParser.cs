namespace Calculator.Core
{
    public interface IParser
    {
        IFormula Parse(string formula);
    }
}