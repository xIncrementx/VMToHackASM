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

        public IEnumerable<string> ToHackAsm(IEnumerable<IVmInstruction> vmInstructions)
        {
            var asmOperations = new List<string>(100);

            foreach (var vmInstruction in vmInstructions)
            {
                switch (vmInstruction.Instruction)
                {
                    case VmInstruction.Operation:
                        var operation = (IVmOperation) vmInstruction;
                        var operationType = operation.VmOperationType;
                        var segment = operation.VmSegment;
                        short value = operation.Value;

                        asmOperations.AddRange(operationType switch
                        {
                            VmOperationType.Push => this.operationParser.GetPushOperation(segment, value),
                            VmOperationType.Pop => this.operationParser.GetPopOperation(segment, value),
                            _ => throw new ArgumentOutOfRangeException(nameof(operationType), "Operation does not exist.")
                        });
                        this.commandParser.StackPointerFocused = true;
                        break;
                    case VmInstruction.Command:
                        var command = (IVmCommand) vmInstruction;
                        var commandType = command.CommandType;

                        asmOperations.AddRange(this.commandParser.GetCommands(commandType));
                        this.commandParser.StackPointerFocused = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(commandType), "Command does not exist.");
                }
            }

            return asmOperations;
        }
    }
}