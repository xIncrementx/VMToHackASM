namespace VMToHackASM.Parsers
{
    public interface IVmParser
    {
        ICommandParser CommandParser { get; }
        IOperationParser OperationParser { get; }
        ILabelParser LabelParser { get; }
        IFunctionParser FunctionParser { get; }
    }
}