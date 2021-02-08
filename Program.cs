using System;
using VMToHackASM.Factories;
using VMToHackASM.IO;
using VMToHackASM.Managers;
using VMToHackASM.Parsers;

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
            
            IOperationParser operationParser = new OperationParser(outputFilename);
            ICommandParser commandParser = new CommandParser(outputFilename);
            var vmParser = new VmParserManager(operationParser, commandParser);
            var fileReader = new VmFileReader(vmTestFile);

            try
            {
                var vmOperations = fileReader.GetAll();
                var vmInstructionInstances = VmInstructionFactory.CreateCollection(vmOperations);
                var asmOperations = vmParser.ToHackAsm(vmInstructionInstances);
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