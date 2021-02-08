using VMToHackASM.Models;
using VMToHackASM.Utilities;

namespace VMToHackASM.Factories
{
    public static class CommandFactory
    {
        public static IInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];

            var commandType = EnumUtils.StringToEnum<CommandType>(instructionTypeString);

            return new Command(commandType);
        }
    }
}