using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface IFunctionParser
    {
        IEnumerable<string> GetFunctionOperation(IFunctionOperation functionOperationType);
    }
}