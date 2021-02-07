using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Managers
{
    public class OperationManager : IOperationManager
    {
        private readonly string fileName;

        public OperationManager(string fileName) => this.fileName = fileName;

        public short StackPtrMem { get; set; } = 256;
        public short LocalPtrMem { get; set; } = 300;
        public short ArgPtrMem { get; set; } = 400;
        public short ThisPtrMem { get; set; } = 3000;
        public short ThatPtrMem { get; set; } = 3010; 
        public short[] TempPtrsMem { get; set; } = new short[7];

        public IEnumerable<string> Push(VmSegment vmSegment, short value)
        {
            var commands = new List<string>();

            commands.AddRange(vmSegment switch
            {
                VmSegment.Constant => new[] {$"@{value}", "D=A", "@SP", "A=M", "M=D"},
                VmSegment.Local => GetSegment("LCL", value),
                VmSegment.Argument => GetSegment("ARG", value),
                VmSegment.This => GetSegment("THIS", value),
                VmSegment.That => GetSegment("THAT", value),
                VmSegment.Static => GetSegment(this.fileName, value),
                VmSegment.Pointer => GetPointerSegment(value),
                _ => throw new Exception("Invalid segment.")
            });

            commands.AddRange(GetIncrementStackPointer());

            return commands;
        }

        public IEnumerable<string> Pop(VmSegment vmSegment, short value)
        {
            var commands = new List<string>();

            commands.AddRange(vmSegment switch
            {
            });

            return null;
        }

        private static IEnumerable<string> GetPointerSegment(short pointerValue) =>
            GetSegment(pointerValue == 0 ? "THIS" : "THAT", pointerValue);

        private static IEnumerable<string> GetSegment(string segment, short value) => new[]
            {$"@{value}", "D=A", $"@{segment}", "A=M+D", "D=M", "@SP", "A=M","M=D"};

        private static IEnumerable<string> GetIncrementStackPointer() => new[] {"@SP", "M=M+1"};
    }
}