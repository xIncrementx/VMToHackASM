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

        public IEnumerable<string> Push(Segment segment, short value)
        {
            var commands = new List<string>();

            commands.AddRange(segment switch
            {
                Segment.Constant => new[] {$@"{value}", "D=A", "@SP", "A=M", "M=D"},
                Segment.Local => GetSegment("LCL", value),
                Segment.Argument => GetSegment("ARG", value),
                Segment.This => GetSegment("THIS", value),
                Segment.That => GetSegment("THAT", value),
                Segment.Static => GetSegment("static", value),
                Segment.Pointer => GetPointerSegment(value),
                _ => throw new Exception("Invalid segment.")
            });

            commands.Add("@SP");
            commands.Add("M=M+1");

            return commands;
        }

        public IEnumerable<string> Pop(Segment segment, short value)
        {
            var commands = new List<string>();

            commands.AddRange(segment switch
            {
            });

            return null;
        }

        private static IEnumerable<string> GetPointerSegment(short pointerValue) =>
            GetSegment(pointerValue == 0 ? "THIS" : "THAT", pointerValue);

        private static IEnumerable<string> GetSegment(string segment, short value) => new[]
            {$"@{segment}", "D=M", $"@{value}", "D=D+A", "@SP", "A=M", "M=D"};
    }
}