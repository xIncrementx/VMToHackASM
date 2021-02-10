using VMToHackASM.Managers;
using VMToHackASM.Models;
using VMToHackASM.Parsers;

namespace VMToHackASM.Factories
{
    public class VmParserFactory : IVmParserFactory
    {
        private readonly string filename;

        public VmParserFactory(string filename) => this.filename = filename;

        public IVmParser<IPushPopOperation> CreatePushPopParser(IStackPointerListener stackPointerListener)
            => new PushPopParser(this.filename, stackPointerListener);

        public IVmParser<IAlOperation>  CreateArithmeticLogicParser(IStackPointerListener stackPointerListener)
            => new ArithmeticLogicParser(this.filename, stackPointerListener);

        public IVmParser<IFunctionOperation>  CreateFunctionParser(IStackPointerListener stackPointerListener)
            => new FunctionParser(this.filename, stackPointerListener);

        public IVmParser<ILabelOperation>  CreateStatementLabelParser(IStackPointerListener stackPointerListener)
            => new LabelParser(this.filename);
    }
}