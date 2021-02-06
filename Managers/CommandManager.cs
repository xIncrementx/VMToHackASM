using System;
using System.Collections.Generic;
using VMToHackASM.Data;

namespace VMToHackASM.Managers
{
    public class CommandManager : ICommandManager
    {
        private readonly string filename;
        private short counter;

        public CommandManager(string filename) => this.filename = filename;

        public IEnumerable<string> GetCommands(string vmOperation)
        {
            var commands = new List<string>();
            var commandEnum = GetOperationEnum(vmOperation);
            bool stackPointerNotFocused = !StackPointerFocused;

            if (stackPointerNotFocused)
                commands.Add("@SP");

            commands.AddRange(commandEnum switch
            {
                AnyOperation.Addition => GetArithmeticOrLogicalCommand('+'),
                AnyOperation.Subtraction => GetArithmeticOrLogicalCommand('-'),
                AnyOperation.And => GetArithmeticOrLogicalCommand('&'),
                AnyOperation.Or => GetArithmeticOrLogicalCommand('|'),
                AnyOperation.UnaryNegation => new[] {"AM=M-1", "M=-M", "@SP", "AM=M+1"},
                AnyOperation.Not => new[] {"A=M-1", "M=!M"},
                AnyOperation.Comparison => GetComparisonOperation(vmOperation),
                _ => throw new Exception("Operator does not exist.")
            });

            return commands;
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
        /// The ASM command for AND, OR, addition and subtraction only differ by their operator sign.
        /// </summary>
        /// <param name="operatorSign"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetArithmeticOrLogicalCommand(char operatorSign) =>
            new[] {"AM=M-1", "D=M", "A=A-1", $"M=M{operatorSign}D"};

        private IEnumerable<string> GetComparisonOperation(string comparisonCommand)
        {
            comparisonCommand = comparisonCommand.ToUpper();

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