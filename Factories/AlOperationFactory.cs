using VMToHackASM.Models;
using VMToHackASM.Utilities;

namespace VMToHackASM.Factories
{
    public static class AlOperationFactory
    {
        public static IInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];

            var commandType = EnumUtils.StringToEnum<AlOperationType>(instructionTypeString);

            return new AlOperation(commandType);
        }
    }
}