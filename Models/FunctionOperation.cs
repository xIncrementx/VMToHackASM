namespace VMToHackASM.Models
{
    public class FunctionOperation : IFunctionOperation
    {
        public FunctionOperation(FunctionType functionType, string functionName, short localVars)
        {
            Type = functionType;
            Name = functionName;
            LocalVars = localVars;
        }

        public InstructionType InstructionType { get; } = InstructionType.Function;
        public FunctionType Type { get; }
        public string Name { get; }
        public short LocalVars { get; }
    }
}