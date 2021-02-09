namespace VMToHackASM.Models
{
    public class PushPopOperation : IPushPopOperation
    {
        public PushPopOperation(PushPopOperationType type, Segment segment, short value)
        {
            Type = type;
            Segment = segment;
            Value = value;
        }

        public InstructionType InstructionType { get; } = InstructionType.PushPop;
        public PushPopOperationType Type { get; }
        public Segment Segment { get; }
        public short Value { get; }
    }
}