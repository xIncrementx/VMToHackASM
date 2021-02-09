using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface IPushPopParser
    {
        public IEnumerable<string> GetPushPopOperation(IPushPopOperation pushPopOperation);
    }
}