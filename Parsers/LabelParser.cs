using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class LabelParser : ILabelParser
    {
        private readonly string filename;

        public LabelParser(string filename) => this.filename = filename;

        public IEnumerable<string> GetStatements(LabelType labelType)
        {
            var asmOperation = new List<string>();




            asmOperation.AddRange(labelType switch
            {
                LabelType.Label => new[] {$"({this.filename}.)"},
                LabelType.Goto => new[] {""},
                LabelType.IfGoto => new[] {""},
                _ => throw new ArgumentException("Label not recognized.", nameof(labelType))
            });

            return asmOperation;
        }
    }
}