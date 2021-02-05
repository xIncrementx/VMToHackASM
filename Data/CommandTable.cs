using System.Collections.Generic;

namespace VMToHackASM.Data
{
    public static class CommandTable
    {
        private static short _counter;
        private static Dictionary<string, string[]> Map { get; } = new Dictionary<string, string[]>();

        static CommandTable()
        {
            // DON'T INCREMENT SP HERE!!! ONLY EXECUTE COMMAND
            // IN OTHER WORDS, DON'T END COMMANDS WITH: @SP, @AM=M+1
            string[] addCommand = {"@SP", "AM=M-1", "D=M", "A=A-1", "M=M+D"};
            string[] subCommand = {"@SP", "AM=M-1", "D=M", "A=A-1", "M=M-D"};
            string[] negCommand = {"@SP", "AM=M-1", "M=-M1"};

            Map.Add("add", addCommand);
            Map.Add("sub", subCommand);
            Map.Add("neg", negCommand);
        }

        public static string[] GetCommand(string command)
        {
            string label1 = GetLabelNumberAsString();
            string label2 = GetLabelNumberAsString();

            switch (command)
            {
                case "eq":
                    return GetEqCommand(label1, label2);
                case "gt":
                    return GetGtCommand(label1, label2);
                case "lt":
                    return GetLtCommand(label1, label2);
                default:
                    Map.TryGetValue(command, out var completeCommand);
                    return completeCommand;
            }
        }

        private static string[] GetEqCommand(string label1, string label2)
        {
            string[] command =
            {
                "@SP", "A=M-1", "A=A-1", "D=M", "A=A+1", "D=D-M",
                "@File." + label1, "D;JEQ",
                "@File." + label2, "0;JMP",
                $"(File.{label1})", "D=-1",
                $"(File.{label2})",
                "@SP", "AM=M-1", "A=A-1", "M=D"
            };

            return command;
        }

        private static string[] GetGtCommand(string label1, string label2)
        {
            string[] command =
            {
                "@SP", "A=M-1", "A=A-1", "D=M", "A=A+1", "D=D-M",
                "@File." + label1, "D;JGT",
                "@File." + label2, "0;JMP",
                $"(File.{label1})", "D=-1",
                $"(File.{label2})",
                "@SP", "AM=M-1", "A=A-1", "M=D"
            };

            return command;
        }

        private static string[] GetLtCommand(string label1, string label2)
        {
            string[] command =
            {
                "@SP", "A=M-1", "A=A-1", "D=M", "A=A+1", "D=D-M",
                "@File." + label1, "D;JLT",
                "@File." + label2, "0;JMP",
                $"(File.{label1})", "D=-1",
                $"(File.{label2})",
                "@SP", "AM=M-1", "A=A-1", "M=D"
            };
            return command;
        }

        private static string GetLabelNumberAsString() => _counter++.ToString();
    }
}