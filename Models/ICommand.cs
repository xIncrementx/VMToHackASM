namespace VMToHackASM.Models
{
    public interface ICommand : IInstruction
    {
        public CommandType CommandType { get; }
    }
}