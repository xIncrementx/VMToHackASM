namespace VMToHackASM.Models
{
    public class FunctionOperation : IFunctionOperation
    {
        public FunctionOperation(FunctionType functionType)
        {
            Type = functionType;
        }

        public InstructionType InstructionType { get; } = InstructionType.Function;
        public FunctionType Type { get; }
    }
}