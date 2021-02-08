using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class VmCommandFactory
    {
        public static IVmInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];
            
            var vmCommandType = Utilities.EnumUtils.StringToEnum<VmCommandType>(instructionTypeString);
   
            return new VmCommand(vmCommandType);
        }
    }
}