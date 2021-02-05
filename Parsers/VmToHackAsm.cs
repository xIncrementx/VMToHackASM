using System.Collections.Generic;
using VMToHackASM.Data;

namespace VMToHackASM.Parsers
{
    public class VmToHackAsm
    {
        private const short StaticPtr = 16;
        
        private readonly Stack<short> stack = new Stack<short>(1791);
        private readonly List<short> heap = new List<short>(14335); // Mem after SCREEN, KBD, STACK and 0-15 allocation

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
        public VmToHackAsm(short stackPtr, short lclPtr, short argPtr, short thisPtr, short thatPtr,
            params short[] tempPtrs)
        {
            this.spPtr = stackPtr;
            this.lclPtr = lclPtr;
            this.argPtr = argPtr;
            this.thisPtr = thisPtr;
            this.thatPtr = thatPtr;
            this.tempPtrs = tempPtrs;
        }

        public IEnumerable<string> VmToAsm(string line)
        {
            var commands = line.Split(' ');
            var list = new List<string>();

            if (commands.Length > 1)
            {
                string command = commands[0];
                string segment = commands[1];
                short value = short.Parse(commands[2]);

                switch (command)
                {
                    case "push":
                        list.AddRange(Push(segment, value));
                        break;
                    case "pop":
                        break;
                }
            }
            else
            {
                var command = CommandTable.GetCommand(line);
                list.AddRange(command);
            }

            return list;
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
        private IEnumerable<string> Push(string segment, short value)
        {
            var listOfCommands = new List<string>();

            switch (segment)
            {
                case "constant":
                    listOfCommands.Add("@" + value);
                    listOfCommands.Add("D=A");
                    listOfCommands.Add("@SP");
                    listOfCommands.Add("A=M");
                    listOfCommands.Add("M=D");
                    listOfCommands.Add("@SP");
                    break;
                case "local":
                    listOfCommands.Add("@LCL");
                    listOfCommands.Add("D=M");
                    listOfCommands.Add("@" + value);
                    listOfCommands.Add("D=D+A");
                    listOfCommands.Add("@SP");
                    listOfCommands.Add("A=M");
                    listOfCommands.Add("M=D");
                    break;
                case "static":
                    listOfCommands.Add("@" + StaticPtr + value);
                    listOfCommands.Add("D=M");
                    listOfCommands.Add("@" + value);
                    listOfCommands.Add("D=D+A");
                    listOfCommands.Add("@SP");
                    listOfCommands.Add("A=M");
                    listOfCommands.Add("M=D");
                    break;
                case "arg":
                    break;
                case "this":
                    break;
                case "that":
                    break;
                //case "temp":
                //break;
                default:
                    break;
            }

            listOfCommands.Add("M=M+1");

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