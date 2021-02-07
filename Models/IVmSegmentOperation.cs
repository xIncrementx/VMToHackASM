namespace VMToHackASM.Models
{
    public interface IVmOperation : IVmInstruction
    {
        public VmOperationType VmOperationType { get; }
        public VmSegment VmSegment { get; }
        public short Value { get; }
    }
}