using System;
using System.Collections.Generic;
using VMToHackASM.Managers;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class VmParser
    {
        private readonly IOperationManager operationManager;
        private readonly ICommandManager commandManager;

        public VmParser(IOperationManager operationManager, ICommandManager commandManager)
        {
            this.operationManager = operationManager;
            this.commandManager = commandManager;
        }

        public IEnumerable<string> ToHackAsm(IEnumerable<IVmInstruction> vmInstructions)
        {
            var asmCommands = new List<string>(100);

            foreach (var vmInstruction in vmInstructions)
            {
                switch (vmInstruction.Instruction)
                {
                    case VmInstruction.Operation:
                        var operation = (IVmOperation) vmInstruction;
                        var operationType = operation.VmOperationType;
                        var segment = operation.Segment;
                        short value = operation.Value;

                        asmCommands.AddRange(operationType switch
                        {
                            VmOperationType.Push => this.operationManager.Push(segment, value),
                            VmOperationType.Pop => this.operationManager.Pop(segment, value),
                            _ => throw new Exception("Operation does not exist.")
                        });
                        this.commandManager.StackPointerFocused = true;
                        break;
                    case VmInstruction.Command:
                        var command = (IVmCommand) vmInstruction;
                        var commandType = command.CommandType;

                        asmCommands.AddRange(this.commandManager.GetCommands(commandType));
                        this.commandManager.StackPointerFocused = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return asmCommands;
        }
    }
}