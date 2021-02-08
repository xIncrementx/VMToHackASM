namespace VMToHackASM.Constants
{
    public static class Paths
    {
        public const string InputFile = InputPath + "Input.vm";
        public const string OutputFile = "Output.asm";
        public const string OutputFilePath = OutputPath + OutputFile;
        private const string Root = "./../../../";
        private const string DataPath = Root + "IO/Data/";
        private const string InputPath = DataPath + "Input/";
        private const string OutputPath =  DataPath + "Output/";
    }
}