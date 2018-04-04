using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class SymbolStreamTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ConstructorShouldSetEndOfStreamToTrueWhenThereAreNoSymbolsInItOrItIsNull(string formula)
        {
            var stream = new SymbolStream(formula, Substitute.For<ISymbolFactory>());

            stream.End.Should().BeTrue();
        }

        private SymbolStream GetSymbolStream(string formula, ISymbolFactory factory)
        {
            return new SymbolStream(formula, factory);
        }

        [Fact]
        public void ConstructorShouldThrowArgumentNullExceptionWhenFactoryIsNull()
        {
            Action constructor = () => new SymbolStream(default(string), null);
            constructor.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null.\r\nParameter name: factory");
        }

        [Fact]
        public void NextShouldReturnAllSymbolsInFormula()
        {
            var factory = Substitute.For<ISymbolFactory>();
            factory.GetNext("-1+2").Returns(new OperatorSymbol(Operator.Subtract));
            factory.GetNext("1+2").Returns(new IntegerSymbol(1));
            factory.GetNext("+2").Returns(new OperatorSymbol(Operator.Add));
            factory.GetNext("2").Returns(new IntegerSymbol(2));

            var stream = GetSymbolStream("-1+2", factory);

            stream.Next().Should().BeOfType<OperatorSymbol>().And.BeEquivalentTo(new {RawValue = "-"});
            stream.Next().Should().BeOfType<IntegerSymbol>().And.BeEquivalentTo(new {RawValue = "1"});
            stream.Next().Should().BeOfType<OperatorSymbol>().And.BeEquivalentTo(new {RawValue = "+"});
            stream.Next().Should().BeOfType<IntegerSymbol>().And.BeEquivalentTo(new {RawValue = "2"});
        }

        [Fact]
        public void NextShouldReturnNextNumberSymbolInFormula()
        {
            var factory = Substitute.For<ISymbolFactory>();
            factory.GetNext("1").Returns(new IntegerSymbol(1));
            var stream = GetSymbolStream("1", factory);

            var result = stream.Next();

            result.Should().BeOfType<IntegerSymbol>();
            result.RawValue.Should().BeEquivalentTo("1");
        }

        [Fact]
        public void NextShouldReturnNextOperatorSymbolInFormula()
        {
            var factory = Substitute.For<ISymbolFactory>();
            factory.GetNext("+").Returns(new OperatorSymbol(Operator.Add));
            var stream = GetSymbolStream("+", factory);

            var result = stream.Next();

            result.Should().BeOfType<OperatorSymbol>();
            result.RawValue.Should().BeEquivalentTo("+");
        }

        [Fact]
        public void NextShouldSetEndOfStreamToTrueWhenFactoryGetReturnsNull()
        {
            var factory = Substitute.For<ISymbolFactory>();
            var stream = GetSymbolStream("1", factory);

            stream.Next();

            stream.End.Should().BeTrue();
        }

        [Fact]
        public void NextShouldSetEndOfStreamToTrueWhenThereIsNoMoreFormulaToParse()
        {
            var factory = Substitute.For<ISymbolFactory>();
            factory.GetNext("1").Returns(new IntegerSymbol(1));
            var stream = GetSymbolStream("1", factory);

            stream.Next();

            stream.End.Should().BeTrue();
        }

        [Fact]
        public void NextShouldThrowInvalidOperationExceptionWhenStreamIsAtEnd()
        {
            var factory = Substitute.For<ISymbolFactory>();
            var stream = GetSymbolStream(default(string), factory);

            Action next = () => stream.Next();

            next.Should().Throw<InvalidOperationException>().WithMessage("End of stream has been reached");
        }

        [Fact]
        public void PeekShouldReturnNullWhenEndOfStreamIsReached()
        {
            var factory = Substitute.For<ISymbolFactory>();

            var stream = GetSymbolStream(default(string), factory);

            stream.Peek().Should().BeNull();
        }

        [Fact]
        public void PeekShouldReturnTheNextSymbolWithoutMovingTheStream()
        {
            var factory = Substitute.For<ISymbolFactory>();
            factory.GetNext("-1+2").Returns(new OperatorSymbol(Operator.Subtract));
            factory.GetNext("1+2").Returns(new IntegerSymbol(1));

            var stream = GetSymbolStream("-1+2", factory);

            stream.Peek().Should().BeEquivalentTo(stream.Next());
        }
    }
}