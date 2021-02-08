using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface IOperationParser
    {
        public IEnumerable<string> GetPushOperation(Segment segment, short value);
        public IEnumerable<string> GetPopOperation(Segment segment, short value);
    }
}