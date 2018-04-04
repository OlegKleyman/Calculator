using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
