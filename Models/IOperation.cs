namespace VMToHackASM.Models
{
    public interface IOperation : IInstruction
    {
        public OperationType OperationType { get; }
        public Segment Segment { get; }
        public short Value { get; }
    }
}