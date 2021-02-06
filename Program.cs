using System;
using System.IO;
using VMToHackASM.IO;
using VMToHackASM.Parsers;

namespace VMToHackASM
{
    class Program
    {
        private const string TestPath = "/Test Files/vmtoasm/StackTest/";
        private static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private static void Main(string[] args)
        {
            const string outputFilename = "StackTest";
            const string outputFileExtension = ".asm";
            string vmTestFile = DesktopPath + TestPath + "StackTest.vm";

            var fileReader = new VmFileReader(vmTestFile);
            var vmTranslator = new VmTranslator(256,1,2,3,4);

            try
            {
                var vmOperations = fileReader.GetAll();
                var asmOperations = vmTranslator.ToHackAsm(vmOperations, outputFilename);

                using var fileWriter = new StreamWriter(DesktopPath + TestPath + outputFilename + outputFileExtension);
                foreach (string operation in asmOperations)
                {
                    fileWriter.WriteLine(operation);
                    Console.WriteLine(operation);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("End");
        }
    }
}