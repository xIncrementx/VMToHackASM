using System;
using System.Collections.Generic;
using VMToHackASM.Models;
using VMToHackASM.Parsers;

namespace VMToHackASM.Managers
{
    public class VmParserManager
    {
        private readonly ICommandParser commandParser;
        private readonly IOperationParser operationParser;
        private readonly IStatementParser statementParser;
        private readonly ICallParser callParser;

        public VmParserManager(IVmParser vmParser)
        {
            this.commandParser = vmParser.CommandParser;
            this.operationParser = vmParser.OperationParser;
            this.statementParser = vmParser.StatementParser;
            this.callParser = vmParser.CallParser;
        }

        public IEnumerable<string> ToHackAsm(IEnumerable<IInstruction> instructions)
        {
            var asmOperations = new List<string>(100);

            foreach (var instruction in instructions)
                switch (instruction.InstructionType)
                {
                    case InstructionType.Operation:
                        var operation = (IOperation) instruction;
                        var operationType = operation.OperationType;
                        var segment = operation.Segment;
                        short value = operation.Value;

                        asmOperations.AddRange(operationType switch
                        {
                            OperationType.Push => this.operationParser.GetPushOperation(segment, value),
                            OperationType.Pop => this.operationParser.GetPopOperation(segment, value),
                            _ => throw new ArgumentOutOfRangeException(nameof(operationType),
                                "Operation does not exist.")
                        });
                        this.commandParser.StackPointerFocused = true;
                        break;
                    case InstructionType.Command:
                        var command = (ICommand) instruction;
                        var commandType = command.CommandType;

                        var commands = this.commandParser.GetCommands(commandType);
                        asmOperations.AddRange(commands);
                        this.commandParser.StackPointerFocused = false;
                        break;
                    case InstructionType.Call:
                        var call = (IFunction) instruction;

                        break;
                    case InstructionType.Statement:
                        var statement = (ILabel) instruction;
                        var statementType = statement.LabelType;

                        var statements = this.statementParser.GetStatements(statementType);
                        asmOperations.AddRange(statements);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(instruction.ToString(), "Instruction does not exist.");
                }

            return asmOperations;
        }
    }
}