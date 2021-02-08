using System;
using System.Collections.Generic;
using System.IO;
using VMToHackASM.Factories;
using VMToHackASM.IO;
using VMToHackASM.Managers;
using VMToHackASM.Parsers;

namespace VMToHackASM
{
    class Program
    {
        private static readonly IReadOnlyList<string> VmInstructions = new List<string>
        {
            "push", "pop", "add", "sub", "neg", "eq", "gt", "lt", "and", "or", "not", "label", "if", "if-goto", "goto",
            "function", "call", "return"
        };
        
        private const string Root = "./../../../";
        private const string DataPath = Root + "Data/";
        private const string InputPath = DataPath + "Input/";
        private const string OutputPath =  DataPath + "Output/";
        private const string InputFile = InputPath + "Input.vm";
        private const string OutputFile = "output.asm";
        private const string OutputFilePath = OutputPath + OutputFile;

        private static void Main(string[] args)
        {
            IOperationParser operationParser = new OperationParser(OutputFile);
            ICommandParser commandParser = new CommandParser(OutputFile);
            var vmParser = new VmParserManager(operationParser, commandParser);
            var fileReader = new VmFileReader(InputFile, VmInstructions);
            
            try
            {
                var vmOperations = fileReader.GetAll();
                var vmInstructionHelpers = VmInstructionHelperFactory.CreateCollection(vmOperations);
                var vmInstructionInstances = VmInstructionFactory.CreateCollection(vmInstructionHelpers);
                var asmOperations = vmParser.ToHackAsm(vmInstructionInstances);
                FileWriter.Write(asmOperations, OutputFilePath);
            }
            catch (Exception e)
            {
                if (e is DirectoryNotFoundException)
                {
                    Console.WriteLine($"Directory error: {e.Message}\n" +
                                      $"Make sure the path is correct and try again.");
                }
                else
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine("End");
        }
    }
}