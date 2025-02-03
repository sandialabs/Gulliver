using System.Collections.Generic;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     Byte Enumerable interface
    /// </summary>
    public interface IByteEnumerable : IReadOnlyCollection<byte>
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
        bool IsTrimmed { get; }

        /// <summary>
        ///     Reverse enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator<byte> GetReverseEnumerator();

        /// <summary>
        ///     Reverse the enumerable
        /// </summary>
        /// <returns></returns>
        IEnumerable<byte> ReverseEnumerable();
    }
}
