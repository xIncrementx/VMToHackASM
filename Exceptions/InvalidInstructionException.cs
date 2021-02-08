using System;

namespace VMToHackASM.Exceptions
{
    public class InvalidInstructionException : Exception
    {
        public InvalidInstructionException(string instruction) : base($"{instruction} is not a valid instruction.")
        {
            
        }
    }
}