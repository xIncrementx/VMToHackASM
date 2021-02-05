using System;
using System.Collections.Generic;
using System.IO;

namespace VMToHackASM.IO
{
    public class VmFileReader
    {
        private readonly IReadOnlyList<string> commandList;
        private readonly string path;

        public VmFileReader(string path)
        {
            this.path = path;

            this.commandList = new List<string>()
            {
                "push", "pop", "add", "sub", "neg", "eq", "gt", "lt", "and", "or", "not"
            };
        }

        public IEnumerable<string> GetAll()
        {
            var fileReader = new StreamReader(this.path);

            
            for (string line; (line = fileReader.ReadLine()) != null;)
            {
                line.TrimStart();
                bool hasComment = line.Contains("//");
                if (hasComment) line = RemoveComment(line);
                if (line.Length == 0) continue;
                
                line.TrimEnd();
                
                bool invalidCommand = !IsValidCommand(line);
                if (invalidCommand) throw new Exception("This command is not valid...");
                
                yield return line;
            }
        }

        private bool IsValidCommand(string text)
        {
            foreach (string s in this.commandList)
            {
                bool validCommand = text.Contains(s);

                if (validCommand)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes comments and white space from a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string RemoveComment(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                if (c != '/') continue;

                int removeTrailing = s.Length - i;
                return s.Remove(i, removeTrailing).Replace(" ", "");
            }
            
            return s.Replace(" ", string.Empty);
        }
    }
}