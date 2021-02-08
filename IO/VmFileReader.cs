using System.Collections.Generic;
using System.IO;
using VMToHackASM.Exceptions;

namespace VMToHackASM.IO
{
    public class VmFileReader
    {
        private readonly IEnumerable<string> vmInstructions;
        private readonly string path;

        public VmFileReader(string path, IEnumerable<string> vmInstructions)
        {
            this.path = path;
            this.vmInstructions = vmInstructions;
        }

        /// <summary>
        /// Gets a clean and trimmed version of the .asm file.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidVmInstructionException"></exception>
        public IEnumerable<string[]> GetAll()
        {
            var vmOperationsSplit = new List<string[]>();
            var fileReader = new StreamReader(this.path);
            var trimChars = new[] {' ', '\n', '\t'};

            for (string line; (line = fileReader.ReadLine()) != null;)
            {
                string trimmed = line.TrimStart(trimChars);
                bool hasComment = trimmed.Contains("//");
                if (hasComment) trimmed = RemoveComment(trimmed);
                if (trimmed.Length == 0) continue;
                string vmOperation = trimmed.TrimEnd(trimChars);

                bool invalidInstruction = !IsValidInstruction(vmOperation);
                if (invalidInstruction) throw new InvalidVmInstructionException(vmOperation);

                var vmOperationSplit = vmOperation.Split(' ');

                vmOperationsSplit.Add(vmOperationSplit);
            }

            return vmOperationsSplit;
        }

        private bool IsValidInstruction(string text)
        {
            foreach (string s in this.vmInstructions)
            {
                bool validCommand = text.Contains(s);

                if (validCommand)
                {
                    return true;
                }
            }

            return false;
        }

        private static string RemoveComment(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                if (c != '/') continue;

                int removeTrailing = s.Length - i;
                return s.Remove(i, removeTrailing);
            }

            return s;
        }
    }
}