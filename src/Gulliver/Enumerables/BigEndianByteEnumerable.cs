﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     an enumerable used to iterate over an <see cref="byte" /> of <see cref="IEnumerable{T}" /> in as BigEndian,
    ///     automatically ignoring zero-valued most significant bytes
    /// </summary>
    public class BigEndianByteEnumerable : AbstractByteEnumerable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BigEndianByteEnumerable"/> class.
        /// </summary>
        /// <param name="bytes">bytes in big endian order used to make the enumerable</param>
        /// <param name="trim"><see langword="true" /> if most significant zero bytes should be trimmed on instantiation</param>
        public BigEndianByteEnumerable(IEnumerable<byte> bytes, bool trim = true)
            : base(ConstructBytes(bytes, trim), trim, false)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
        }

        internal static byte[] ConstructBytes(IEnumerable<byte> bytes, bool trim)
        {
            // BigEndian most significant bytes are at the lowest index (start of collection)
            return bytes
                .SkipWhile(b => trim && b == 0x00) // remove most significant zero-valued bytes during iteration if called for
                .ToArray();
        }

        #region static factory methods

        /// <summary>
        ///     Build a BigEndianByteEnumerable from a LittleEndian source by reversing the input
        /// </summary>
        /// <param name="bytes">the source bytes</param>
        /// <param name="trim"><see langword="true" /> if most significant zero bytes should be trimmed</param>
        /// <returns></returns>
        public static BigEndianByteEnumerable FromLittleEndian(IEnumerable<byte> bytes, bool trim = true)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            return new BigEndianByteEnumerable(bytes.Reverse(), trim);
        }

        /// <summary>
        ///     Build a BigEndianByteEnumerable from a byte enumerable in system byte order as determined by
        ///     <see cref="BitConverter.IsLittleEndian" />
        /// </summary>
        /// <param name="bytes">the source bytes</param>
        /// <param name="trim"><see langword="true" /> if most significant zero bytes should be trimmed</param>
        /// <returns></returns>
        public static BigEndianByteEnumerable FromSystemEndian(IEnumerable<byte> bytes, bool trim = true)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            return BitConverter.IsLittleEndian
                ? new BigEndianByteEnumerable(bytes.Reverse(), trim)
                : new BigEndianByteEnumerable(bytes, trim);
        }

        #endregion
    }
}
