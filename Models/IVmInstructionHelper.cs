namespace VMToHackASM.Models
{
    public interface IVmInstructionHelper : IVmInstruction
    {
        string[] InstructionSplit { get; }
    }
}