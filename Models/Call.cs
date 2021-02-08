namespace VMToHackASM.Models
{
    public class Call : IInstruction
    {
        public Call()
        {
            
        }

        public InstructionType InstructionType { get; } = InstructionType.Call;
    }
}