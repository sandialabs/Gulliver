using System.Collections.Generic;
using JetBrains.Annotations;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     Byte Enumerable interface
    /// </summary>
    [PublicAPI]
    public interface IByteEnumerable : IReadOnlyCollection<byte>
    {
        /// <summary>
        ///     Returns <see langword="true" /> if the given <see cref="IByteEnumerable" /> is of little endian ordering
        /// </summary>
        bool IsLittleEndian { get; }

        /// <summary>
        ///     Returns <see langword="true" /> if most significant zero bytes are trimmed from iteration
        /// </summary>
        bool IsTrimmed { get; }

        /// <summary>
        ///     Reverse enumerator
        /// </summary>
        IEnumerator<byte> GetReverseEnumerator();

        /// <summary>
        ///     Reverse the enumerable
        /// </summary>
        IEnumerable<byte> ReverseEnumerable();
    }
}
