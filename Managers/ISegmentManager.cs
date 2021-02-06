using System.Collections.Generic;

namespace VMToHackASM.Managers
{
    public interface ISegmentManager
    {
        public IEnumerable<string> Push(string segment, short value);
    }
}