using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface ICommandParser
    {
        IEnumerable<string> GetCommands(VmCommandType commandType);
        public bool StackPointerFocused { get; set; }
    }
}