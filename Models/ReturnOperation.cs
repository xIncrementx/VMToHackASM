namespace VMToHackASM.Models
{
    public class ReturnOperation :IInstruction
    {
        public InstructionType InstructionType { get; } = InstructionType.Return;
    }
}