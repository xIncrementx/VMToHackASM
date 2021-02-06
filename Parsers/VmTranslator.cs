using System.Collections.Generic;
using VMToHackASM.Data;
using VMToHackASM.Managers;

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
        /// <param name="filename"></param>
        /// <returns></returns>
        public IEnumerable<string> ToHackAsm(IEnumerable<string> vmOperations, string filename)
        {
            var asmOperationManager = new AsmOperationManager(filename);
            var asmOperations = new List<string>(100);

            foreach (string vmOperation in vmOperations)
            {
                var splitVmOperation = vmOperation.Split(' ');

                if (splitVmOperation.Length > 1)
                {
                    string command = splitVmOperation[0];
                    string segment = splitVmOperation[1];
                    short value = short.Parse(splitVmOperation[2]);

                    switch (command)
                    {
                        case "push":
                            var asmOperation = Push(segment, value);
                            asmOperations.AddRange(asmOperation);
                            asmOperations.Add("@SP");
                            asmOperations.Add("M=M+1");
                            asmOperationManager.StackPointerFocused = true;
                            break;
                        case "pop":
                            break;
                    }
                }
                else
                {
                    var asmOperation = asmOperationManager.GetOperation(vmOperation);
                    asmOperations.AddRange(asmOperation);
                    asmOperationManager.StackPointerFocused = false;
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