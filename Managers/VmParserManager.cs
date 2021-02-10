using System;
using System.Collections.Generic;
using VMToHackASM.Factories;
using VMToHackASM.Models;
using VMToHackASM.Parsers;

namespace VMToHackASM.Managers
{
    public class VmParserManager : IStackPointerListener
    {
        private readonly IVmParser<IAlOperation> alParser;
        private readonly IVmParser<IPushPopOperation> pushPopParser;
        private readonly IVmParser<ILabelOperation> labelParser;
        private readonly IVmParser<IFunctionOperation> funcParser;

        public VmParserManager(IVmParserFactory vmParserFactory)
        {
            this.alParser = vmParserFactory.CreateArithmeticLogicParser(this);
            this.pushPopParser = vmParserFactory.CreatePushPopParser(this);
            this.labelParser = vmParserFactory.CreateStatementLabelParser(this);
            this.funcParser = vmParserFactory.CreateFunctionParser(this);
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
                    InstructionType.PushPop => this.pushPopParser.GetAsmOperation((IPushPopOperation) instruction),
                    InstructionType.ArithmeticLogic => this.alParser.GetAsmOperation((IAlOperation) instruction),
                    InstructionType.Function => this.funcParser.GetAsmOperation((IFunctionOperation) instruction),
                    InstructionType.Label => this.labelParser.GetAsmOperation((ILabelOperation) instruction),
                    _ => throw new ArgumentOutOfRangeException(nameof(instructionType), "Invalid instruction type.")
                });
            }

            return asmOperations;
        }
    }
}