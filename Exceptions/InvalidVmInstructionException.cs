using System;

namespace VMToHackASM.Exceptions
{
    public class InvalidVmInstructionException : Exception
    {
        public InvalidVmInstructionException(string instruction) : base($"{instruction} is not a valid VM instruction.")
        {
            
        }
    }
}