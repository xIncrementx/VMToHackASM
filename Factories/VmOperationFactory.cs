using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class VmOperationFactory
    {
        public static IVmInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];
            string segmentString = instructionSplit[1];
            
            var instructionType = Utilities.EnumUtils.StringToEnum<VmOperationType>(instructionTypeString);
            var segmentType = Utilities.EnumUtils.StringToEnum<VmSegment>(segmentString);

            short value = short.Parse(instructionSplit[2]);

            return new VmOperation(instructionType, segmentType, value);
        }
    }
}