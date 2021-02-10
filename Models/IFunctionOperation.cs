namespace VMToHackASM.Models
{
    public interface IFunctionOperation : IInstruction
    {
        public FunctionType Type { get; }
    }
}