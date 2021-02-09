using VMToHackASM.Managers;
using VMToHackASM.Parsers;

namespace VMToHackASM.Factories
{
    public interface IVmParserFactory
    {
        IArithmeticLogicParser CreateArithmeticLogicParser(IStackPointerListener stackPointerListener);
        IFunctionParser CreateFunctionParser(IStackPointerListener stackPointerListener);
        ILabelParser CreateStatementLabelParser(IStackPointerListener stackPointerListener);
        IPushPopParser CreatePushPopParser(IStackPointerListener stackPointerListener);
    }
}