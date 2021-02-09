﻿using System;
using System.Collections.Generic;
using VMToHackASM.Models;

namespace VMToHackASM.Parsers
{
    public class FunctionParser : IFunctionParser
    {
        // Maybe ctor?
        
        public IEnumerable<string> GetFunctionOperation(IFunction function)
        {
            var asmOperations = new List<string>();
            var functionType = function.Type;

            asmOperations.AddRange(functionType switch
            {
                FunctionType.Return => new[] {""},
                FunctionType.Call => new[] {""},
                FunctionType.Function => new[] {""},
                _ => throw new Exception("Function type does not exist.")
            });

            return asmOperations;
        }
    }
}