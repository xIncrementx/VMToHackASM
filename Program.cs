using System;
using System.IO;
using VMToHackASM.Constants;
using VMToHackASM.Factories;
using VMToHackASM.IO;
using VMToHackASM.Managers;

namespace VMToHackASM
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // TODO: Create a class that handles multiple vm files from the input folder
            // Sys.init is a function that exists outside of this program, it's one of the initial functions called
            // The first instructions to be called are the following (in the exact order presented):
            // SP = 256 (stack i hosts RAM) 
            // call Sys.init
            // 
            /*  How VM functions work:
             
                The function name is an arbitrary string composed of any sequence of letters, 
                digits, underscore (_), dot (.), and colon (:) that does not begin with a digit.
                (We expect that a method bar in class Foo in some high-level language will be translated by the
                compiler to a VM function named Foo.bar). The scope of the function name is global:
                All functions in all files are seen by each other and may call each other using the function name 
                By convention, main.vm is the first file to be read
            */
            
            var vmInstructionStrings = VmInstructions.VmInstructionStrings;
            var vmFileReader = new VmFileReader(vmInstructionStrings);
            var vmFileManager = new VmFileHandler(vmFileReader);

            var vmParserFactory = new VmParserFactory(Paths.OutputFilename);
            var vmParserManager = new VmParserManager(vmParserFactory);

            try
            {
                var allVmInstructions = vmFileManager.GetAll(Paths.InputPath);
                var vmInstructionHelpers = InstructionHelperFactory.CreateCollection(allVmInstructions);
                var vmInstructionInstances = InstructionFactory.CreateCollection(vmInstructionHelpers);
                var asmOperations = vmParserManager.ToHackAsm(vmInstructionInstances);
                FileWriter.Write(asmOperations, Paths.OutputFilePath);
            }
            catch (Exception e)
            {
                if (e is DirectoryNotFoundException)
                    Console.WriteLine($"Directory error: {e.Message}\n" +
                                      "Make sure the path is correct and try again.");
                else
                    Console.WriteLine(e);
            }

            Console.WriteLine("End");
        }
    }
}