using System;
using System.Collections.Generic;

namespace VMToHackASM.Data
{
    public class AsmOperationManager
    {
        public static bool Pushed { get; private set; }
        private static short _counter;
        private static readonly string[][] ArithmeticOperations;
        private static IReadOnlyDictionary<string, Operator> OperatorMap { get; }

        static AsmOperationManager()
        {
            // DON'T INCREMENT SP HERE!!! ONLY EXECUTE COMMAND
            // IN OTHER WORDS, DON'T END COMMANDS WITH: @SP, @AM=M+1
            ArithmeticOperations = new[]
            {
                new[] {"@SP", "AM=M-1", "D=M", "A=A-1", "M=M+D"},
                new[] {"@SP", "AM=M-1", "D=M", "A=A-1", "M=M-D"},
                new[] {"@SP", "AM=M-1", "M=-M", "@SP", "AM=M+1"}
            };

            OperatorMap = new Dictionary<string, Operator>()
            {
                {"eq", Operator.Comparison},
                {"gt", Operator.Comparison},
                {"lt", Operator.Comparison},
                {"and", Operator.Logical},
                {"or", Operator.Logical},
                {"not", Operator.Logical},
                {"add", Operator.Arithmetic},
                {"sub", Operator.Arithmetic},
                {"neg", Operator.Arithmetic}
            };
        }

        public IEnumerable<string> GetOperation(string operation)
        {
            OperatorMap.TryGetValue(operation, out var op);

            return op switch
            {
                Operator.Logical => GetLogicalOperation(operation),
                Operator.Comparison => GetComparisonOperation(operation),
                _ => GetArithmeticOperation(operation)
            };
        }

        private static IEnumerable<string> GetArithmeticOperation(string operation)
        {
            return operation switch
            {
                "add" => ArithmeticOperations[0],
                "sub" => ArithmeticOperations[1],
                "neg" => ArithmeticOperations[2],
                _ => throw new Exception("Arithmetic operation does not exist.")
            };
        }

        private static IEnumerable<string> GetComparisonOperation(string operation)
        {
            operation = operation switch
            {
                "eq" => "JEQ",
                "lt" => "JLT",
                "gt" => "JGT",
                _ => throw new Exception("Comparison operator does not exist.")
            };

            string label1 = GetLabelNumberAsString();
            string label2 = GetLabelNumberAsString();

            return new[]
            {
                "@SP", "A=M-1", "A=A-1", "D=M", "A=A+1", "D=D-M",
                "@File." + label1, $"D;{operation}",
                "@File." + label2, "D=0", "0;JMP",
                $"(File.{label1})", "D=-1",
                $"(File.{label2})",
                "@SP", "AM=M-1", "A=A-1", "M=D"
            };
        }

        private static IEnumerable<string> GetLogicalOperation(string operation)
        {
            char c;

            switch (operation)
            {
                case "and":
                    c = '&';
                    break;
                case "or":
                    c = '|';
                    break;
                case "not":
                    return new[] {"@SP", "A=M-1", "M=!M"};
                default:
                    throw new Exception("Logical operator does not exist...");
            }

            return new[] {"@SP", "AM=M-1", "D=M", "A=A-1", $"M=M{c}D"};
        }

        private static string GetLabelNumberAsString() => _counter++.ToString();
    }
}