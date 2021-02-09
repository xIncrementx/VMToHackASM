using System;
using System.Collections.Generic;
using VMToHackASM.Factories;
using VMToHackASM.Models;
using VMToHackASM.Parsers;

namespace VMToHackASM.Managers
{
    public class VmParserManager : IStackPointerListener
    {
        private readonly IArithmeticLogicParser arithmeticLogicParser;
        private readonly IPushPopParser pushPopParser;
        private readonly IStatementLabelParser statementLabelParser;
        private readonly IFunctionParser functionParser;

        public VmParserManager(IVmParserFactory vmParserFactory)
        {
            this.arithmeticLogicParser = vmParserFactory.CreateArithmeticLogicParser(this);
            this.pushPopParser = vmParserFactory.CreatePushPopParser(this);
            this.statementLabelParser = vmParserFactory.CreateStatementLabelParser(this);
            this.functionParser = vmParserFactory.CreateFunctionParser(this);
        }

        public bool StackPointerFocused { get; set; }

        public IEnumerable<string> ToHackAsm(IEnumerable<IInstruction> instructions)
        {
            var asmOperations = new List<string>(100);

            foreach (var instruction in instructions)
            {
                var instructionType = instruction.InstructionType;

                asmOperations.AddRange(instructionType switch
                {
                    InstructionType.PushPop => this.pushPopParser.GetPushPopOperation((IPushPopOperation) instruction),
                    InstructionType.ArithmeticLogic =>
                        this.arithmeticLogicParser.GetLogicalOperation((IAlOperation) instruction),
                    InstructionType.Function => this.functionParser.GetFunctionOperation((IFunction) instruction),
                    InstructionType.Statement =>
                        this.statementLabelParser.GetLabelStatementOperation((ILabel) instruction),
                    _ => throw new ArgumentOutOfRangeException(nameof(instructionType), "Invalid instruction type.")
                });
            }

            return asmOperations;
        }
    }
}