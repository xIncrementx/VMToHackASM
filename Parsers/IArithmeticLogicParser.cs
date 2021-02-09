using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface IArithmeticLogicParser
    {
        IEnumerable<string> GetLogicalOperation(IAlOperation alOperation);
    }
}