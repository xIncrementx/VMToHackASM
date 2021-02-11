namespace VMToHackASM.Models
{
    public class ReturnOperation : IReturnOperation
    {
        public InstructionType InstructionType { get; } = InstructionType.Return;
    }
}