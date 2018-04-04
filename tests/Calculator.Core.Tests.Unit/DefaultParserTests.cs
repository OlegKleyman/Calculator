using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Calculator.Core.Tests.Unit
{
    public class DefaultParserTests
    {
        [Fact]
        public void ParseShouldReturnFormulaFromStreamWithSingleIntegerSymbolWithSign()
        {
            var parser = GetDefaultParser();

            var stream = Substitute.For<ISymbolStream>();
            stream.End.Returns(false);
            var symbols = new Queue<Symbol>(new Symbol[] {new OperatorSymbol(Operator.Add), new IntegerSymbol(1)});

            stream.Next().Returns(info =>
            {
                var next = symbols.Dequeue();
                
                var peeked = symbols.Count > 0 ? symbols.Peek() : null;
                stream.End.Returns(peeked == null);
                stream.Peek().Returns(peeked);
                return next;
            });

            var result = parser.Parse(stream).ToList();

            result.Should().BeEquivalentTo(new AddOperation(1));
            result.Select(operation => operation.GetType()).Should()
                .BeEquivalentTo(typeof(AddOperation));
        }

        [Fact]
        public void ParseShouldReturnFormulaFromStreamWithSingleIntegerSymbolWithoutSign()
        {
            var parser = GetDefaultParser();

            var stream = Substitute.For<ISymbolStream>();
            stream.End.Returns(false);
            var symbols = new Queue<Symbol>(new Symbol[] { new IntegerSymbol(1) });
            stream.Peek().Returns(symbols.Peek());

            stream.Next().Returns(info =>
            {
                var next = symbols.Dequeue();

                var peeked = symbols.Count > 0 ? symbols.Peek() : null;
                stream.End.Returns(peeked == null);
                stream.Peek().Returns(peeked);
                return next;
            });

            var result = parser.Parse(stream);

            result.Should().BeEquivalentTo(new AddOperation(1));
            result.Select(operation => operation.GetType()).Should()
                .BeEquivalentTo(typeof(AddOperation));
        }

        [Fact]
        public void ParseShouldReturnFormulaFromStreamWithMultipleIntegers()
        {
            var parser = GetDefaultParser();

            var stream = Substitute.For<ISymbolStream>();
            stream.End.Returns(false);
            var symbols = new Queue<Symbol>(new Symbol[] { new OperatorSymbol(Operator.Add), new IntegerSymbol(1), new OperatorSymbol(Operator.Add), new IntegerSymbol(1),  });

            stream.Next().Returns(info =>
            {
                var next = symbols.Dequeue();

                var peeked = symbols.Count > 0 ? symbols.Peek() : null;
                stream.End.Returns(peeked == null);
                stream.Peek().Returns(peeked);
                return next;
            });

            var result = parser.Parse(stream);

            result.Should().BeEquivalentTo(new AddOperation(1), new AddOperation(1));
            result.Select(operation => operation.GetType()).Should()
                .BeEquivalentTo(typeof(AddOperation), typeof(AddOperation));
        }

        [Fact]
        public void ParseShouldThrowArgumentNullExceptionWhenStreamIsNull()
        {
            var parser = GetDefaultParser();

            Action parse = () => parser.Parse(null);

            parse.Should().Throw<ArgumentNullException>().Which.ParamName.Should().BeEquivalentTo("stream");
        }

        [Fact]
        public void ParseShouldThrowParserExceptionWhenStreamDoesNotContainIntegerAfterOperator()
        {
            var parser = GetDefaultParser();

            var stream = Substitute.For<ISymbolStream>();
            stream.End.Returns(false);
            var symbols = new Queue<Symbol>(new Symbol[] { new OperatorSymbol(Operator.Add)});

            stream.Next().Returns(info =>
            {
                var next = symbols.Dequeue();

                var peeked = symbols.Count > 0 ? symbols.Peek() : null;
                stream.End.Returns(peeked == null);
                stream.Peek().Returns(peeked);
                return next;
            });

            Action parse = () => parser.Parse(stream);

            parse.Should().Throw<ParserException>().WithMessage("Invalid end '+'");
        }

        [Fact]
        public void ParseShouldThrowParserExceptionWhenStreamContainsTwoOfTheSameSymbolsConsecutively()
        {
            var parser = GetDefaultParser();

            var stream = Substitute.For<ISymbolStream>();
            stream.End.Returns(false);
            var symbols = new Queue<Symbol>(new Symbol[] { new IntegerSymbol(1), new IntegerSymbol(1),   });

            stream.Next().Returns(info =>
            {
                var next = symbols.Dequeue();

                var peeked = symbols.Count > 0 ? symbols.Peek() : null;
                stream.End.Returns(peeked == null);
                stream.Peek().Returns(peeked);
                return next;
            });

            Action parse = () => parser.Parse(stream);

            parse.Should().Throw<ParserException>().WithMessage("'1' is not a valid operator");
        }

        private DefaultParser GetDefaultParser()
        {
            return new DefaultParser();
        }
    }
}
