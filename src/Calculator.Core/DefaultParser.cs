using System;
using System.Collections.Generic;

namespace Calculator.Core
{
    public class DefaultParser : IParser
    {
        public IEnumerable<Operation> Parse(ISymbolStream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var operations = new List<Operation>();
            if (stream.Peek() is IntegerSymbol value) operations.Add(new AddOperation(value.Value));

            while (!stream.End)
            {
                var symbol = stream.Next();
                var peeked = stream.Peek();

                if (peeked != null && symbol.GetType() == peeked.GetType())
                {
                    var errorIdentifier = symbol is IntegerSymbol ? "operator" : "number";
                    throw new ParserException($"'{peeked.RawValue}' is not a valid {errorIdentifier}");
                }

                if (symbol is OperatorSymbol op)
                {
                    if (peeked == null) throw new ParserException($"Invalid end '{symbol.RawValue}'");

                    if (peeked is IntegerSymbol integer)
                    {
                        var operation = op.Operator == Operator.Add
                            ? (Operation) new AddOperation(integer.Value)
                            : new SubtractOperation(integer.Value);

                        operations.Add(operation);
                    }
                }
            }

            return operations;
        }
    }
}