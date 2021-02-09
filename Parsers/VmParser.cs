namespace VMToHackASM.Parsers
{
    public class VmParser: IVmParser
    {
        public VmParser(string filename) 
        {
            CommandParser = new CommandParser(filename);
            OperationParser = new OperationParser(filename);
            StatementParser = new StatementParser(filename);
            CallParser = new CallParser();
        }

        public ICommandParser CommandParser { get; }
        public IOperationParser OperationParser { get; }
        public IStatementParser StatementParser { get; }
        public ICallParser CallParser { get; }
    }
}