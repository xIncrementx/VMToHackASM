using VMToHackASM.Managers;
using VMToHackASM.Parsers;

namespace VMToHackASM.Factories
{
    public class VmParserFactory : IVmParserFactory
    {
        private readonly string filename;

        public VmParserFactory(string filename) => this.filename = filename;

        public IPushPopParser CreatePushPopParser(IStackPointerListener stackPointerListener)
            => new PushPopParser(this.filename, stackPointerListener);

        public IArithmeticLogicParser CreateArithmeticLogicParser(IStackPointerListener stackPointerListener)
            => new ArithmeticLogicParser(this.filename, stackPointerListener);

        public IFunctionParser CreateFunctionParser(IStackPointerListener stackPointerListener)
            => new FunctionParser(this.filename, stackPointerListener);

        public ILabelParser CreateStatementLabelParser(IStackPointerListener stackPointerListener)
            => new LabelParser(this.filename);
    }
}