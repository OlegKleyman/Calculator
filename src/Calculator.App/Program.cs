using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Core;

namespace Calculator.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new DefaultParser();
            var calculator = new Core.Calculator();

            var factory = new SymbolFactory();
            
            var rawFormula = args.Length == 0 ? GetFormula() : args[0];

            do
            {
                var symbolStream = new SymbolStream(rawFormula, factory);

                try
                {
                    var formula = parser.Parse(symbolStream);
                    Console.WriteLine($"result: {calculator.Calculate(formula)}");
                }
                catch (SymbolException ex)
                {
                    Console.WriteLine($"{ex.Message} '{ex.Symbol}'");
                }
                catch (ParserException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                rawFormula = GetFormula();
            } while (!string.IsNullOrWhiteSpace(rawFormula));
        }

        private static string GetFormula()
        {
            Console.WriteLine("Enter formula");
            return Console.ReadLine();
        }
    }
}
