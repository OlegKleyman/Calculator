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

            var formula = parser.Parse(args[0]);

            Console.WriteLine(calculator.Calculate(formula));
        }
    }
}
