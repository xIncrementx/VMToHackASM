using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class OperationParser : IOperationParser
    {
        private readonly string fileName;

        public OperationParser(string fileName) => this.fileName = fileName;

        public IEnumerable<string> GetPushOperation(Segment segment, short value)
        {
            var asmCommands = new List<string> {"// Push "};

            asmCommands.AddRange(segment switch
            {
                Segment.Constant => new[] {$"@{value}", "D=A", "@SP", "A=M", "M=D"},
                Segment.Local => GetPushOperation("LCL", value),
                Segment.Argument => GetPushOperation("ARG", value),
                Segment.This => GetPushOperation("THIS", value),
                Segment.That => GetPushOperation("THAT", value),
                Segment.Temp => GetPushOperation("temp", value),
                Segment.Static => new[] {$"@{this.fileName}.{value}", "D=M", "@SP", "A=M", "M=D"},
                Segment.Pointer => new[] {$"@{(value == 0 ? "THIS" : "THAT")}", "D=M", "@SP", "A=M", "M=D"},
                _ => throw new ArgumentOutOfRangeException(nameof(segment), "Invalid segment.")
            });

            asmCommands.AddRange(GetIncrementStackPointer());

            return asmCommands;
        }

        public IEnumerable<string> GetPopOperation(Segment segment, short value)
        {
            var asmCommands = new List<string>() {"// Pop "};

            asmCommands.AddRange(segment switch
            {
                Segment.Local => GetPopOperation("LCL", value),
                Segment.Argument => GetPopOperation("ARG", value),
                Segment.This => GetPopOperation("THIS", value),
                Segment.That => GetPopOperation("THAT", value),
                Segment.Static => new[] {"@SP", "AM=M-1", "D=M", $"@{this.fileName}.{value}", "M=D"},
                Segment.Pointer => new[] {"@SP", "AM=M-1", "D=M", $"@{(value == 0 ? "THIS" : "THAT")}", "M=D"},
                Segment.Temp => new[] {"@SP", "AM=M-1", "D=M", $"@temp.{value}", "M=D"},
                _ => throw new ArgumentOutOfRangeException(nameof(segment),
                    "Segment not applicable for pop operations")
            });

            return asmCommands;
        }

        private static IEnumerable<string> GetPopOperation(string segment, short value) => new[]
            {$"@{value}", "D=A", $"@{segment}", "D=D+M", "@R15", "M=D", "@SP", "AM=M-1", "D=M", "@R15", "A=M", "M=D"};

        private static IEnumerable<string> GetPushOperation(string segment, short value) => new[]
            {$"@{value}", "D=A", $"@{segment}", "A=M+D", "D=M", "@SP", "A=M", "M=D"};

        private static IEnumerable<string> GetIncrementStackPointer() => new[] {"@SP", "M=M+1"};
    }
}