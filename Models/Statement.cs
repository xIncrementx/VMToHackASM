namespace VMToHackASM.Models
{
    public class Statement : IStatement
    {
        public Statement(StatementType statementType) => StatementType = statementType;

        public InstructionType InstructionType { get; } = InstructionType.Statement;
        public StatementType StatementType { get; }
    }
}