using System;
using System.Collections.Generic;
using System.Linq;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     an enumerable used to iterate over an <see cref="IEnumerable{T}" /> of <see cref="byte" /> in as LittleEndian,
    ///     automatically ignoring zero-valued most significant bytes
    /// </summary>
    public class LittleEndianByteEnumerable : AbstractByteEnumerable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LittleEndianByteEnumerable"/> class.
        /// </summary>
        /// <param name="bytes">bytes in little endian order used to make the enumerable</param>
        /// <param name="trim"><see langword="true" /> if most significant zero bytes should be trimmed on instantiation</param>
        public LittleEndianByteEnumerable(IEnumerable<byte> bytes, bool trim = true)
            : base(ConstructBytes(bytes, trim), trim, true)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
        }

        /// <summary>
        ///     Constructs a byte array from the provided enumerable of bytes, optionally trimming trailing zero bytes.
        /// </summary>
        /// <param name="bytes">The source enumerable of bytes to construct the array from.</param>
        /// <param name="trim">
        ///     A boolean value indicating whether to trim trailing zero-valued bytes from the resulting array.
        /// </param>
        /// <returns>
        ///     A byte array containing the constructed bytes, with trailing zero bytes removed if <paramref name="trim"/> is <see langword="true"/>.
        /// </returns>
        internal static byte[] ConstructBytes(IEnumerable<byte> bytes, bool trim)
        {
            // LittleEndian most significant bytes are at the highest index (end of collection)
            return trim
                ? Trim() // remove most significant zero-valued bytes during iteration
                : bytes.ToArray();

            byte[] Trim()
            {
                var stack = new Stack<byte>(); // using a stack to reverse collected bytes
                foreach (var x in bytes.Reverse().SkipWhile(b => b == 0x00)) // iterate in reverse skipping zero-valued bytes found at the original collections end
                {
                    stack.Push(x);
                }

                return stack.ToArray();
            }
        }

        #region static factory methods

        /// <summary>
        ///     Build a LittleEndianByteEnumerable from a BigEndian source by reversing the input
        /// </summary>
        /// <param name="bytes">the source bytes</param>
        /// <param name="trim"><see langword="true" /> if most significant zero bytes should be trimmed</param>
        /// <returns>
        ///     A new instance of <see cref="LittleEndianByteEnumerable"/> created from the reversed input bytes,
        ///     optionally trimming the most significant zero bytes if <paramref name="trim"/> is <see langword="true"/>.
        /// </returns>
        public static LittleEndianByteEnumerable FromBigEndian(IEnumerable<byte> bytes, bool trim = true)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            return new LittleEndianByteEnumerable(bytes.Reverse(), trim);
        }

        /// <summary>
        ///     Build a LittleEndianByteEnumerable from a byte enumerable in system byte order as determined by
        ///     <see cref="BitConverter.IsLittleEndian" />
        /// </summary>
        /// <param name="bytes">the source bytes</param>
        /// <param name="trim"><see langword="true" /> if most significant zero bytes should be trimmed</param>
        /// <returns>
        ///     A new instance of <see cref="LittleEndianByteEnumerable"/> created from the input bytes,
        ///     using the original order if the system is little-endian, or reversed if the system is big-endian.
        /// </returns>
        public static LittleEndianByteEnumerable FromSystemEndian(IEnumerable<byte> bytes, bool trim = true)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            return BitConverter.IsLittleEndian
                ? new LittleEndianByteEnumerable(bytes, trim)
                : new LittleEndianByteEnumerable(bytes.Reverse(), trim);
        }

        #endregion
    }
}
