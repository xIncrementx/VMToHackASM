namespace VMToHackASM.Models
{
    public interface IInstructionPrototype : IInstruction
    {
        string[] Instructions { get; }
    }
}