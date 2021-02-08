using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class VmInstructionFactory
    {
        /// <summary>
        /// Creates a collection of VMInstructions from the string collection given.
        /// </summary>
        /// <param name="instructionHelpers"></param>
        /// <returns></returns>
        public static IEnumerable<IVmInstruction> CreateCollection(IEnumerable<IVmInstructionHelper> instructionHelpers)
        {
            var instructionInstances = new List<IVmInstruction>();

            foreach (var instructionHelper in instructionHelpers)
            {
                var vmInstructionType = instructionHelper.InstructionType;
                var instructionSplit = instructionHelper.InstructionSplit;

                instructionInstances.Add(vmInstructionType switch
                {
                    VmInstructionType.Operation => VmOperationFactory.Create(instructionSplit),
                    VmInstructionType.Command => VmCommandFactory.Create(instructionSplit),
                    VmInstructionType.Call => VmCallFactory.Create(instructionSplit),
                    VmInstructionType.Statement => VmStatementFactory.Create(instructionSplit),
                    _ => throw new Exception("No such type.")
                });
            }

            return instructionInstances;
        }
    }
}