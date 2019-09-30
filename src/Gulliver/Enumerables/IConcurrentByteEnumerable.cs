using System.Collections.Generic;
using JetBrains.Annotations;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     Concurrent Byte Enumerable interface
    /// </summary>
    [PublicAPI]
    public interface IConcurrentByteEnumerable : IReadOnlyCollection<(byte leftByte, byte rightByte)>
    {
        /// <summary>
        ///     Returns <see langword="true" /> if the given <see cref="IByteEnumerable" /> is of little endian ordering
        /// </summary>
        bool IsLittleEndian { get; }

        /// <summary>
        ///     Returns <see langword="true" /> if most significant zero bytes are trimmed from iteration
        /// </summary>
        bool IsMutuallyTrimmed { get; }

        /// <summary>
        ///     Returns <see langword="true" /> if most significant zero bytes are trimmed left
        /// </summary>
        bool IsLeftTrimmed { get; }

        /// <summary>
        ///     Returns <see langword="true" /> if most significant zero bytes are trimmed right
        /// </summary>
        bool IsRightTrimmed { get; }

        /// <summary>
        ///     The count of bytes for the largest <see cref="IByteEnumerable" />, same as count
        /// </summary>
        int MaxCount { get; }

        /// <summary>
        ///     The count of bytes for the shortest <see cref="IByteEnumerable" />, same as count
        /// </summary>
        int MinCount { get; }

        /// <summary>
        ///     Get the Left <see cref="IByteEnumerable" />
        /// </summary>
        IByteEnumerable Left { get; }

        /// <summary>
        ///     Get the Right <see cref="IByteEnumerable" />
        /// </summary>
        IByteEnumerable Right { get; }

        /// <summary>
        ///     Reverse enumerator
        /// </summary>
        IEnumerator<(byte leftByte, byte rightByte)> GetReverseEnumerator();

        /// <summary>
        ///     Reverse the enumerable
        /// </summary>
        IEnumerable<(byte leftByte, byte rightByte)> ReverseEnumerable();

        /// <summary>
        ///     Get an enumerable that starts as the most significant byte
        /// </summary>
        IEnumerable<(byte leftByte, byte rightByte)> GetMsbToLsbEnumerable();

        /// <summary>
        ///     Get an enumerable that starts as the least significant byte
        /// </summary>
        IEnumerable<(byte leftByte, byte rightByte)> GetLsbToMsbEnumerable();
    }
}
