using System.Collections.Generic;

namespace VMToHackASM.Parsers
{
    public interface IVmParser<in T>
    {
        IEnumerable<string> GetAsmOperation(T operation);
    }
}