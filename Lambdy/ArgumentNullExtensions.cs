using System;

namespace Lambdy
{
    internal static class ArgumentNullExtensions
    {
        public static T ThrowIfNull<T>(this T argument, string paramName)
        {
            if (argument is null) throw new ArgumentNullException(paramName);
            return argument;
        }
    }
}
