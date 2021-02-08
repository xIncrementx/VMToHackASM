namespace VMToHackASM.Models
{
    public class VmCommand : IVmCommand
    {
        public VmCommand(VmCommandType commandType) => CommandType = commandType;

        public VmInstructionType InstructionType { get; } = VmInstructionType.Command;
        public VmCommandType CommandType { get; }
    }
}