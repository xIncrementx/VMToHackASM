namespace VMToHackASM.Models
{
    public interface ICall : IInstruction
    {
        public CallType CallType { get; }
    }
}