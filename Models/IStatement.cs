namespace VMToHackASM.Models
{
    public interface IStatement : IInstruction
    {
        StatementType StatementType { get; }
    }
}