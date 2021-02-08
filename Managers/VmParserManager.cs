using System;
using System.Collections.Generic;
using VMToHackASM.Models;
using VMToHackASM.Parsers;

namespace VMToHackASM.Managers
{
    public class VmParserManager
    {
        private readonly IOperationParser operationParser;
        private readonly ICommandParser commandParser;

        public VmParserManager(IOperationParser operationParser, ICommandParser commandParser)
        {
            this.operationParser = operationParser;
            this.commandParser = commandParser;
        }

        public IEnumerable<string> ToHackAsm(IEnumerable<IInstruction> instructions)
        {
            var asmOperations = new List<string>(100);

            foreach (var instruction in instructions)
            {
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
                            _ => throw new ArgumentOutOfRangeException(nameof(operationType), "Operation does not exist.")
                        });
                        this.commandParser.StackPointerFocused = true;
                        break;
                    case InstructionType.Command:
                        var command = (ICommand) instruction;
                        var commandType = command.CommandType;

                        asmOperations.AddRange(this.commandParser.GetCommands(commandType));
                        this.commandParser.StackPointerFocused = false;
                        break;
                    case InstructionType.Call:
                        break;
                    case InstructionType.Statement:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(instruction.ToString(), "Instruction does not exist.");
                }
            }

            return asmOperations;
        }
    }
}