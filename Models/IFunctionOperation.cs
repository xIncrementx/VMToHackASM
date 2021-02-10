namespace VMToHackASM.Models
{
    public interface IFunctionOperation : IInstruction
    {
        public string Name { get; }
        public short LocalVars { get; }
        public FunctionType Type { get;}
    }
}