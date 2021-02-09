namespace VMToHackASM.Models
{
    public class InstructionPrototype : IInstructionPrototype
    {
        public InstructionPrototype(string[] instructionSplit, InstructionType instructionType)
        {
            Instructions = instructionSplit;
            InstructionType = instructionType;
        }

        public string[] Instructions { get; }
        public InstructionType InstructionType { get; }
    }
}