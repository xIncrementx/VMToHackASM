namespace VMToHackASM.Models
{
    public class InstructionHelper : IInstructionHelper
    {
        public InstructionHelper(string[] instructionSplit, InstructionType instructionType)
        {
            InstructionSplit = instructionSplit;
            InstructionType = instructionType;
        }

        public string[] InstructionSplit { get; }
        public InstructionType InstructionType { get; }
    }
}