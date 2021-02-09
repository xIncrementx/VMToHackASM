namespace VMToHackASM.Models
{
    public interface ILabel : IInstruction
    {
        LabelType LabelType { get; }
    }
}