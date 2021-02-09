namespace VMToHackASM.Models
{
    public interface IFunction : IInstruction
    {
        public FunctionType Type { get; }
    }
}