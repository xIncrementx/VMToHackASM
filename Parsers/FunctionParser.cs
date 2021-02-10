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

        public IEnumerable<string> GetAsmOperation(IFunctionOperation functionOperation)
        {
            var asmOperations = new List<string>();
            var functionType = functionOperation.Type;
            string functionName = functionOperation.Name;
            short localVars = functionOperation.LocalVars;

            asmOperations.AddRange(functionType switch
            {
                FunctionType.Call => new[] {$"@{functionName}", "0;JMP"},
                FunctionType.Function => new[] {$"({functionName})"},
                _ => throw new Exception("Function type does not exist.")
            });

            return asmOperations;
        }
    }
}