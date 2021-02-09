namespace VMToHackASM.Models
{
    public interface IFunction : IInstruction
    {
        public FunctionType FunctionType { get; }
    }
}