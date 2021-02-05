using System.Collections.Generic;
using VMToHackASM.Data;

namespace VMToHackASM.Parsers
{
    public class VmTranslator
    {
        private const short StaticPtr = 16;

        public bool Pushed { get; private set; }

        private short spPtr;
        private short lclPtr;
        private short argPtr;
        private short thisPtr;
        private short thatPtr;
        private short[] tempPtrs;

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
        public VmTranslator(short stackPtr, short lclPtr, short argPtr, short thisPtr, short thatPtr,
            params short[] tempPtrs)
        {
            this.spPtr = stackPtr;
            this.lclPtr = lclPtr;
            this.argPtr = argPtr;
            this.thisPtr = thisPtr;
            this.thatPtr = thatPtr;
            this.tempPtrs = tempPtrs;
        }

        /// <summary>
        /// Translates VM to Hack assembly language.
        /// </summary>
        /// <param name="vmOperations"></param>
        /// <returns></returns>
        public IEnumerable<string> ToHackAsm(IEnumerable<string> vmOperations)
        {
            var asmOperationManager = new AsmOperationManager();
            var asmOperations = new List<string>(100);

            foreach (string operation in vmOperations)
            {
                var splitOperation = operation.Split(' ');

                if (splitOperation.Length > 1)
                {
                    string command = splitOperation[0];
                    string segment = splitOperation[1];
                    short value = short.Parse(splitOperation[2]);

                    switch (command)
                    {
                        case "push":
                            asmOperations.AddRange(Push(segment, value));
                            asmOperations.Add("@SP");
                            asmOperations.Add("M=M+1");
                            this.Pushed = true;
                            break;
                        case "pop":
                            break;
                    }
                }
                else
                {
                    var operations = asmOperationManager.GetOperation(operation);
                    asmOperations.AddRange(operations);
                    this.Pushed = false;
                }
            }
            
            return asmOperations;
        }

        /// <summary>
        /// Pushes a value from the stack pointer's position, to specified address.
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

            return listOfCommands;
        }

        /// <summary>
        /// Decrements The stack pointer's position and pops the value to the segment.
        /// </summary>
        private static void Pop(string SpPos, string segment, string value)
        {
        }
    }
}