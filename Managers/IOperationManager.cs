using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Managers
{
    public interface IOperationManager
    {
        public IEnumerable<string> Push(VmSegment vmSegment, short value);
        public IEnumerable<string> Pop(VmSegment vmSegment, short value);
    }
}