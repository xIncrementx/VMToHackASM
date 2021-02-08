using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class CommandFactory
    {
        public static IInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];
            
            var vmCommandType = Utilities.EnumUtils.StringToEnum<CommandType>(instructionTypeString);
   
            return new Command(vmCommandType);
        }
    }
}