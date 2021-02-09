namespace VMToHackASM.Models
{
    public class Label : ILabel
    {
        public Label(LabelType labelType) => Type = labelType;

        public InstructionType InstructionType { get; } = InstructionType.Statement;
        public LabelType Type { get; }
    }
}