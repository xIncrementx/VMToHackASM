namespace VMToHackASM.Models
{
    public interface IVmCommand : IVmInstruction
    {
        public VmCommandType CommandType { get; }
    }
}