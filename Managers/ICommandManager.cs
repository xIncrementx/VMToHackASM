using System.Collections.Generic;

namespace VMToHackASM.Managers
{
    public interface ICommandManager
    {
        IEnumerable<string> GetCommands(string vmCommand);
        public bool StackPointerFocused { get; set; }
    }
}