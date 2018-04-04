using FluentAssertions;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class OperatorSymbolTests
    {
        [Fact]
        public void ConstructorShouldSetOperator()
        {
            var symbol = new OperatorSymbol(Operator.Subtract);

            symbol.Operator.Should().Be(Operator.Subtract);
        }

        [Fact]
        public void ConstructorShouldSetRawValueWithMinusWhenOperatorIsSubtract()
        {
            var symbol = new OperatorSymbol(Operator.Subtract);

            symbol.RawValue.Should().BeEquivalentTo("-");
        }

        [Fact]
        public void ConstructorShouldSetRawValueWithPlusWhenOperatorIsAdd()
        {
            var symbol = new OperatorSymbol(Operator.Add);

            symbol.RawValue.Should().BeEquivalentTo("+");
        }
    }
}