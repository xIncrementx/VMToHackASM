using System;
using System.Collections.Generic;
using VMToHackASM.IO;
using VMToHackASM.Parsers;

namespace VMToHackASM
{
    class Program
    {
        private const string SimpleAddPath = "./../../../Tests/SimpleAdd/";
        private const string StackTestPath = "./../../../Tests/StackTest/";

        private static void Main(string[] args)
        {
            const string simpleAddFile = "SimpleAdd.vm";
            const string stackTestFile = "StackTest.vm";

            var fileReader = new VmFileReader(StackTestPath + stackTestFile);
            var translator = new VmToHackAsm(256, 1, 2, 3, 4, 5);
            var list = new List<string>();

            try
            {
                var file = fileReader.GetAll();

                foreach (string item in file)
                {
                   list.AddRange(translator.VmToAsm(item));
                }
                
                PrintAll(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("End");
        }

        private static void PrintAll(IEnumerable<string> file)
        {
            foreach (string s in file)
            {
                Console.WriteLine($"{s}");
            }
        }
        
        private static void PrintAllWithLine(IEnumerable<string> file)
        {
            int line = 0;
            foreach (string s in file)
            {
                Console.WriteLine($"{line++} {s}");
            }
        }
    }
}