using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface IOperationParser
    {
        public IEnumerable<string> GetPushOperation(VmSegment vmSegment, short value);
        public IEnumerable<string> GetPopOperation(VmSegment vmSegment, short value);
    }
}