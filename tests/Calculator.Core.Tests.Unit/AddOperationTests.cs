using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class AddOperationTests
    {
        [Fact]
        public void ConstructorShouldSetValue()
        {
            var operation = new AddOperation(1);
            operation.Value.Should().Be(1);
        }

        [Fact]
        public void ExecuteShouldReturnSumOfArgumentAndValue()
        {
            var operation = GetAddOperation(1);
            operation.Execute(10).Should().Be(11);
        }

        private AddOperation GetAddOperation(int value)
        {
            return new AddOperation(value);
        }
    }
}
