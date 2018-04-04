using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class SymbolFactoryTests
    {
        [Theory]
        [MemberData(nameof(SymbolFactoryTestsTheories.GetShouldReturnIntegerSymbolTheories), MemberType =
            typeof(SymbolFactoryTestsTheories))]
        public void GetNextShouldReturnIntegerSymbol(string formula, Symbol symbol)
        {
            var factory = GetSymbolFactory();

            var result = factory.GetNext(formula);

            result.Should().BeOfType(symbol.GetType());
            result.RawValue.Should().BeEquivalentTo(symbol.RawValue);
        }

        private SymbolFactory GetSymbolFactory()
        {
            return new SymbolFactory();
        }

        public static class SymbolFactoryTestsTheories
        {
            public static IEnumerable<object[]> GetShouldReturnIntegerSymbolTheories()
            {
                var cases = new[]
                {
                    new object[]
                    {
                        "1",
                        new IntegerSymbol(1)
                    },
                    new object[]
                    {
                        "+",
                        new OperatorSymbol(Operator.Add)
                    },
                    new object[]
                    {
                        "-",
                        new OperatorSymbol(Operator.Subtract)
                    },
                    new object[]
                    {
                        "11",
                        new IntegerSymbol(11)
                    },
                    new object[]
                    {
                        "11+",
                        new IntegerSymbol(11)
                    },
                    new object[]
                    {
                        "11  ",
                        new IntegerSymbol(11)
                    },
                    new object[]
                    {
                        "11 -",
                        new IntegerSymbol(11)
                    }
                };

                return cases;
            }
        }

        [Fact]
        public void GetShouldThrowArgumentExceptionWhenFormulaIsEmpty()
        {
            var factory = GetSymbolFactory();
            Action getNext = () => factory.GetNext(string.Empty);

            getNext
                .Should().Throw<ArgumentException>()
                .WithMessage("Argument empty\r\nParameter name: formula")
                .And.ParamName.Should().BeEquivalentTo("formula");
        }

        [Fact]
        public void GetShouldThrowArgumentExceptionWhenFormulaStartsWithSpace()
        {
            var factory = GetSymbolFactory();
            Action getNext = () => factory.GetNext(" ");

            getNext
                .Should().Throw<ArgumentException>()
                .WithMessage("Argument starts with space\r\nParameter name: formula")
                .And.ParamName.Should().BeEquivalentTo("formula");
        }

        [Fact]
        public void GetShouldThrowArgumentNullExceptionWhenFormulaArgumentIsNull()
        {
            var factory = GetSymbolFactory();
            Action getNext = () => factory.GetNext(null);

            getNext.Should().Throw<ArgumentNullException>().Which.ParamName.Should().BeEquivalentTo("formula");
        }

        [Fact]
        public void GetShouldThrowSymbolExceptionWhenLiteralIsNotNumber()
        {
            var factory = GetSymbolFactory();
            Action getNext = () => factory.GetNext("a");

            getNext
                .Should().Throw<SymbolException>()
                .WithMessage("Invalid number")
                .And.Symbol.Should().BeEquivalentTo("a");
        }
    }
}