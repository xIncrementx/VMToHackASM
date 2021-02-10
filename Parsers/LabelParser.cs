﻿using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class LabelParser : IVmParser<ILabelOperation>
    {
        public IEnumerable<string> GetAsmOperation(ILabelOperation labelOperation)
        {
            var asmOperation = new List<string>();
            var labelType = labelOperation.Type;
            string labelName = labelOperation.LabelName;

            asmOperation.AddRange(labelType switch
            {
                LabelType.Label => new[] {$"({labelName})"},
                LabelType.Goto => new[] {$"@{labelName}", "0;JMP"},
                LabelType.IfGoto => new[] {"AM=M-1","D=M", $"@{labelName}", "D;JGT"},
                _ => throw new ArgumentOutOfRangeException(nameof(labelType), "Label not recognized.")
            });

            return asmOperation;
        }
    }
}