using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class OperationParser : IOperationParser
    {
        private readonly string fileName;

        public OperationParser(string fileName) => this.fileName = fileName;

        public IEnumerable<string> GetPushOperation(VmSegment vmSegment, short value)
        {
            var asmCommands = new List<string> {"// Push "};

            asmCommands.AddRange(vmSegment switch
            {
                VmSegment.Constant => new[] {$"@{value}", "D=A", "@SP", "A=M", "M=D"},
                VmSegment.Local => GetPushOperation("LCL", value),
                VmSegment.Argument => GetPushOperation("ARG", value),
                VmSegment.This => GetPushOperation("THIS", value),
                VmSegment.That => GetPushOperation("THAT", value),
                VmSegment.Temp => GetPushOperation("temp", value),
                VmSegment.Static => new[] {$"@{this.fileName}.{value}", "D=M", "@SP", "A=M", "M=D"},
                VmSegment.Pointer => new[] {$"@{(value == 0 ? "THIS" : "THAT")}", "D=M", "@SP", "A=M", "M=D"},
                _ => throw new Exception("Invalid segment.")
            });

            asmCommands.AddRange(GetIncrementStackPointer());

            return asmCommands;
        }

        public IEnumerable<string> GetPopOperation(VmSegment vmSegment, short value)
        {
            var asmCommands = new List<string>() {"// Pop "};

            asmCommands.AddRange(vmSegment switch
            {
                VmSegment.Local => GetPopOperation("LCL", value),
                VmSegment.Argument => GetPopOperation("ARG", value),
                VmSegment.This => GetPopOperation("THIS", value),
                VmSegment.That => GetPopOperation("THAT", value),
                VmSegment.Static => new[] {"@SP", "AM=M-1", "D=M", $"@{this.fileName}.{value}", "M=D"},
                VmSegment.Pointer => new[] {"@SP", "AM=M-1", "D=M", $"@{(value == 0 ? "THIS" : "THAT")}", "M=D"},
                VmSegment.Temp => new[] {"@SP", "AM=M-1", "D=M", $"@temp.{value}", "M=D"},
                _ => throw new ArgumentOutOfRangeException(nameof(vmSegment),
                    $"{VmSegment.Constant} not applicable for pop operations")
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