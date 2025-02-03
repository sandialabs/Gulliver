using System.Collections.Generic;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     Concurrent Byte Enumerable interface
    /// </summary>
    public interface IConcurrentByteEnumerable : IReadOnlyCollection<(byte leftByte, byte rightByte)>
    {
        /// <summary>
        ///     Gets a value indicating whether returns <see langword="true" /> if the given <see cref="IByteEnumerable" /> is of little endian ordering
        /// </summary>
        /// <value>
        /// A value indicating whether returns <see langword="true" /> if the given <see cref="IByteEnumerable" /> is of little endian ordering
        /// </value>
        bool IsLittleEndian { get; }

        /// <summary>
        ///     Gets a value indicating whether returns <see langword="true" /> if most significant zero bytes are trimmed from iteration
        /// </summary>
        /// <value>
        /// A value indicating whether returns <see langword="true" /> if most significant zero bytes are trimmed from iteration
        /// </value>
        bool IsMutuallyTrimmed { get; }

        /// <summary>
        ///     Gets a value indicating whether returns <see langword="true" /> if most significant zero bytes are trimmed left
        /// </summary>
        /// <value>
        /// A value indicating whether returns <see langword="true" /> if most significant zero bytes are trimmed left
        /// </value>
        bool IsLeftTrimmed { get; }

        /// <summary>
        ///     Gets a value indicating whether returns <see langword="true" /> if most significant zero bytes are trimmed right
        /// </summary>
        /// <value>
        /// A value indicating whether returns <see langword="true" /> if most significant zero bytes are trimmed right
        /// </value>
        bool IsRightTrimmed { get; }

        /// <summary>
        ///     Gets the count of bytes for the largest <see cref="IByteEnumerable" />, same as count
        /// </summary>
        /// <value>
        /// The count of bytes for the largest <see cref="IByteEnumerable" />, same as count
        /// </value>
        int MaxCount { get; }

        /// <summary>
        ///     Gets the count of bytes for the shortest <see cref="IByteEnumerable" />, same as count
        /// </summary>
        /// <value>
        /// The count of bytes for the shortest <see cref="IByteEnumerable" />, same as count
        /// </value>
        int MinCount { get; }

        /// <summary>
        ///     Gets get the Left <see cref="IByteEnumerable" />
        /// </summary>
        /// <value>
        /// Get the Left <see cref="IByteEnumerable" />
        /// </value>
        IByteEnumerable Left { get; }

        /// <summary>
        ///     Gets get the Right <see cref="IByteEnumerable" />
        /// </summary>
        /// <value>
        /// Get the Right <see cref="IByteEnumerable" />
        /// </value>
        IByteEnumerable Right { get; }

        /// <summary>
        ///     Reverse enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator<(byte leftByte, byte rightByte)> GetReverseEnumerator();

        /// <summary>
        ///     Reverse the enumerable
        /// </summary>
        /// <returns></returns>
        IEnumerable<(byte leftByte, byte rightByte)> ReverseEnumerable();

        /// <summary>
        ///     Get an enumerable that starts as the most significant byte
        /// </summary>
        /// <returns></returns>
        IEnumerable<(byte leftByte, byte rightByte)> GetMsbToLsbEnumerable();

        /// <summary>
        ///     Get an enumerable that starts as the least significant byte
        /// </summary>
        /// <returns></returns>
        IEnumerable<(byte leftByte, byte rightByte)> GetLsbToMsbEnumerable();
    }
}
