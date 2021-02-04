using System;
using System.Collections.Generic;

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

            try
            {
                var file = fileReader.GetAll();

                PrintAll(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("End");
        }

        private static void PrintAll(IEnumerable<string> file)
        {
            int line = 0;
            foreach (string s in file)
            {
                Console.WriteLine($"{line++} {s}");
            }
        }
    }
}