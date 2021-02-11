using VMToHackASM.Managers;
using VMToHackASM.Models;
using VMToHackASM.Parsers;

namespace VMToHackASM.Factories
{
    public interface IVmParserFactory
    {
        IVmParser<IAlOperation>  CreateArithmeticLogicParser(IStackPointerListener stackPointerListener);
        IVmParser<IFunctionOperation>  CreateFunctionParser(IStackPointerListener stackPointerListener);
        IVmParser<ILabelOperation>  CreateLabelParser(IStackPointerListener stackPointerListener);
        IVmParser<IPushPopOperation> CreatePushPopParser(IStackPointerListener stackPointerListener);
        IVmParser<IReturnOperation> CreateReturnParser();
    }
}