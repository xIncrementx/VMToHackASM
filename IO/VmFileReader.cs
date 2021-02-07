﻿using System;
using System.Collections.Generic;
using System.IO;
using VMToHackASM.Exceptions;

namespace VMToHackASM.IO
{
    public class VmFileReader
    {
        private readonly IReadOnlyList<string> vmOperations;
        private readonly string path;

        public VmFileReader(string path)
        {
            this.path = path;

            this.vmOperations = new List<string>
            {
                "push", "pop", "add", "sub", "neg", "eq", "gt", "lt", "and", "or", "not"
            };
        }

        public IEnumerable<string[]> GetAll()
        {
            var vmFile = new List<string[]>();
            var fileReader = new StreamReader(this.path);

            for (string line; (line = fileReader.ReadLine()) != null;)
            {
                try
                {
                    line.TrimStart();
                    bool hasComment = line.Contains("//");
                    if (hasComment) line = RemoveComment(line);
                    if (line.Length == 0) continue;
                    line.TrimEnd();

                    bool invalidCommand = !IsValidCommand(line);
                    if (invalidCommand) throw new InvalidVmCommandException(line);

                    var lineSplit = line.Split(' ');

                    vmFile.Add(lineSplit);
                }
                catch (InvalidVmCommandException e)
                {
                    Console.WriteLine(e);
                }
            }

            return vmFile;
        }

        private bool IsValidCommand(string text)
        {
            foreach (string s in this.vmOperations)
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
                return s.Remove(i, removeTrailing).Replace(" ", "");
            }

            return s.Replace(" ", string.Empty);
        }
    }
}