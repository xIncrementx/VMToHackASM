namespace VMToHackASM.Models
{
    public class Statement : IInstruction
    {
        public Statement() 
        {
            
        }

        public InstructionType InstructionType { get; } = InstructionType.Statement;
    }
}