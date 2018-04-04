using FluentAssertions;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class SymbolExceptionTests
    {
        [Fact]
        public void ConstructorShouldInitializeMessageAndSymbol()
        {
            var exception = new SymbolException("test", "symbol");

            exception.Should().BeEquivalentTo(new {Message = "test", Symbol = "symbol"});
        }
    }
}