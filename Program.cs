using System;
using System.Collections.Generic;
using System.IO;
using VMToHackASM.Constants;
using VMToHackASM.Factories;
using VMToHackASM.IO;
using VMToHackASM.Managers;
using VMToHackASM.Parsers;

namespace VMToHackASM
{
    class Program
    {
        private static void Main(string[] args)
        {
            var vmInstructionStrings = VmInstructions.VmInstructionStrings;
            var fileReader = new VmFileReader(Paths.InputFile, vmInstructionStrings);
            
            IOperationParser operationParser = new OperationParser(Paths.OutputFile);
            ICommandParser commandParser = new CommandParser(Paths.OutputFile);
            var vmParser = new VmParserManager(operationParser, commandParser);
            
            try
            {
                var vmOperationStrings = fileReader.GetAll();
                var vmInstructionHelpers = VmInstructionHelperFactory.CreateCollection(vmOperationStrings);
                var vmInstructionInstances = VmInstructionFactory.CreateCollection(vmInstructionHelpers);
                var asmOperations = vmParser.ToHackAsm(vmInstructionInstances);
                FileWriter.Write(asmOperations, Paths.OutputFilePath);
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