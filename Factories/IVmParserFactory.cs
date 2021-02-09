using VMToHackASM.Parsers;

namespace VMToHackASM.Factories
{
    public interface IVmParserFactory
    {
        IArithmeticLogicParser CreateArithmeticLogicParser(IStackPointerListener stackPointerListener);
        IFunctionParser CreateFunctionParser(IStackPointerListener stackPointerListener);
        IStatementLabelParser CreateStatementLabelParser(IStackPointerListener stackPointerListener);
        IPushPopParser CreatePushPopParser(IStackPointerListener stackPointerListener);
    }
}