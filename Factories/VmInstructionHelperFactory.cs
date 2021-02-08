using System.Collections.Generic;
using VMToHackASM.Constants;
using VMToHackASM.Exceptions;
using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class VmInstructionHelperFactory
    {
        public static IEnumerable<IVmInstructionHelper> CreateCollection(IEnumerable<string[]> vmOperations)
        {
            var vmInstructions = new List<IVmInstructionHelper>();

            foreach (var vmOperationSplit in vmOperations)
            {
                string vmInstruction = vmOperationSplit[0];
                var instructionType = GetMatchingInstruction(vmInstruction);

                IVmInstructionHelper vmInstructionInstance = new VmInstructionHelper(vmOperationSplit, instructionType);
                vmInstructions.Add(vmInstructionInstance);
            }

            return vmInstructions;
        }

        private static VmInstructionType GetMatchingInstruction(string vmInstruction)
        {
            VmInstructionType vmInstructionType;

            switch (vmInstruction)
            {
                case VmInstructions.Push:
                case VmInstructions.Pop:
                    vmInstructionType = VmInstructionType.Operation;
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
                    vmInstructionType = VmInstructionType.Command;
                    break;
                case VmInstructions.If:
                case VmInstructions.IfGoto:
                case VmInstructions.Goto:
                case VmInstructions.Label:
                    vmInstructionType = VmInstructionType.Statement;
                    break;
                case VmInstructions.Function:
                case VmInstructions.Call:
                case VmInstructions.Return:
                    vmInstructionType = VmInstructionType.Call;
                    break;
                default:
                    throw new InvalidVmInstructionException(vmInstruction);
            }

            return vmInstructionType;
        }
    }
}