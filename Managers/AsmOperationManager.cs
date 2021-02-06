using System;
using System.Collections.Generic;
using VMToHackASM.Data;

namespace VMToHackASM.Managers
{
    public class AsmOperationManager
    {
        private readonly string filename;
        private short counter;

        public AsmOperationManager(string filename) => this.filename = filename;

        public IEnumerable<string> GetOperation(string vmOperation)
        {
            var operation = new List<string>();
            var operationEnum = GetOperationEnum(vmOperation);
            bool stackPointerNotFocused = !StackPointerFocused;

            if (stackPointerNotFocused)
                operation.Add("@SP");

            operation.AddRange(operationEnum switch
            {
                AnyOperation.Addition => GetAndOrAddSubOperation('+'),
                AnyOperation.Subtraction => GetAndOrAddSubOperation('-'),
                AnyOperation.And => GetAndOrAddSubOperation('&'),
                AnyOperation.Or => GetAndOrAddSubOperation('|'),
                AnyOperation.UnaryNegation => new[] {"AM=M-1", "M=-M", "@SP", "AM=M+1"},
                AnyOperation.Not => new[] {"A=M-1", "M=!M"},
                AnyOperation.Comparison => GetComparisonOperation(vmOperation),
                _ => throw new Exception("Operator does not exist.")
            });

            return operation;
        }

        public bool StackPointerFocused { get; set; }

        private static AnyOperation GetOperationEnum(string vmOperation)
        {
            return vmOperation switch
            {
                "eq" => AnyOperation.Comparison,
                "gt" => AnyOperation.Comparison,
                "lt" => AnyOperation.Comparison,
                "and" => AnyOperation.And,
                "or" => AnyOperation.Or,
                "not" => AnyOperation.Not,
                "add" => AnyOperation.Addition,
                "sub" => AnyOperation.Subtraction,
                "neg" => AnyOperation.UnaryNegation,
                _ => throw new Exception("Operation enum does not exist.")
            };
        }

        /// <summary>
        /// The ASM operations for AND, OR, addition and subtraction only differ by their operator sign.
        /// </summary>
        /// <param name="operatorSign"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetAndOrAddSubOperation(char operatorSign) =>
            new[] {"AM=M-1", "D=M", "A=A-1", $"M=M{operatorSign}D"};

        private IEnumerable<string> GetComparisonOperation(string comparisonOperation)
        {
            comparisonOperation = comparisonOperation.ToUpper();

            string asmLabelName1 = GetNextNumber();
            string asmLabelName2 = GetNextNumber();

            return new[]
            {
                "AM=M-1", "D=M", "A=A-1", "D=M-D",
                $"@{this.filename}.{asmLabelName1}", $"D;J{comparisonOperation}", "D=0",
                $"@{this.filename}.{asmLabelName2}", "0;JMP",
                $"({this.filename}.{asmLabelName1})", "D=-1",
                $"({this.filename}.{asmLabelName2})", "@SP", "A=M-1", "M=D"
            };
        }

        private string GetNextNumber() => this.counter++.ToString();
    }
}