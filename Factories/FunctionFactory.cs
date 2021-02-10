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
            
            string functionName = instructionSplit[1];
            short localVars = short.Parse(instructionSplit[2]);

            return new FunctionOperation(functionType, functionName, localVars);
        }
    }
}