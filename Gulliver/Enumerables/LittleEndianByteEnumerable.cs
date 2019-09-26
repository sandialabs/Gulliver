using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     an enumerable used to iterate over an <see cref="IEnumerable{T}" /> of <see cref="byte" /> in as LittleEndian,
    ///     automatically ignoring zero-valued most significant bytes
    /// </summary>
    [PublicAPI]
    public class LittleEndianByteEnumerable : AbstractByteEnumerable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LittleEndianByteEnumerable"/> class.
        /// </summary>
        /// <param name="bytes">bytes in little endian order used to make the enumerable</param>
        /// <param name="trim"><see langword="true" /> if most significant zero bytes should be trimmed on instantiation</param>
        public LittleEndianByteEnumerable([NotNull] IEnumerable<byte> bytes,
                                          bool trim = true)
            : base(ConstructBytes(bytes, trim), trim, true)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
        }

        internal static byte[] ConstructBytes(IEnumerable<byte> bytes,
                                              bool trim)
        {
            // LittleEndian most significant bytes are at the highest index (end of collection)
            return trim
                       ? Trim() // remove most significant zero-valued bytes during iteration
                       : bytes.ToArray();

            byte[] Trim()
            {
                var stack = new Stack<byte>(); // using a stack to reverse collected bytes
                foreach (var x in bytes.Reverse()
                                       .SkipWhile(b => b == 0x00)) // iterate in reverse skipping zero-valued bytes found at the original collections end
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
        public static LittleEndianByteEnumerable FromBigEndian([NotNull] IEnumerable<byte> bytes,
                                                               bool trim = true)
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
        public static LittleEndianByteEnumerable FromSystemEndian([NotNull] IEnumerable<byte> bytes,
                                                                  bool trim = true)
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
