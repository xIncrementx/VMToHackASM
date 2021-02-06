using System.Collections.Generic;
using VMToHackASM.Managers;

namespace VMToHackASM.Parsers
{
    public class VmParser
    {
        private readonly ISegmentManager segmentManager;
        private readonly ICommandManager commandManager;

        public VmParser(ISegmentManager segmentManager, ICommandManager commandManager)
        {
            this.segmentManager = segmentManager;
            this.commandManager = commandManager;
        }

        /// <summary>
        /// Translates VM to Hack assembly language.
        /// </summary>
        /// <param name="vmCommandsSplit"></param>
        /// <returns></returns>
        public IEnumerable<string> ToHackAsm(IEnumerable<string[]> vmCommandsSplit)
        {
            var asmCommands = new List<string>(100);

            foreach (var vmCommandSplit in vmCommandsSplit)
            {
                if (vmCommandSplit.Length > 1)
                {
                    string segment = vmCommandSplit[1];
                    short value = short.Parse(vmCommandSplit[2]);
                    var asmCommand = this.segmentManager.Push(segment, value);
                    asmCommands.AddRange(asmCommand);
                    this.commandManager.StackPointerFocused = true;
                }
                else
                {
                    string operation = vmCommandSplit[0];
                    var asmCommand = this.commandManager.GetCommands(operation);
                    asmCommands.AddRange(asmCommand);
                    this.commandManager.StackPointerFocused = false;
                }
            }

            return asmCommands;
        }
    }
}