namespace VMToHackASM.Models
{
    public class VmInstructionHelper : IVmInstructionHelper
    {
        public VmInstructionHelper(string[] instructionSplit, VmInstructionType vmInstructionType)
        {
            InstructionSplit = instructionSplit;
            InstructionType = vmInstructionType;
        }

        public string[] InstructionSplit { get; }
        public VmInstructionType InstructionType { get; }
    }
}