namespace VMToHackASM.Constants
{
    public static class Paths
    {
        public const string OutputFilePath = OutputPath + OutputFilename + OutputFileExt;
        public const string OutputFilename = "Output";
        public const string InputPath = DataPath + "Input/";
        private const string OutputPath = DataPath + "Output/";
        private const string DataPath = Root + "IO/Data/";
        private const string Root = "./../../../";
        private const string OutputFileExt = ".asm";
    }
}