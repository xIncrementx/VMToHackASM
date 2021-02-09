using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface ILabelParser
    {
        IEnumerable<string> GetLabelOperation(ILabelOperation labelOperation);
    }
}