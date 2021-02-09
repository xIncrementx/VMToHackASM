using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class StatementParser : IStatementParser
    {
        private readonly string filename;

        public StatementParser(string filename) => this.filename = filename;

        public IEnumerable<string> GetStatements(LabelType labelType)
        {
            var asmOperation = new List<string>();




            asmOperation.AddRange(labelType switch
            {
                LabelType.Label => new[] {$"({this.filename}.)"},
                LabelType.Goto => new[] {""},
                LabelType.IfGoto => new[] {""},
                _ => throw new ArgumentException("Statement not recognized.", nameof(labelType))
            });

            return asmOperation;
        }
    }
}