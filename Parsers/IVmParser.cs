namespace VMToHackASM.Parsers
{
    public interface IVmParser
    {
        ICommandParser CommandParser { get; }
        IOperationParser OperationParser { get; }
        IStatementParser StatementParser { get; }
        ICallParser CallParser { get; }
    }
}