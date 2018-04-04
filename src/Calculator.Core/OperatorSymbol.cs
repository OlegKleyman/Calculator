﻿using System;

namespace Calculator.Core
{
    public class OperatorSymbol : Symbol
    {
        public Operator Operator { get; }

        public OperatorSymbol(Operator op) : base(op == Operator.Add ? "+" : "-")
        {
            Operator = op;
        }
    }
}