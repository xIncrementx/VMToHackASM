using System.Collections.Generic;
using VMToHackASM.Constants;
using VMToHackASM.Exceptions;
using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class InstructionPrototypeFactory
    {

        public static IEnumerable<IInstructionPrototype> CreateCollection(IEnumerable<IEnumerable<string[]>> allVmOperations)
        {
            var instructionHelperCollection = new List<IInstructionPrototype>();

            foreach (var vmOperations in allVmOperations)
            {
                var instructionHelpers = CreateCollection(vmOperations);
                instructionHelperCollection.AddRange(instructionHelpers);
            }

            return instructionHelperCollection;
        }

        private static IEnumerable<IInstructionPrototype> CreateCollection(IEnumerable<string[]> vmOperations)
        {
            var instructionHelpers = new List<IInstructionPrototype>();

            foreach (var vmOperationSplit in vmOperations)
            {
                string instruction = vmOperationSplit[0];
                var instructionType = InstructionStringToType(instruction);

                IInstructionPrototype instructionInstance = new InstructionPrototype(vmOperationSplit, instructionType);
                instructionHelpers.Add(instructionInstance);
            }

            return instructionHelpers;
        }

        private static InstructionType InstructionStringToType(string vmInstruction)
        {
            InstructionType type;

            switch (vmInstruction)
            {
                case VmInstructions.Push:
                case VmInstructions.Pop:
                    type = InstructionType.PushPop;
                    break;
                case VmInstructions.Add:
                case VmInstructions.Sub:
                case VmInstructions.Neg:
                case VmInstructions.Eq:
                case VmInstructions.Gt:
                case VmInstructions.Lt:
                case VmInstructions.And:
                case VmInstructions.Or:
                case VmInstructions.Not:
                    type = InstructionType.ArithmeticLogic;
                    break;
                case VmInstructions.IfGoto:
                case VmInstructions.Goto:
                case VmInstructions.Label:
                    type = InstructionType.Label;
                    break;
                case VmInstructions.Function:
                case VmInstructions.Call:
                    type = InstructionType.Function;
                    break;
                case VmInstructions.Return:
                    type = InstructionType.Return;
                    break;
                default:
                    throw new InvalidInstructionException(vmInstruction);
            }

            return type;
        }
    }
}