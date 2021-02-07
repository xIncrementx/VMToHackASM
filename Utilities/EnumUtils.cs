using System;

namespace VMToHackASM.Utilities
{
    public static class EnumUtils
    {
        /// <summary>
        /// Gets an enumerated type equivalent to the string provided.
        /// </summary>
        /// <param name="commandString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T StringToEnum<T>(string commandString)
        {
            bool genericNotAnEnum = !typeof(T).IsEnum;
            if (genericNotAnEnum) throw new ArgumentException($"{typeof(T)} is not an enumerated type.");

            var vmCommandEnums = (T[]) Enum.GetValues(typeof(T));

            foreach (var vmCommand in vmCommandEnums)
            {
                string vmCommandString = vmCommand.ToString();
                if (vmCommandString == null) throw new ArgumentNullException($"Command {commandString} is null.");

                string vmCommandStringLowerCase = vmCommandString.ToLower();
                if (commandString.Contains(vmCommandStringLowerCase)) return vmCommand;
            }

            throw new ArgumentException($"Cannot find an enumerated type equivalent to '{commandString}'.");
        }
    }
}