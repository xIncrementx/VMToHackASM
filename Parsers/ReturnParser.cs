using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class ReturnParser : IVmParser<IReturnOperation>
    {
        public IEnumerable<string> GetAsmOperation(IReturnOperation operation)
        {
            return new[] {"RETURN GOES HERE..."};
        }
    }
}