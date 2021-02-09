namespace VMToHackASM.Models
{
    public interface IAlOperation : IInstruction
    {
        public AlOperationType Type { get; }
    }
}