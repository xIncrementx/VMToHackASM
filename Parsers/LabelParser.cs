using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class LabelParser : IVmParser<ILabelOperation>
    {
        private readonly string filename;

        public LabelParser(string filename) => this.filename = filename;

        public IEnumerable<string> GetAsmOperation(ILabelOperation labelOperation)
        {
            var asmOperation = new List<string>();
            var labelType = labelOperation.Type;
            string labelName = labelOperation.LabelName;

            asmOperation.AddRange(labelType switch
            {
                LabelType.Label => new[] {$"({labelName})"},
                LabelType.Goto => new[] {""},
                LabelType.IfGoto => new[] {""},
                _ => throw new ArgumentOutOfRangeException(nameof(labelType), "Label not recognized.")
            });

            return asmOperation;
        }
    }
}