using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface IStatementParser
    {
        IEnumerable<string> GetStatements(LabelType labelType);
    }
}