using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class StatementLabelParser : IStatementLabelParser
    {
        private readonly string filename;

        public StatementLabelParser(string filename) => this.filename = filename;

        public IEnumerable<string> GetLabelStatementOperation(ILabel label)
        {
            var asmOperation = new List<string>();
            var statementLabelType = label.Type;

            asmOperation.AddRange(statementLabelType switch
            {
                LabelType.Label => new[] {$"({this.filename}.)"},
                LabelType.Goto => new[] {""},
                LabelType.IfGoto => new[] {""},
                _ => throw new ArgumentException("Label not recognized.", nameof(statementLabelType))
            });

            return asmOperation;
        }
    }
}