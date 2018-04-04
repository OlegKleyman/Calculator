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
        [MemberData(nameof(CalculatorTestsTheories.CalculateShouldReturnTheArithmeticResultOperationCalculations), MemberType = typeof(CalculatorTestsTheories))]
        public void CalculateShouldReturnTheArithmeticResultOperationCalculations(IEnumerable<Operation> operations, int result)
        {
            var calculator = GetCalculator();

            calculator.Calculate(operations).Should().Be(result);
        }

        [Fact]
        public void CalculateShouldThrowArgumentNullExceptionWhenOperationsIsNull()
        {
            var calculator = GetCalculator();

            Action calculate = () => calculator.Calculate((IEnumerable<Operation>) null);

            calculate
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().BeEquivalentTo("operations");
        }

        private Calculator GetCalculator()
        {
            return new Calculator();
        }

        public class CalculatorTestsTheories
        {
            private static IEnumerable<Operation> GetOperations(params (int next, Op op)[] next)
            {
                var operations = next.Select(tuple =>
                {
                    var operation = Substitute.For<Operation>(default(int));
                    operation.Execute(Arg.Any<int>()).Returns(info =>
                    {
                        var x = (int)info[0];


                        return tuple.op == Op.Add ? x + tuple.next : x - tuple.next;
                    });

                    return operation;
                });

                return operations;
            }

            public static IEnumerable<object[]> CalculateShouldReturnTheArithmeticResultOperationCalculations()
            {
                var cases = new[]
                {
                    new object[]
                    {
                        GetOperations((1, Op.Add)),
                        1
                    },
                    new object[]
                    {
                        GetOperations((1, Op.Add), (1, Op.Add)),
                        2
                    },
                    new object[]
                    {
                        GetOperations((24, Op.Add), (27, Op.Add), (10, Op.Add)),
                        61
                    },
                    new object[]
                    {
                        GetOperations((24, Op.Add), (0, Op.Add), (10, Op.Add)),
                        34
                    },
                    new object[]
                    {
                        GetOperations((10, Op.Add), (10, Op.Subtract)),
                        0
                    },
                    new object[]
                    {
                        GetOperations((32, Op.Add), (10, Op.Add), (14, Op.Subtract), (100, Op.Add)),
                        128
                    },
                };

                return cases;
            }
        }
    }
}
