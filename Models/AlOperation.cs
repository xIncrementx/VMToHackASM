namespace VMToHackASM.Models
{
    public class AlOperation : IAlOperation
    {
        public AlOperation(AlOperationType alOperationType) => Type = alOperationType;

        public InstructionType InstructionType { get; } = InstructionType.ArithmeticLogic;
        public AlOperationType Type { get; }
    }
}