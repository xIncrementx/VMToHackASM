using System;

namespace VMToHackASM.Utilities
{
    public static class EnumUtils
    {
        /// <summary>
        ///     Gets an enumerated type matching the string provided.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If no matching enumerated type is found. </exception>
        public static T StringToEnum<T>(string s)
        {
            bool genericNotAnEnum = !typeof(T).IsEnum;
            if (genericNotAnEnum) throw ArgumentException(s);

            bool containsNonLetters = StringUtils.TryGetNonLetters(s, out string nonLetters);
            if (containsNonLetters) s = s.Replace(nonLetters, "");

            var enumValues = (T[]) Enum.GetValues(typeof(T));

            foreach (var enumValue in enumValues)
            {
                string enumValueString = enumValue.ToString();
                if (enumValueString == null) throw new ArgumentNullException($"Type {s} is null.");

                string enumValueStringLowerCase = enumValueString.ToLower();
                if (enumValueStringLowerCase.Contains(s)) return enumValue;
            }

            throw ArgumentException(s);
        }

        private static ArgumentException ArgumentException(string s) =>
            new ArgumentException($"No enumerated matches string '{s}'.");
    }
}