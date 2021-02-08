using System.Collections.Generic;
using System.IO;
using VMToHackASM.Exceptions;

namespace VMToHackASM.IO
{
    public class VmFileReader
    {
        private readonly IEnumerable<string> instructionStrings;
        private readonly string path;

        public VmFileReader(string path, IEnumerable<string> instructionStrings)
        {
            this.path = path;
            this.instructionStrings = instructionStrings;
        }

        /// <summary>
        /// Gets a clean and trimmed version of the .asm file.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidInstructionException"></exception>
        public IEnumerable<string[]> GetAll()
        {
            var operationsSplit = new List<string[]>();
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
                if (invalidInstruction) throw new InvalidInstructionException(vmOperation);

                var vmOperationSplit = vmOperation.Split(' ');

                operationsSplit.Add(vmOperationSplit);
            }

            return operationsSplit;
        }

        private bool IsValidInstruction(string text)
        {
            foreach (string s in this.instructionStrings)
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