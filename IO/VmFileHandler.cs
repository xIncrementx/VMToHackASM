using System.Collections.Generic;
using System.IO;

namespace VMToHackASM.IO
{
    public class VmFileHandler
    {
        private readonly IVmFileReader vmFileReader;

        public VmFileHandler(IVmFileReader vmFileReader) => this.vmFileReader = vmFileReader;

        /// <summary>
        /// Gets instructions from all .vm files in specified directory.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<string[]>> GetAll(string directoryPath)
        {
            var allOperationsSplit = new List<IEnumerable<string[]>>();
            var vmFilePaths = Directory.GetFiles(directoryPath, "*.vm");
            SortMainFirst(vmFilePaths);

            foreach (string path in vmFilePaths)
            {
                var operationsSplit = this.vmFileReader.GetAll(path);
                allOperationsSplit.Add(operationsSplit);
            }

            return allOperationsSplit;
        }

        private static void SortMainFirst(IList<string> filePaths)
        {
            for (int i = 0; i < filePaths.Count; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePaths[i]);
                string fileNameLower = fileName.ToLower();

                if (fileNameLower != "main") continue;

                string temp = filePaths[0];
                filePaths[0] = filePaths[i];
                filePaths[i] = temp;
                break;
            }
        }
    }
}