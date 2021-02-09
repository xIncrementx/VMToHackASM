namespace VMToHackASM.Models
{
    public interface ILabel : IInstruction
    {
        LabelType Type { get; }
    }
}