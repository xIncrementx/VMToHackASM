namespace VMToHackASM.Models
{
    public class Command : ICommand
    {
        public Command(CommandType commandType) => CommandType = commandType;

        public InstructionType InstructionType { get; } = InstructionType.Command;
        public CommandType CommandType { get; }
    }
}