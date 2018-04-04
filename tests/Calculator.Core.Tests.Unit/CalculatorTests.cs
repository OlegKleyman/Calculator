using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class CalculatorTests
    {
        [Theory]
        [MemberData(nameof(CalculatorTestsTheories.CalculateShouldReturnTheArithmeticResultOfFormulaCases), MemberType = typeof(CalculatorTestsTheories))]
        public void CalculateShouldReturnTheArithmeticResultOfFormula(IFormula formula, int result)
        {
            var calculator = GetCalculator();

            calculator.Calculate(formula).Should().Be(result);
        }

        [Fact]
        public void CalculateShouldThrowArgumentNullExceptionWhenFormulaIsNull()
        {
            var calculator = GetCalculator();

            Action calculate = () => calculator.Calculate(null);

            calculate
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().BeEquivalentTo("formula");
        }

        [Fact]
        public void CalculateShouldThrowArgumentExceptionWhenFormulaOperationsIsNull()
        {
            var calculator = GetCalculator();

            var formula = Substitute.For<IFormula>();
            formula.Operations.Returns((IEnumerable<Operation>) null);

            Action calculate = () => calculator.Calculate(formula);

            calculate
                .Should().Throw<ArgumentException>().WithMessage("Operations is null\r\nParameter name: formula")
                .Which.ParamName.Should().BeEquivalentTo("formula");
        }

        private Calculator GetCalculator()
        {
            return new Calculator();
        }

        public class CalculatorTestsTheories
        {
            public static IEnumerable<object[]> CalculateShouldReturnTheArithmeticResultOfFormulaCases()
            {
                var cases = new[]
                {
                    new object[]
                    {
                        GetFormula(1, (null, Op.Add)),
                        1
                    },
                    new object[]
                    {
                        GetFormula(1, (1, Op.Add), (null, Op.Add)),
                        2
                    },
                    new object[]
                    {
                        GetFormula(24, (27, Op.Add), (10, Op.Add), (null, Op.Add)),
                        61
                    },
                    new object[]
                    {
                        GetFormula(24, (0, Op.Add), (10, Op.Add), (null, Op.Add)),
                        34
                    },
                    new object[]
                    {
                        GetFormula(1, (1, Op.Add), (null, Op.Subtract)),
                        0
                    },
                    new object[]
                    {
                        GetFormula(24, (10, Op.Add), (14, Op.Subtract), (100, Op.Add), (null, Op.Add)),
                        128
                    },
                };

                return cases;
            }

            private static IFormula GetFormula(int seed, params (int? next, Op op)[] next)
            {
                var formula = Substitute.For<IFormula>();
                formula.CurrentValue.Returns(seed);

                var operations = next.Select(tuple =>
                {
                    var operation = Substitute.For<Operation>();
                    operation.Execute(Arg.Any<int>(), Arg.Any<int>()).Returns(info =>
                    {
                        var x = (int) info[0];
                        var y = (int)info[1];

                        formula.CurrentValue.Returns(tuple.next);
                        return tuple.op == Op.Add ? x + y : x - y;
                    });

                    return operation;
                });

                formula.Operations.Returns(operations);

                return formula;
            }
        }
    }
}
