using VMToHackASM.Models;
using VMToHackASM.Utilities;

namespace VMToHackASM.Factories
{
    public static class OperationFactory
    {
        public static IInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];
            string segmentString = instructionSplit[1];

            var operationType = EnumUtils.StringToEnum<PushPopOperationType>(instructionTypeString);
            var segmentType = EnumUtils.StringToEnum<Segment>(segmentString);

            short value = short.Parse(instructionSplit[2]);

            return new PushPopOperation(operationType, segmentType, value);
        }
    }
}