using System.Collections.Generic;

namespace VMToHackASM.IO
{
    public interface IVmFileReader
    {
        IEnumerable<string[]> GetAll(string path);
    }
}