namespace VMToHackASM.Models
{
    public class Operation : IOperation
    {
        public Operation(OperationType operationType, Segment segment, short value)
        {
            OperationType = operationType;
            Segment = segment;
            Value = value;
        }

        public InstructionType InstructionType { get; } = InstructionType.Operation;
        public OperationType OperationType { get; }
        public Segment Segment { get; }
        public short Value { get; }
    }
}