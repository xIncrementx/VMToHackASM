namespace VMToHackASM.Models
{
    public interface IInstruction
    {
        public InstructionType InstructionType { get; }
    }
}