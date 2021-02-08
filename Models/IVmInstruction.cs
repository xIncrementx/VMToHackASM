namespace VMToHackASM.Models
{
    public interface IVmInstruction
    {
        public VmInstructionType InstructionType { get; }
    }
}