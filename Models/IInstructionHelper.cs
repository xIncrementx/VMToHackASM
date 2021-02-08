namespace VMToHackASM.Models
{
    public interface IInstructionHelper : IInstruction
    {
        string[] InstructionSplit { get; }
    }
}