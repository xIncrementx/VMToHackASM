namespace VMToHackASM.Models
{
    public class VmCommand : IVmCommand
    {
        public VmCommand(VmCommandType commandType) => CommandType = commandType;

        public VmInstruction Instruction { get; } = VmInstruction.Command;
        public VmCommandType CommandType { get; }
    }
}