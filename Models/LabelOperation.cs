namespace VMToHackASM.Models
{
    public class LabelOperation : ILabelOperation
    {
        public LabelOperation(LabelType type, string labelName)
        {
            Type = type;
            LabelName = labelName;
        }

        public InstructionType InstructionType { get; } = InstructionType.Statement;
        public LabelType Type { get; }
        public string LabelName { get; }
    }
}