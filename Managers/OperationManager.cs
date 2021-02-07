using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Managers
{
    public class OperationManager : IOperationManager
    {
        private short spPtrMem;
        private short lclPtrMem;
        private short argPtrMem;
        private short thisPtrMem;
        private short thatPtrMem;
        private short[] tempPtrsMem;

        public OperationManager(short spPtrMem, short lclPtrMem, short argPtrMem, short thisPtrMem, short thatPtrMem,
            params short[] tempPtrsMem)
        {
            this.spPtrMem = spPtrMem;
            this.lclPtrMem = lclPtrMem;
            this.argPtrMem = argPtrMem;
            this.thisPtrMem = thisPtrMem;
            this.thatPtrMem = thatPtrMem;
            this.tempPtrsMem = tempPtrsMem;
        }

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
                VmSegment.Static => GetSegment("static", value),
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
            {$"@{segment}", "D=M", $"@{value}", "D=D+A", "@SP", "A=M", "M=D"};

        private static IEnumerable<string> GetIncrementStackPointer() => new[] {"@SP", "M=M+1"};
    }
}