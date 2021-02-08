using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface ICommandParser
    {
        public bool StackPointerFocused { get; set; }
        IEnumerable<string> GetCommands(CommandType commandType);
    }
}