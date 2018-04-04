using FluentAssertions;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class SubtractOperationTests
    {
        private SubtractOperation GetSubtractOperation(int value)
        {
            return new SubtractOperation(value);
        }

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
    }
}