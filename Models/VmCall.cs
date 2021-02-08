namespace VMToHackASM.Models
{
    public class VmCall : IVmInstruction
    {
        public VmCall()
        {
            
        }

        public VmInstructionType InstructionType { get; } = VmInstructionType.Call;
    }
}