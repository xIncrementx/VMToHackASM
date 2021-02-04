using System.Collections.Generic;

namespace VMToHackASM
{
    public class VmTranslator
    {
        public static string Command { get; set; }

        public static string Segment { get; set; }

        public static string Value { get; set; }

        public static byte LineIndex { get; set; }

        public VmTranslator()
        {
            
        }

        // Command || Segment || Value
        public static void VmToAsm (string commandLine)
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