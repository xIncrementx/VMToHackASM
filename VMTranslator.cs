using System.Collections.Generic;

namespace VMToHackASM
{
    public class VmTranslator
    {
        private readonly Stack<short> stack = new Stack<short>(1791);
        private readonly List<short> heap = new List<short>(14576); // Mem after SCREEN, KBD, STACK and 0-15 allocation

        private readonly short spPtr; 
        private readonly short lclPtr; 
        private readonly short argPtr; 
        private readonly short thisPtr; 
        private readonly short thatPtr;
        private readonly short[] tempPtrs;
        
        public static string Command { get; set; }

        public static string Segment { get; set; }

        public static string Value { get; set; }

        public static byte LineIndex { get; set; }

        /// <summary>
        /// Parameter order:<br/>
        /// SP, LCL, ARG, THIS, THAT 
        /// </summary>
        /// <param name="stackPtr"></param>
        /// <param name="lclPtr"></param>
        /// <param name="argPtr"></param>
        /// <param name="thisPtr"></param>
        /// <param name="thatPtr"></param>
        /// <param name="tempPtrs"></param>
        public VmTranslator(short stackPtr, short lclPtr, short argPtr, short thisPtr, short thatPtr, params short[] tempPtrs)
        {
            this.spPtr = stackPtr;
            this.lclPtr = lclPtr;
            this.argPtr = argPtr;
            this.thisPtr = thisPtr;
            this.thatPtr = thatPtr;
            this.tempPtrs = tempPtrs;
        }

        // Command || Segment || Value
        public void VmToAsm (string commandLine)
        {
            
            
            
            CombineFullLine(commandLine);
        }

        /// <summary>
        /// Check string chars until white space.
        /// </summary>
        /// <param name="line"></param>
        private static byte SetCommand(string line)
        {
            Command = "";

            LineIndex = 0;

            return 0;
        }

        /// <summary>
        /// Check string chars until white space.
        /// </summary>
        /// <param name="line"></param>
        private static byte SetSegment(string line)
        {
            Segment = "";

            LineIndex = 0;

            return 0;
        }

        /// <summary>
        /// Check string chars until end of line.
        /// </summary>
        /// <param name="line"></param>
        private static byte SetValue(string line)
        {
            Value = "";

            LineIndex = 0;

            return 0;
        }

        /// <summary>
        /// Pushes a value at Stackpointer's position.
        /// </summary>
        private static void Push(string SPpos, string segment, string value)
        {

        }

        /// <summary>
        /// Decrements Stackpointer's position and pops the value to the segment.
        /// </summary>
        private static void Pop(string SPpos, string segment, string value)
        {

        }

        private static void CombineFullLine(string line)
        {
            SetCommand(line);
            SetSegment(line);
            SetValue(line);

            // Use ...
        }
    }
}