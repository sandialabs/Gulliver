using System;
using JetBrains.Annotations;

namespace Gulliver
{
    /// <summary>
    ///     Byte Array helper methods
    /// </summary>
    [PublicAPI]
    public static partial class ByteArrayUtils
    {
        /// <summary>
        ///     Create a <c>byte[]</c> of  the given <paramref name="length" /> composed only of
        ///     <paramref name="element" /> valued bytes
        /// </summary>
        /// <param name="length">the number of bytes within the byte array</param>
        /// <param name="element">the byte value to fill the array with</param>
        /// <returns>the created byte array</returns>
        [NotNull]
        public static byte[] CreateByteArray(int length,
                                             byte element = 0x00)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, "length must be greater than or equal to 0");
            }

            var result = new byte[length];

            for (var i = 0; i < length; i++)
            {
                result[i] = element;
            }

            return result;
        }

        /// <summary>
        ///     Return the given byte array in reverse
        /// </summary>
        /// <param name="bytes">the source bytes</param>
        [NotNull]
        public static byte[] ReverseBytes([NotNull] this byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var length = bytes.Length;

            var result = new byte[length];

            for (var i = 0; i < length; i++)
            {
                result[length - 1 - i] = bytes[i];
            }

            return result;
        }

        #region Bitwise Operations

        /// <summary>
        ///     Perform a bitwise inversion on bits within array of bytes
        /// </summary>
        /// <param name="bytes">the <see langword="byte" /> array to invert</param>
        /// <returns>the inverted byte array</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bytes" /> is <see langword="null" />.</exception>
        [NotNull]
        public static byte[] BitwiseNot([NotNull] byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var result = new byte[bytes.Length];

            for (var i = 0; i < bytes.Length; i++)
            {
                result[i] = (byte)~bytes[i];
            }

            return result;
        }

        #endregion

        #region Shift

        /// <summary>
        ///     Address an array of <see cref="byte" /> as <see cref="bool" /> bits
        ///     8 bits per byte
        /// </summary>
        /// <param name="bytes">the source bytes</param>
        /// <param name="index">the bit index</param>
        public static bool AddressBit([NotNull] this byte[] bytes,
                                      int index)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var bitLength = bytes.Length * 8;

            if (index < 0
                || index >= bitLength)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, $"index must be between 0 and {bitLength}");
            }

            var byteIndex = index / 8;
            var b = bytes[byteIndex]; // get the byte we're interested in

            var bitIndex = index % 8;
            var bit = (b >> bitIndex) & 0x01;

            return bit != 0; // ((input[index / 8] >> index % 8) & 0x01) != 0
        }

        /// <summary>
        ///     Shift a bits in byte array shiftCount times left
        /// </summary>
        /// <param name="bytes">byte array of bits to shift</param>
        /// <param name="shift">the number of bit positions to shift</param>
        /// <returns>a byte array of shifted bits</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bytes" /> is <see langword="null" />.</exception>
        [NotNull]
        public static byte[] ShiftBitsLeft([NotNull] this byte[] bytes,
                                           int shift)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (shift < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(shift), shift, $"{nameof(shift)} must be 0 or greater");
            }

            return ShiftBitsLeft(bytes, shift, out _);
        }

        /// <summary>
        ///     Shift a bits in byte array <paramref name="shift" /> times left
        ///     An operation on an empty byte array will result in an empty byte array for carry regardless of shift
        /// </summary>
        /// <param name="bytes">the byte array to shift</param>
        /// <param name="shift">the number of shifts to perform</param>
        /// <param name="carry">the carried bits as bytes</param>
        /// <returns>the shifted byte array</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bytes" /> is <see langword="null" />.</exception>
        [NotNull]
        public static byte[] ShiftBitsLeft([NotNull] this byte[] bytes,
                                           int shift,
                                           out byte[] carry)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (shift < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(shift), shift, $"{nameof(shift)} must be 0 or greater");
            }

            var length = bytes.Length;

            var result = new byte[length];

            if (length == 0    // nothing to shift
                || shift == 0) // no shifting, return copy of original with an empty carry byte array
            {
                carry = Array.Empty<byte>();
                Array.Copy(bytes, result, length);
                return result;
            }

            var byteOffset = shift / 8;
            var shiftOffset = shift % 8;
            var carryLength = byteOffset
                              + (shiftOffset > 0
                                     ? 1
                                     : 0); // get the ceiling of the division w/o using Math.Ceiling to get size need for carry

            carry = new byte[carryLength]; // define carry

            if (shiftOffset == 0) // shifting is along the byte border, nothing special has to be done beyond some copy operations in order to shift
            {
                var sourceCopyBreak = length - byteOffset;
                if (sourceCopyBreak > 0) // partial carry
                {
                    Array.Copy(bytes, carryLength, result, 0, length - carryLength); // result copy
                    Array.Copy(bytes, 0, carry, 0, carryLength);                     // carry copy
                }
                else // complete carry
                {
                    Array.Copy(bytes, 0, carry, 0, length); // carry copy
                }

                return result;
            }

            // ragged shifting (shifting on non-byte boundaries)
            for (var i = 0; i < length; i++)
            {
                // shift one byte (8 bits) at a time
                var sourceByte = bytes[i];
                var resultByte = (byte)(sourceByte << shiftOffset);      // left shift source byte by the bit shift offset creating a 0-suffixed bits
                var carryByte = (byte)(sourceByte >> (8 - shiftOffset)); // right shift source byte so that its value is equivalent to the carry of the above left shift operation

                var resultIndex = i - byteOffset; //length - 1 - byteOffset + i -1;
                var carryIndex = resultIndex - 1;

                // set the destination byte
                if (resultIndex >= 0)
                {
                    // set destination byte on result
                    result[resultIndex] |= resultByte;
                }
                else
                {
                    carry[carryLength + resultIndex] |= resultByte;
                }

                // set the cary byte
                if (carryIndex >= 0)
                {
                    // set the carry byte on the result
                    result[carryIndex] |= carryByte;
                }
                else
                {
                    carry[carryLength + carryIndex] |= carryByte;
                }
            }

            return result;
        }

        /// <summary>
        ///     Shift a bits in byte array <paramref name="shift" /> times right
        /// </summary>
        /// <param name="bytes">byte array of bits to shift</param>
        /// <param name="shift">the number of bit positions to shift</param>
        /// <returns>a byte array of shifted bits</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bytes" /> is <see langword="null" />.</exception>
        [NotNull]
        public static byte[] ShiftBitsRight([NotNull] this byte[] bytes,
                                            int shift)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (shift < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(shift), shift, $"{nameof(shift)} must be 0 or greater");
            }

            return ShiftBitsRight(bytes, shift, out _);
        }

        /// <summary>
        ///     Shift bits in byte array <paramref name="shift" /> times right
        ///     An operation on an empty byte array will result in an empty byte array for carry regardless of shift
        /// </summary>
        /// <param name="bytes">the byte array to shift</param>
        /// <param name="shift">the number of shifts to perform</param>
        /// <param name="carry">the carried bits as bytes</param>
        /// <returns>the shifted byte array</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bytes" /> is <see langword="null" />.</exception>
        [NotNull]
        public static byte[] ShiftBitsRight([NotNull] this byte[] bytes,
                                            int shift,
                                            out byte[] carry)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (shift < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(shift), shift, $"{nameof(shift)} must be 0 or greater");
            }

            var length = bytes.Length;

            var result = new byte[length];

            if (length == 0    // nothing to shift
                || shift == 0) // no shifting, return copy of original with an empty carry byte array
            {
                carry = Array.Empty<byte>();
                Array.Copy(bytes, result, length);
                return result;
            }

            var byteOffset = shift / 8;
            var shiftOffset = shift % 8;
            var carryLength = byteOffset
                              + (shiftOffset > 0
                                     ? 1
                                     : 0); // get the ceiling of the division w/o using Math.Ceiling to get size need for carry

            carry = new byte[carryLength]; // define carry

            if (shiftOffset == 0) // shifting is along the byte border, nothing special has to be done beyond some copy operations in order to shift
            {
                var sourceCopyBreak = length - byteOffset;

                if (sourceCopyBreak > 0) // partial carry
                {
                    Array.Copy(bytes, 0, result, byteOffset, sourceCopyBreak); // result copy
                    Array.Copy(bytes, sourceCopyBreak, carry, 0, carryLength); // carry copy
                }
                else // complete carry
                {
                    Array.Copy(bytes, 0, carry, carryLength - length, length); // carry copy
                }

                return result;
            }

            // ragged shifting (shifting on non-byte boundaries)
            for (var i = 0; i < length; i++)
            {
                // shift one byte (8 bits) at a time
                var sourceByte = bytes[i];
                var resultByte = (byte)(sourceByte >> shiftOffset);      // right shift source byte by the bit shift offset creating a 0-prefixed bits
                var carryByte = (byte)(sourceByte << (8 - shiftOffset)); // left shift source byte so that its value is equivalent to the carry of the above right shift operation

                var resultIndex = i + byteOffset;
                var carryIndex = resultIndex + 1; // the cary byte sits on byte after the destination byte

                // set the destination byte
                if (resultIndex < length)
                {
                    // set destination byte on result
                    result[resultIndex] |= resultByte;
                }
                else
                {
                    // set destination byte on the carry
                    carry[resultIndex - length] |= resultByte;
                }

                // set the cary byte
                if (carryIndex < length)
                {
                    // set the carry byte on the result
                    result[carryIndex] |= carryByte;
                }
                else
                {
                    // set the carry byte on the carry
                    carry[carryIndex - length] |= carryByte;
                }
            }

            return result;
        }

        #endregion

        #region Append

        /// <summary>
        ///     Concatenate <paramref name="count" /> <paramref name="element" /> valued bytes to the end of they byte array
        /// </summary>
        /// <param name="source">the source bytes</param>
        /// <param name="count">number of bytes to append</param>
        /// <param name="element">the byte to append</param>
        public static byte[] AppendBytes([NotNull] this byte[] source,
                                         int count,
                                         byte element = 0x00)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, $"{nameof(count)} must be greater than or equal to 0");
            }

            var result = new byte[source.Length + count];

            Array.Copy(source, result, source.Length);
            Array.Copy(CreateByteArray(count, element), 0, result, source.Length, count);

            return result;
        }

        /// <summary>
        ///     Take two bye arrays, appending the shortest with 0x00 valued bytes so that both are the same length
        ///     effectively adds leading 0x00 to the shortest little endian byte array
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static (byte[] left, byte[] right) AppendShortest([NotNull] byte[] left,
                                                                 [NotNull] byte[] right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            var leftLength = left.Length;
            var rightLength = right.Length;

            if (leftLength == rightLength)
            {
                return (left, right);
            }

            return leftLength > rightLength
                       ? (left, AppendBytes(right, leftLength - rightLength))
                       : (AppendBytes(left, rightLength - leftLength), right);
        }

        #endregion

        #region Prepend

        /// <summary>
        ///     Prepend <paramref name="count" /> <paramref name="element" /> valued bytes to the start of they byte array
        /// </summary>
        /// <param name="source">the source bytes</param>
        /// <param name="count">number of bytes to prepend</param>
        /// <param name="element">the byte to append</param>
        public static byte[] PrependBytes([NotNull] this byte[] source,
                                          int count,
                                          byte element = 0x00)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, $"{nameof(count)} must be greater than or equal to 0");
            }

            var result = new byte[count + source.Length];

            Array.Copy(CreateByteArray(count, element), result, count);
            Array.Copy(source, 0, result, count, source.Length);

            return result;
        }

        /// <summary>
        ///     Take two bye arrays, pretending the shortest with 0x00 valued bytes so that both are the same length
        ///     effectively adds leading 0x00 to the shortest big endian byte array
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static (byte[] left, byte[] right) PrependShortest([NotNull] byte[] left,
                                                                  [NotNull] byte[] right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            var leftLength = left.Length;
            var rightLength = right.Length;

            if (leftLength == rightLength)
            {
                return (left, right);
            }

            return leftLength > rightLength
                       ? (left, PrependBytes(right, leftLength - rightLength))
                       : (PrependBytes(left, rightLength - leftLength), right);
        }

        #endregion
    }
}
