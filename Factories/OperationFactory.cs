using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class OperationFactory
    {
        public static IInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];
            string segmentString = instructionSplit[1];
            
            var instructionType = Utilities.EnumUtils.StringToEnum<OperationType>(instructionTypeString);
            var segmentType = Utilities.EnumUtils.StringToEnum<Segment>(segmentString);

            short value = short.Parse(instructionSplit[2]);

            return new Operation(instructionType, segmentType, value);
        }
    }
}