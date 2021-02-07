using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Managers
{
    public interface IOperationManager
    {
        public IEnumerable<string> Push(Segment segment, short value);
        public IEnumerable<string> Pop(Segment segment, short value);
    }
}