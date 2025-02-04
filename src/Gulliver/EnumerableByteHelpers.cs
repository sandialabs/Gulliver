using System;
using System.Collections.Generic;

namespace Gulliver
{
    /// <summary>
    ///     Static Extension helper methods for <see cref="IEnumerable{T}" />
    /// </summary>
    [Obsolete("Canidate for removal in future major version")]
    public static class EnumerableByteHelpers
    {
        /// <summary>
        ///     Create a <see cref="FixedBytes" /> based on the provided <paramref name="bytes" />
        /// </summary>
        /// <param name="bytes">bytes to copy into a new<see cref="FixedBytes" /> </param>
        /// <returns></returns>
        public static FixedBytes ToFixedBytes(this IEnumerable<byte> bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            return new FixedBytes(bytes);
        }
    }
}
