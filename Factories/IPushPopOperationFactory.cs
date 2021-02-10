using VMToHackASM.Models;
using VMToHackASM.Utilities;

namespace VMToHackASM.Factories
{
    public static class PushPopOperationFactory
    {
        public static IInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];
            string segmentString = instructionSplit[1];
            short value = short.Parse(instructionSplit[2]);

            var pushPopOperationType = EnumUtils.StringToEnum<PushPopOperationType>(instructionTypeString);
            var segmentType = EnumUtils.StringToEnum<Segment>(segmentString);

            return new PushPopOperation(pushPopOperationType, segmentType, value);
        }
    }
}