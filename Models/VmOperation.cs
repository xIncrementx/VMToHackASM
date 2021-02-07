namespace VMToHackASM.Models
{
    public class VmOperation : IVmOperation
    {
        public VmOperation(VmOperationType vmOperationType, VmSegment vmSegment, short value)
        {
            VmOperationType = vmOperationType;
            VmSegment = vmSegment;
            Value = value;
        }

        public VmInstruction Instruction { get; } = VmInstruction.Operation;
        public VmOperationType VmOperationType  { get; }
        public VmSegment VmSegment { get; }
        public short Value { get; }
    }
}