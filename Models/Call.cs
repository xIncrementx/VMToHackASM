namespace VMToHackASM.Models
{
    public class Call : ICall
    {
        public Call(CallType callType)
        {
            CallType = callType;
        }

        public InstructionType InstructionType { get; } = InstructionType.Call;
        public CallType CallType { get; }
    }
}