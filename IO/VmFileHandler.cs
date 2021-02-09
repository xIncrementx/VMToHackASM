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
            var filePaths = Directory.GetFiles(directoryPath);
            SortMainFirst(filePaths);

            foreach (string path in filePaths)
            {
                var operationsSplit =this.vmFileReader.GetAll(path);
                allOperationsSplit.Add(operationsSplit);
            }
            
            return allOperationsSplit;
        }

        private static void SortMainFirst(IList<string> array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(array[i]);
                string fileNameLower = fileName.ToLower();

                if (fileNameLower != "main") continue;
                
                string temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                break;
            }
        }
    }
}