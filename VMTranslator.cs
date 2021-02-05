using System.Collections.Generic;
using System;

namespace VMToHackASM
{
    public class VmTranslator
    {
        private readonly Stack<short> stack = new Stack<short>(1791);
        private readonly List<short> heap = new List<short>(14576); // Mem after SCREEN, KBD, STACK and 0-15 allocation

        private short spPtr; 
        private short lclPtr; 
        private short argPtr; 
        private short thisPtr; 
        private short thatPtr;
        private short[] tempPtrs;
        
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
            List<string> sList = Push("constant", 1);

            for (int i = 0; i < sList.Count; i++)
            {
                Console.WriteLine(sList[i]);
            }

            Console.ReadKey();
            
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
        /// Pushes a value from Stackpointer's position, to specified address.
        /// </summary>
        private List<string> Push(string segment, short value)
        {
            // constant, this, that, 
            //push constant 1
            //@1
            //D = A // D = 1
            //@SP
            //A = M // push D onto stack
            //M = D
            //@SP
            //M = M + 1 // “preincrement” SP

            List<string> listOfCommands = new List<string>();

            switch (segment)
            {
                case "constant":
                    listOfCommands.Add("@" + value);
                    listOfCommands.Add("D=A");
                    listOfCommands.Add("@SP");
                    listOfCommands.Add("A=M");
                    listOfCommands.Add("M=D");
                    listOfCommands.Add("@SP");
                    listOfCommands.Add("M=M+1");
                    break;
                default:
                    break;
            }

            this.spPtr++;

            return listOfCommands;
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