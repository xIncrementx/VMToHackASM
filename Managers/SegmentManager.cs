using System.Collections.Generic;

namespace VMToHackASM.Managers
{
    public class SegmentManager : ISegmentManager
    {
        private const short StaticPtr = 16;

        private short spPtr;
        private short lclPtr;
        private short argPtr;
        private short thisPtr;
        private short thatPtr;
        private short[] tempPtrs;
        
        /// <summary>
        /// Memory pointers:<br/>
        /// SP (stack), LCL (local), ARG (argument), THIS (objects), THAT (arrays) 
        /// </summary>
        public SegmentManager(short spPtr, short lclPtr, short argPtr, short thisPtr, short thatPtr, params short[] tempPtrs)
        {
            this.spPtr = spPtr;
            this.lclPtr = lclPtr;
            this.argPtr = argPtr;
            this.thisPtr = thisPtr;
            this.thatPtr = thatPtr;
            this.tempPtrs = tempPtrs;
        }

        /// <summary>
        /// Pushes a value from the stack pointer's position to the specified address.
        /// </summary>
        public IEnumerable<string> Push(string segment, short value)
        {
           
            
            var commands = new List<string>();

            switch (segment)
            {
                case "constant":
                    commands.Add("@" + value);
                    commands.Add("D=A");
                    commands.Add("@SP");
                    commands.Add("A=M");
                    commands.Add("M=D");
                    break;
                case "local":
                    commands.Add("@LCL");
                    commands.Add("D=M");
                    commands.Add("@" + value);
                    commands.Add("D=D+A");
                    commands.Add("@SP");
                    commands.Add("A=M");
                    commands.Add("M=D");
                    break;
                case "static":
                    commands.Add("@" + StaticPtr + value);
                    commands.Add("D=M");
                    commands.Add("@" + value);
                    commands.Add("D=D+A");
                    commands.Add("@SP");
                    commands.Add("A=M");
                    commands.Add("M=D");
                    break;
                case "arg":
                    break;
                case "this":
                    break;
                case "that":
                    break;
            }
            
            commands.Add("@SP");
            commands.Add("M=M+1");

            return commands;
        }

        private IEnumerable<string> GetSegment()
        {
            return null;
        }
    }
}