using System.Collections.Generic;
using System.IO;

namespace VMToHackASM.IO
{
    public static class FileWriter
    {
        /// <summary>
        /// Writes a file to the given path. The file extension is determined by the path given.<br/>
        /// Example: C:/Some/Path.ext
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        public static void Write(IEnumerable<string> file, string path)
        {
            using var fileWriter = new StreamWriter(path);

            foreach (string line in file)
            {
                fileWriter.WriteLine(line);
            }
        }
    }
}