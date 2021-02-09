using System.Collections.Generic;
using VMToHackASM.Constants;
using VMToHackASM.Exceptions;
using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class InstructionHelperFactory
    {

        public static IEnumerable<IInstructionHelper> CreateCollection(IEnumerable<IEnumerable<string[]>> allVmOperations)
        {
            var instructionHelperCollection = new List<IInstructionHelper>();

            foreach (var vmOperations in allVmOperations)
            {
                var instructionHelpers = CreateCollection(vmOperations);
                instructionHelperCollection.AddRange(instructionHelpers);
            }

            return instructionHelperCollection;
        }

        private static IEnumerable<IInstructionHelper> CreateCollection(IEnumerable<string[]> vmOperations)
        {
            var instructionHelpers = new List<IInstructionHelper>();

            foreach (var vmOperationSplit in vmOperations)
            {
                string vmInstruction = vmOperationSplit[0];
                var instructionType = GetMatchingInstruction(vmInstruction);

                IInstructionHelper instructionInstance = new InstructionHelper(vmOperationSplit, instructionType);
                instructionHelpers.Add(instructionInstance);
            }

            return instructionHelpers;
        }

        private static InstructionType GetMatchingInstruction(string vmInstruction)
        {
            InstructionType instructionType;

            switch (vmInstruction)
            {
                case VmInstructions.Push:
                case VmInstructions.Pop:
                    instructionType = InstructionType.Operation;
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
                    instructionType = InstructionType.Command;
                    break;
                case VmInstructions.IfGoto:
                case VmInstructions.Goto:
                case VmInstructions.Label:
                    instructionType = InstructionType.Statement;
                    break;
                case VmInstructions.Function:
                case VmInstructions.Call:
                case VmInstructions.Return:
                    instructionType = InstructionType.Call;
                    break;
                default:
                    throw new InvalidInstructionException(vmInstruction);
            }

            return instructionType;
        }
    }
}