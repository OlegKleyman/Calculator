using FluentAssertions;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class ParserExceptionTests
    {
        [Fact]
        public void ConstructorShouldSetMessageProperty()
        {
            var exception = new ParserException("test");

            exception.Message.Should().BeEquivalentTo("test");
        }
    }
}