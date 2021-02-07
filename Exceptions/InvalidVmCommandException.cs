using System;

namespace VMToHackASM.Exceptions
{
    public class InvalidVmCommandException : Exception
    {
        public InvalidVmCommandException(string command) : base($"{command} is not a valid command.")
        {
            
        }
    }
}