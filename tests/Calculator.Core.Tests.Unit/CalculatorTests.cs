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
                        GetFormula((1, Op.Add)),
                        1
                    },
                    new object[]
                    {
                        GetFormula((1, Op.Add), (1, Op.Add)),
                        2
                    },
                    new object[]
                    {
                        GetFormula((24, Op.Add), (27, Op.Add), (10, Op.Add)),
                        61
                    },
                    new object[]
                    {
                        GetFormula((24, Op.Add), (0, Op.Add), (10, Op.Add)),
                        34
                    },
                    new object[]
                    {
                        GetFormula((10, Op.Add), (10, Op.Subtract)),
                        0
                    },
                    new object[]
                    {
                        GetFormula((32, Op.Add), (10, Op.Add), (14, Op.Subtract), (100, Op.Add)),
                        128
                    },
                };

                return cases;
            }

            private static IFormula GetFormula(params (int next, Op op)[] next)
            {
                var formula = Substitute.For<IFormula>();
                
                var operations = next.Select(tuple =>
                {
                    var operation = Substitute.For<Operation>();
                    operation.Execute(Arg.Any<int>()).Returns(info =>
                    {
                        var x = (int) info[0];

                        
                        return tuple.op == Op.Add ? x + tuple.next : x - tuple.next;
                    });

                    return operation;
                });

                formula.Operations.Returns(operations);

                return formula;
            }
        }
    }
}
