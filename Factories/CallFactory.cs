﻿using VMToHackASM.Models;

namespace VMToHackASM.Factories
{
    public static class CallFactory
    {
        public static IInstruction Create(string[] instructionSplit)
        {
            string instructionTypeString = instructionSplit[0];
            
           // bool enumNotFound = !Enum.TryParse<VmCommandType>(instructionTypeString, out var vmCommandType);
           // if (enumNotFound)   throw new InvalidEnumArgumentException($"Enumerated type '{instructionTypeString}' does not exist.");
   
            return new Call();
        }
    }
}