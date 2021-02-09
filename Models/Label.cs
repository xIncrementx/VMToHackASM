namespace VMToHackASM.Models
{
    public class Label : ILabel
    {
        public Label(LabelType labelType) => LabelType = labelType;

        public InstructionType InstructionType { get; } = InstructionType.Statement;
        public LabelType LabelType { get; }
    }
}