using System.Collections.Generic;

namespace VMToHackASM.Constants
{
    public static class VmInstructions
    {
        public const string Push = "push";
        public const string Pop = "pop";
        public const string Add = "add";
        public const string Sub = "sub";
        public const string Neg = "neg";
        public const string Eq = "eq";
        public const string Gt = "gt";
        public const string Lt = "lt";
        public const string And = "and";
        public const string Or = "or";
        public const string Not = "not";
        public const string Label = "label";
        public const string If = "if";
        public const string IfGoto = "if-goto";
        public const string Goto = "goto";
        public const string Function = "function";
        public const string Call = "call";
        public const string Return = "return";

        public static readonly IEnumerable<string> VmInstructionStrings;

        static VmInstructions()
        {
            VmInstructionStrings = new[]
                {Push, Pop, Add, Sub, Neg, Eq, Gt, Lt, And, Or, Not, Label, If, IfGoto, Goto, Function, Call, Return};
        }
    }
}