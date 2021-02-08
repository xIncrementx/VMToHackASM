using System.Collections.Generic;
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
                case "push":
                case "pop":
                    vmInstructionType = VmInstructionType.Operation;
                    break;
                case "add":
                case "sub":
                case "neg":
                case "eq":
                case "gt":
                case "lt":
                case "and":
                case "or":
                case "not":
                    vmInstructionType = VmInstructionType.Command;
                    break;
                case "if":
                case "if-goto":
                case "goto":
                case "label":
                    vmInstructionType = VmInstructionType.Statement;
                    break;
                case "function":
                case "call":
                case "return":
                    vmInstructionType = VmInstructionType.Call;
                    break;
                default:
                    throw new InvalidVmInstructionException(vmInstruction);
            }

            return vmInstructionType;
        }
    }
}