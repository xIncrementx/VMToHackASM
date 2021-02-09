namespace VMToHackASM.Models
{
    public interface ILabelOperation : IInstruction
    {
        LabelType Type { get; }
        string LabelName { get; }
    }
}