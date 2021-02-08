namespace VMToHackASM.Models
{
    public class VmStatement : IVmInstruction
    {
        public VmStatement() 
        {
            
        }

        public VmInstructionType InstructionType { get; } = VmInstructionType.Statement;
    }
}