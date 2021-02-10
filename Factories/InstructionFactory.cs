using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class InstructionFactory
    {
        public static IEnumerable<IInstruction> CreateCollection(IEnumerable<IInstructionPrototype> instructionPrototypes)
        {
            var instructionInstances = new List<IInstruction>();

            foreach (var instruction in instructionPrototypes)
            {
                var instructionType = instruction.InstructionType;
                var instructionSplit = instruction.Instructions;

                instructionInstances.Add(instructionType switch
                {
                    InstructionType.PushPop => PushPopOperationFactory.Create(instructionSplit),
                    InstructionType.ArithmeticLogic => AlOperationFactory.Create(instructionSplit),
                    InstructionType.Function => FunctionFactory.Create(instructionSplit),
                    InstructionType.Label => LabelFactory.Create(instructionSplit),
                    InstructionType.Return => new ReturnOperation(),
                    _ => throw new ArgumentOutOfRangeException(instructionType.ToString(),"No such type.")
                });
            }

            return instructionInstances;
        }
    }
}