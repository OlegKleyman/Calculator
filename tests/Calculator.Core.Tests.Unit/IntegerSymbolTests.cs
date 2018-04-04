using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class IntegerSymbolTests
    {
        [Fact]
        public void ConstructorShouldSetRawValue()
        {
            var symbol = new IntegerSymbol(1);
            symbol.RawValue.Should().Be("1");
        }

        [Fact]
        public void ConstructorShouldSetValue()
        {
            var symbol = new IntegerSymbol(1);
            symbol.Value.Should().Be(1);
        }
    }
}
