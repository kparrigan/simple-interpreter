﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core
{
    public enum TokenType
    {
        INTEGER,
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        LEFT_PAREN,
        RIGHT_PAREN,
        EOF,
        BEGIN,
        END,
        DOT,
        ID,
        ASSIGN,
        SEMI
    }
}
