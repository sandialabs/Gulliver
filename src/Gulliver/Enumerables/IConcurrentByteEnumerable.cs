using System.Collections.Generic;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     Concurrent Byte Enumerable interface
    /// </summary>
    public interface IConcurrentByteEnumerable : IReadOnlyCollection<(byte leftByte, byte rightByte)>
    {
        /// <summary>
        ///     Gets a value indicating whether the given <see cref="IByteEnumerable" /> is of little endian ordering
        /// </summary>
        /// <value>
        /// A value indicating whether the given <see cref="IByteEnumerable" /> is of little endian ordering
        /// </value>
        bool IsLittleEndian { get; }

        /// <summary>
        ///     Gets a value indicating whether the most significant zero bytes are trimmed from enumeration
        /// </summary>
        /// <value>
        /// A value indicating whether returns the most significant zero bytes are trimmed from enumeration
        /// </value>
        bool IsMutuallyTrimmed { get; }

        /// <summary>
        ///     Gets a value indicating whether the most significant zero bytes are trimmed left
        /// </summary>
        /// <value>
        /// A value indicating whether the most significant zero bytes are trimmed left
        /// </value>
        bool IsLeftTrimmed { get; }

        /// <summary>
        ///     Gets a value indicating whether the most significant zero bytes are trimmed right
        /// </summary>
        /// <value>
        /// A value indicating whether the most significant zero bytes are trimmed right
        /// </value>
        bool IsRightTrimmed { get; }

        /// <summary>
        ///     Gets the count of bytes for the largest <see cref="IByteEnumerable" />. Same as count
        /// </summary>
        /// <value>
        /// The count of bytes for the largest <see cref="IByteEnumerable" />
        /// </value>
        int MaxCount { get; }

        /// <summary>
        ///     Gets the count of bytes for the shortest <see cref="IByteEnumerable" />. Same as count
        /// </summary>
        /// <value>
        /// The count of bytes for the shortest <see cref="IByteEnumerable" />
        /// </value>
        int MinCount { get; }

        /// <summary>
        ///     Gets the Left <see cref="IByteEnumerable" />
        /// </summary>
        /// <value>
        /// The Left <see cref="IByteEnumerable" />
        /// </value>
        IByteEnumerable Left { get; }

        /// <summary>
        ///     Gets he Right <see cref="IByteEnumerable" />
        /// </summary>
        /// <value>
        /// The Right <see cref="IByteEnumerable" />
        /// </value>
        IByteEnumerable Right { get; }

        /// <summary>
        ///     Reverse enumerator
        /// </summary>
        /// <returns>
        ///     An enumerator that iterates through pairs of bytes in reverse order,
        ///     yielding tuples containing the first byte and the second byte.
        /// </returns>
        IEnumerator<(byte leftByte, byte rightByte)> GetReverseEnumerator();

        /// <summary>
        ///     Reverse the enumerable
        /// </summary>
        /// <returns>
        ///     An enumerable that iterates through pairs of bytes in reverse order,
        ///     yielding tuples containing the first byte and the second byte.
        /// </returns>
        IEnumerable<(byte leftByte, byte rightByte)> ReverseEnumerable();

        /// <summary>
        ///     Get an enumerable that starts as the most significant byte
        /// </summary>
        /// <returns>
        ///     An enumerable that iterates through pairs of bytes starting from the most significant byte,
        ///     yielding tuples containing the first byte (most significant) and the second byte (least significant).
        /// </returns>
        IEnumerable<(byte leftByte, byte rightByte)> GetMsbToLsbEnumerable();

        /// <summary>
        ///     Get an enumerable that starts as the least significant byte
        /// </summary>
        /// <returns>
        ///     An enumerable that iterates through pairs of bytes starting from the least significant byte,
        ///     yielding tuples containing the first byte and the second byte.
        /// </returns>
        IEnumerable<(byte leftByte, byte rightByte)> GetLsbToMsbEnumerable();
    }
}
