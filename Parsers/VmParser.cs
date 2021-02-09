namespace VMToHackASM.Parsers
{
    public class VmParser: IVmParser
    {
        public VmParser(string filename) 
        {
            CommandParser = new CommandParser(filename);
            OperationParser = new OperationParser(filename);
            LabelParser = new LabelParser(filename);
            FunctionParser = new FunctionParser();
        }

        public ICommandParser CommandParser { get; }
        public IOperationParser OperationParser { get; }
        public ILabelParser LabelParser { get; }
        public IFunctionParser FunctionParser { get; }
    }
}