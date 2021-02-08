using System;

namespace VMToHackASM.Utilities
{
    public static class EnumUtils
    {
        /// <summary>
        ///     Gets an enumerated type equivalent to the string provided.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T StringToEnum<T>(string s)
        {
            bool genericNotAnEnum = !typeof(T).IsEnum;
            if (genericNotAnEnum) throw new ArgumentException($"{typeof(T)} is not an enumerated type.");

            var enumValues = (T[]) Enum.GetValues(typeof(T));

            foreach (var enumValue in enumValues)
            {
                string enumValueString = enumValue.ToString();
                if (enumValueString == null) throw new ArgumentNullException($"Type {s} is null.");

                string enumValueStringLowerCase = enumValueString.ToLower();
                if (s.Contains(enumValueStringLowerCase)) return enumValue;
            }

            throw new ArgumentException($"Cannot find an enumerated type equivalent to '{s}'.");
        }
    }
}