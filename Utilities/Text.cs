using System.Collections.Generic;

namespace VMToHackASM.Utilities
{
    public static class Text
    {
        /// <summary>
        /// Splits each string in a string collection and returns a jagged array containing the
        /// split strings. The sequence remains the same. 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string[]> SplitAll(IReadOnlyList<string> stringCollection)
        {
            int size = stringCollection.Count;
            var splitStrings = new string[size][];

            for (int i = 0; i < size; i++)
            {
                splitStrings[i] = stringCollection[i].Split();
            }

            return splitStrings;
        }
    }
}