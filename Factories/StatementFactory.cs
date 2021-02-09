using VMToHackASM.Models;
using VMToHackASM.Utilities;

namespace VMToHackASM.Factories
{
    public static class StatementFactory
    {
        public static IInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];
            var statementType = EnumUtils.StringToEnum<LabelType>(instructionTypeString);
            string labelName = instructionSplit[1];
            
            return new LabelOperation(statementType, labelName);
        }
    }
}