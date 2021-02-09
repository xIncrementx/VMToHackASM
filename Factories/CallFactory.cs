using VMToHackASM.Models;
using VMToHackASM.Utilities;

namespace VMToHackASM.Factories
{
    public static class CallFactory
    {
        public static IInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];

            var callType = EnumUtils.StringToEnum<FunctionType>(instructionTypeString);

            return new Function(callType);
        }
    }
}