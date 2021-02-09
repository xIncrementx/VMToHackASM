namespace VMToHackASM.Models
{
    public interface IPushPopOperation : IInstruction
    {
        public PushPopOperationType Type { get; }
        public Segment Segment { get; }
        public short Value { get; }
    }
}