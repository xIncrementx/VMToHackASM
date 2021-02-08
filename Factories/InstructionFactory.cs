using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class InstructionFactory
    {
        public static IEnumerable<IInstruction> CreateCollection(IEnumerable<IInstructionHelper> instructionHelpers)
        {
            var instructionInstances = new List<IInstruction>();

            foreach (var instructionHelper in instructionHelpers)
            {
                var instructionType = instructionHelper.InstructionType;
                var instructionSplit = instructionHelper.InstructionSplit;

                instructionInstances.Add(instructionType switch
                {
                    InstructionType.Operation => OperationFactory.Create(instructionSplit),
                    InstructionType.Command => CommandFactory.Create(instructionSplit),
                    InstructionType.Call => CallFactory.Create(instructionSplit),
                    InstructionType.Statement => StatementFactory.Create(instructionSplit),
                    _ => throw new Exception("No such type.")
                });
            }

            return instructionInstances;
        }
    }
}