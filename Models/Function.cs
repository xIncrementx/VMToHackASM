namespace VMToHackASM.Models
{
    public class Function : IFunction
    {
        public Function(FunctionType functionType)
        {
            FunctionType = functionType;
        }

        public InstructionType InstructionType { get; } = InstructionType.Function;
        public FunctionType FunctionType { get; }
    }
}