using System;
using System.Collections.Generic;
using VMToHackASM.Managers;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class PushPopParser : IVmParser<IPushPopOperation>
    {
        private readonly string fileName;
        private readonly IStackPointerListener stackPointerListener;

        public PushPopParser(string fileName, IStackPointerListener stackPointerListener)
        {
            this.fileName = fileName;
            this.stackPointerListener = stackPointerListener;
        }

        public IEnumerable<string> GetAsmOperation(IPushPopOperation pushPopOperation)
        {
            var operationType = pushPopOperation.Type;
            var segment = pushPopOperation.Segment;
            short value = pushPopOperation.Value;

            return operationType switch
            {
                PushPopOperationType.Push => GetPushOperation(segment, value),
                PushPopOperationType.Pop => GetPopOperation(segment, value),
                _ => throw new ArgumentOutOfRangeException(nameof(operationType), "Not a Push/Pop operation.")
            };
        }

        private IEnumerable<string> GetPushOperation(Segment segment, short value)
        {
            var asmCommands = new List<string> {"// Push "};

            asmCommands.AddRange(segment switch
            {
                Segment.Constant => new[] {$"@{value}", "D=A", "@SP", "A=M", "M=D"},
                Segment.Local => PushOperation("LCL", value),
                Segment.Argument => PushOperation("ARG", value),
                Segment.This => PushOperation("THIS", value),
                Segment.That => PushOperation("THAT", value),
                Segment.Temp => PushOperation("temp", value),
                Segment.Static => new[] {$"@{this.fileName}.{value}", "D=M", "@SP", "A=M", "M=D"},
                Segment.Pointer => new[] {$"@{(value == 0 ? "THIS" : "THAT")}", "D=M", "@SP", "A=M", "M=D"},
                _ => throw new ArgumentOutOfRangeException(nameof(segment), "Invalid segment.")
            });
            asmCommands.AddRange(IncrementStackPointer());
            
            this.stackPointerListener.StackPointerFocused = true;

            return asmCommands;
        }

        private IEnumerable<string> GetPopOperation(Segment segment, short value)
        {
            var asmCommands = new List<string> {"// Pop "};

            asmCommands.AddRange(segment switch
            {
                Segment.Local => PopOperation("LCL", value),
                Segment.Argument => PopOperation("ARG", value),
                Segment.This => PopOperation("THIS", value),
                Segment.That => PopOperation("THAT", value),
                Segment.Static => new[] {"@SP", "AM=M-1", "D=M", $"@{this.fileName}.{value}", "M=D"},
                Segment.Pointer => new[] {"@SP","AM=M-1", "D=M", $"@{(value == 0 ? "THIS" : "THAT")}", "M=D"},
                Segment.Temp => new[] {"@SP","AM=M-1", "D=M", $"@temp.{value}", "M=D"},
                _ => throw new ArgumentOutOfRangeException(nameof(segment),
                    "Segment not applicable for pop operations")
            });

            this.stackPointerListener.StackPointerFocused = false;

            return asmCommands;
        }

        private static IEnumerable<string> PushOperation(string segment, short value) =>
            new[] {$"@{value}", "D=A", $"@{segment}", "A=M+D", "D=M", "@SP", "A=M", "M=D"};

        private static IEnumerable<string> PopOperation(string segment, short value) =>
            new[]
            {
                $"@{segment}", "D=M", $"@{value}", "D=A+M", "@R15", "M=D", "@SP", "AM=M-1", "D=M", "@R15", "A=M", "M=D"
            };

        private static IEnumerable<string> IncrementStackPointer() => new[] {"@SP", "M=M+1"};
    }
}