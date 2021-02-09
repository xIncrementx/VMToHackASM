namespace VMToHackASM.Parsers
{
    public interface IStackPointerListener
    {
        bool StackPointerFocused { get; set; }
    }
}