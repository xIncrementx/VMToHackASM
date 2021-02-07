namespace VMToHackASM.Models
{
    public class VmOperation : IVmOperation
    {
        public VmOperation(VmOperationType vmOperationType, Segment segment, short value)
        {
            VmOperationType = vmOperationType;
            Segment = segment;
            Value = value;
        }

        public VmInstruction Instruction { get; } = VmInstruction.Operation;
        public VmOperationType VmOperationType  { get; }
        public Segment Segment { get; }
        public short Value { get; }
    }
}