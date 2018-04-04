using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class SubtractOperationTests
    {
        [Fact]
        public void ConstructorShouldSetValue()
        {
            var operation = new SubtractOperation(10);
            operation.Value.Should().Be(10);
        }

        [Fact]
        public void ExecuteShouldReturnDifferenceOfArgumentAndValue()
        {
            var operation = GetSubtractOperation(1);
            operation.Execute(10).Should().Be(9);
        }

        private SubtractOperation GetSubtractOperation(int value)
        {
            return new SubtractOperation(value);
        }
    }
}
