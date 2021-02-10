namespace VMToHackASM.Utilities
{
    public static class StringUtils
    {
        public static bool TryGetNonLetters(string s, out string nonLetters)
        {
            nonLetters = "";
            
            foreach (char c in s)
            {
                bool isLetter = char.IsLetter(c);

                if (isLetter) continue;

                nonLetters += c;
            }

            return nonLetters.Length > 0;
        }
    }
}