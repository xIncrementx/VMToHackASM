namespace VMToHackASM.Models
{
    public class Function : IFunction
    {
        public Function(FunctionType functionType)
        {
            Type = functionType;
        }

        public InstructionType InstructionType { get; } = InstructionType.Function;
        public FunctionType Type { get; }
    }
}