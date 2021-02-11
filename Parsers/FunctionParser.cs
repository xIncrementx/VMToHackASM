using System;
using System.Collections.Generic;
using VMToHackASM.Managers;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class FunctionParser : IVmParser<IFunctionOperation>
    {
        private readonly IStackPointerListener stackPointerListener;
        private readonly string filename;

        public FunctionParser(string filename, IStackPointerListener stackPointerListener)
        {
            this.stackPointerListener = stackPointerListener;
            this.filename = filename;
        }

        private string FunctionName { get; set; }

        public IEnumerable<string> GetAsmOperation(IFunctionOperation functionOperation)
        {
            var functionType = functionOperation.Type;

            if (functionType == FunctionType.Return) ReturnOperation();
            
            FunctionName = functionOperation.Name;
            short localVars = functionOperation.LocalVars;

            return functionType switch
            {
                FunctionType.Call => new[] {$"@{FunctionName}.{localVars}", "0;JMP"},
                FunctionType.Function => new[] {$"({FunctionName})"},
                FunctionType.Return => ReturnOperation(),
                _ => throw new Exception("Function type does not exist.")
            };
        }

        private IEnumerable<string> ReturnOperation()
        {


            return new[] {$"@{FunctionName}", "0;JMP"};


        }
    }
}