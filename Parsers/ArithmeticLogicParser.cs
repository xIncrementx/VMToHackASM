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
                AlOperationType.Add => GetArithmeticOrLogicalOperation('+'),
                AlOperationType.Sub => GetArithmeticOrLogicalOperation('-'),
                AlOperationType.Or => GetArithmeticOrLogicalOperation('|'),
                AlOperationType.And => GetArithmeticOrLogicalOperation('&'),
                AlOperationType.Neg => new[] {"AM=M-1", "M=-M", "@SP", "AM=M+1"},
                AlOperationType.Not => new[] {"A=M-1", "M=!M"},
                AlOperationType.Eq => GetComparisonOperation("EQ"),
                AlOperationType.Gt => GetComparisonOperation("GT"),
                AlOperationType.Lt => GetComparisonOperation("LT"),
                _ => throw new ArgumentException("Operator not recognized.", nameof(alOperationType))
            });
            
            this.stackPointerListener.StackPointerFocused = false;

            return asmOperation;
        }

        private static IEnumerable<string> GetArithmeticOrLogicalOperation(char operatorSign) =>
            new[] {"AM=M-1", "D=M", "A=A-1", $"M=M{operatorSign}D"};

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

        private short GetNextNumber() => this.counter++;
    }
}