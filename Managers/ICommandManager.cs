using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Managers
{
    public interface ICommandManager
    {
        IEnumerable<string> GetCommands(VmCommandType commandType);
        public bool StackPointerFocused { get; set; }
    }
}