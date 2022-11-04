using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Gulliver
{
    /// <summary>
    ///     Static Extension helper methods for <see cref="IEnumerable{T}" />
    /// </summary>
    [PublicAPI]
    public static class EnumerableByteHelpers
    {
        /// <summary>
        ///     Create a <see cref="FixedBytes" /> based on the provided <paramref name="bytes" />
        /// </summary>
        /// <param name="bytes">bytes to copy into a new<see cref="FixedBytes" /> </param>
        /// <returns></returns>
        [NotNull]
        public static FixedBytes ToFixedBytes([NotNull] this IEnumerable<byte> bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            return new FixedBytes(bytes);
        }
    }
}
