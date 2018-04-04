using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class FormulaTests
    {
        [Fact]
        public void OperationsShouldReturnStartingWithAddOperationIfSymbolsDoNotContainStartingOperation()
        {
            var symbols = new Symbol[]
            {
                new IntegerSymbol(default(int))
            };

            var formula = GetFormula(symbols);

            formula.Operations.Select(operation => operation.GetType())
                .Should().BeEquivalentTo(typeof(AddOperation));
        }

        [Theory]
        [InlineData(Operator.Add, typeof(AddOperation))]
        [InlineData(Operator.Subtract, typeof(SubtractOperation))]
        public void OperationsShouldReturnStartingWithOperationFromSymbolsStartIfSymbolsStartsWithOperation(Operator op, Type type)
        {
            var symbols = new Symbol[]
            {
                new OperatorSymbol(op),
                new IntegerSymbol(default(int))
            };

            var formula = GetFormula(symbols);

            formula.Operations.Select(operation => operation.GetType())
                .Should().BeEquivalentTo(type);
        }

        [Fact]
        public void OperationsShouldReturnOperationsFromSymbolStream()
        {
            var symbols = new Symbol[]
            {
                new OperatorSymbol(Operator.Add),
                new IntegerSymbol(default(int)),
                new OperatorSymbol(Operator.Add),
                new IntegerSymbol(default(int)),
                new OperatorSymbol(Operator.Subtract),
                new IntegerSymbol(default(int))
            };

            var formula = GetFormula(symbols);

            formula.Operations.Select(operation => operation.GetType())
                .Should().BeEquivalentTo(typeof(AddOperation), typeof(AddOperation), typeof(SubtractOperation));
        }

        [Fact]
        public void ConstructorShouldThrowArgumentNullExceptionWhenSymbolsAreNull()
        {
            Action constructor = () => new Formula(null);

            constructor
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().BeEquivalentTo("symbols");
        }

        [Fact]
        public void OperationsShouldThrowInvalidOperationExceptionWhenSymbolsContainsNonSupportedSymbol()
        {
            var symbols = new[]
            {
                Substitute.For<Symbol>()
            };

            var formula = GetFormula(symbols);

            Action operations = () => formula.Operations.ToList();

            operations.Should().Throw<InvalidOperationException>().WithMessage("SymbolProxy is not supported");
        }

        private Formula GetFormula(IEnumerable<Symbol> symbols)
        {
            return new Formula(symbols);
        }
    }
}
