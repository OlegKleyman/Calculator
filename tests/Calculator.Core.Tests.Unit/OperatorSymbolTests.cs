using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class OperatorSymbolTests
    {
        [Fact]
        public void ConstructorShouldSetRawValueWithPlusWhenOperatorIsAdd()
        {
            var symbol = new OperatorSymbol(Operator.Add);

            symbol.RawValue.Should().BeEquivalentTo("+");
        }

        [Fact]
        public void ConstructorShouldSetRawValueWithMinusWhenOperatorIsSubtract()
        {
            var symbol = new OperatorSymbol(Operator.Subtract);

            symbol.RawValue.Should().BeEquivalentTo("-");
        }

        [Fact]
        public void ConstructorShouldSetOperator()
        {
            var symbol = new OperatorSymbol(Operator.Subtract);

            symbol.Operator.Should().Be(Operator.Subtract);
        }
    }
}
