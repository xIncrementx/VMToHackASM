using VMToHackASM.Models;
using VMToHackASM.Utilities;

namespace VMToHackASM.Factories
{
    public static class FunctionFactory
    {
        public static IInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];

            var functionType = EnumUtils.StringToEnum<FunctionType>(instructionTypeString);

            return new Function(functionType);
        }
    }
}