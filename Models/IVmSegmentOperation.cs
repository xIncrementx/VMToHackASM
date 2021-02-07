namespace VMToHackASM.Models
{
    public interface IVmOperation : IVmInstruction
    {
        public VmOperationType VmOperationType { get; }
        public Segment Segment { get; }
        public short Value { get; }
    }
}