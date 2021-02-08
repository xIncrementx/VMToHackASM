using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public interface ICommandParser
    {
        IEnumerable<string> GetCommands(CommandType commandType);
        public bool StackPointerFocused { get; set; }
    }
}