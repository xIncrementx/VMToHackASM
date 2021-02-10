using System;
using System.Collections.Generic;
using VMToHackASM.Managers;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class ArithmeticLogicParser : IVmParser<IAlOperation>
    {
        private readonly IStackPointerListener stackPointerListener;
        private readonly string filename;
        private short counter;

        public ArithmeticLogicParser(string filename, IStackPointerListener stackPointerListener)
        {
            this.filename = filename;
            this.stackPointerListener = stackPointerListener;
        }

        private bool StackPointerNotFocused => !this.stackPointerListener.StackPointerFocused;

        public IEnumerable<string> GetAsmOperation(IAlOperation alOperation)
        {
            var asmOperation = new List<string>();
            var alOperationType = alOperation.Type;

            if (StackPointerNotFocused) asmOperation.Add("@SP");

            asmOperation.AddRange(alOperationType switch
            {
                AlOperationType.Add => ArithmeticLogicOperation('+'),
                AlOperationType.Sub => ArithmeticLogicOperation('-'),
                AlOperationType.Or => ArithmeticLogicOperation('|'),
                AlOperationType.And => ArithmeticLogicOperation('&'),
                AlOperationType.Neg => new[] {"AM=M-1", "M=-M", "@SP", "AM=M+1"},
                AlOperationType.Not => new[] {"A=M-1", "M=!M"},
                AlOperationType.Eq => ComparisonOperation("EQ"),
                AlOperationType.Gt => ComparisonOperation("GT"),
                AlOperationType.Lt => ComparisonOperation("LT"),
                _ => throw new ArgumentException("Operator not recognized.", nameof(alOperationType))
            });
            
            this.stackPointerListener.StackPointerFocused = false;

            return asmOperation;
        }

        private static IEnumerable<string> ArithmeticLogicOperation(char lOperator) =>
            new[] {"AM=M-1", "D=M", "A=A-1", $"M=M{lOperator}D"};

        private IEnumerable<string> ComparisonOperation(string cOperator)
        {
            short asmVarNumber1 = IncrementCounter();
            short asmVarNumber2 = IncrementCounter();

            return new[]
            {
                "AM=M-1", "D=M", "A=A-1", "D=M-D",
                $"@{this.filename}.{asmVarNumber1}", $"D;J{cOperator}", "D=0",
                $"@{this.filename}.{asmVarNumber2}", "0;JMP",
                $"({this.filename}.{asmVarNumber1})", "D=-1",
                $"({this.filename}.{asmVarNumber2})", "@SP", "A=M-1", "M=D"
            };
        }

        private short IncrementCounter() => this.counter++;
    }
}