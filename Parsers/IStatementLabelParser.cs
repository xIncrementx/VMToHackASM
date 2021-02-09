using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface IStatementLabelParser
    {
        IEnumerable<string> GetLabelStatementOperation(ILabel label);
    }
}