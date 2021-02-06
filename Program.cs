using System;
using VMToHackASM.IO;
using VMToHackASM.Managers;
using VMToHackASM.Parsers;
using VMToHackASM.Utilities;

namespace VMToHackASM
{
    class Program
    {
        private const string TestPath = "/Test Files/vmtoasm/StackTest/";
        private const string OutputFileExtension = ".asm";
        private static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private static void Main(string[] args)
        {
            const string outputFilename = "StackTest";
            string vmTestFile = DesktopPath + TestPath + "StackTest.vm";
            string outputFilePath = DesktopPath + TestPath + outputFilename + OutputFileExtension;
            

            ISegmentManager segmentManager = new SegmentManager(256, 300, 400, 3000, 3010);
            ICommandManager commandManager = new CommandManager(outputFilename);
            var vmParser = new VmParser(segmentManager, commandManager);
            var fileReader = new VmFileReader(vmTestFile);

            try
            {
                var vmOperations = fileReader.GetAll();
                var vmOperationsSplit = Text.SplitAll(vmOperations);
                var asmOperations = vmParser.ToHackAsm(vmOperationsSplit);
                FileWriter.Write(asmOperations, outputFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("End");
        }
    }
}