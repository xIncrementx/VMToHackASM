﻿using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class CommandParser : ICommandParser
    {
        private readonly string filename;
        private short counter;

        public CommandParser(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<string> GetCommands(CommandType commandType)
        {
            var asmOperation = new List<string>();
            bool stackPointerNotFocused = !StackPointerFocused;

            if (stackPointerNotFocused)
                asmOperation.Add("@SP");

            asmOperation.AddRange(commandType switch
            {
                CommandType.Add => GetArithmeticOrLogicalOperation('+'),
                CommandType.Sub => GetArithmeticOrLogicalOperation('-'),
                CommandType.Or => GetArithmeticOrLogicalOperation('|'),
                CommandType.And => GetArithmeticOrLogicalOperation('&'),
                CommandType.Neg => new[] {"AM=M-1", "M=-M", "@SP", "AM=M+1"},
                CommandType.Not => new[] {"A=M-1", "M=!M"},
                CommandType.Eq => GetComparisonOperation("EQ"),
                CommandType.Gt => GetComparisonOperation("GT"),
                CommandType.Lt => GetComparisonOperation("LT"),
                _ => throw new ArgumentException("Operator not recognized.", nameof(commandType))
            });

            return asmOperation;
        }

        public bool StackPointerFocused { get; set; }

        private static IEnumerable<string> GetArithmeticOrLogicalOperation(char operatorSign)
        {
            return new[] {"AM=M-1", "D=M", "A=A-1", $"M=M{operatorSign}D"};
        }

        private IEnumerable<string> GetComparisonOperation(string comparisonCommand)
        {
            short asmVarNumber1 = GetNextNumber();
            short asmVarNumber2 = GetNextNumber();

            return new[]
            {
                "AM=M-1", "D=M", "A=A-1", "D=M-D",
                $"@{this.filename}.{asmVarNumber1}", $"D;J{comparisonCommand}", "D=0",
                $"@{this.filename}.{asmVarNumber2}", "0;JMP",
                $"({this.filename}.{asmVarNumber1})", "D=-1",
                $"({this.filename}.{asmVarNumber2})", "@SP", "A=M-1", "M=D"
            };
        }

        private short GetNextNumber()
        {
            return this.counter++;
        }
    }
}