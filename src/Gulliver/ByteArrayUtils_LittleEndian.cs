using System;
using System.Collections.Generic;
using System.Linq;
using Gulliver.Enumerables;

namespace Gulliver
{
    /// <summary>
    ///     Byte Array helper methods - LittleEndian
    /// </summary>
    public static partial class ByteArrayUtils
    {
        /// <summary>
        ///     Compare two unsigned little endian <see cref="byte" /> arrays
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        /// <returns>
        ///     A signed integer indicating the relative order of the two byte arrays:
        ///     <see langword="1"/> if <paramref name="left"/> is greater,
        ///     <see langword="-1"/> if <paramref name="right"/> is greater,
        ///     or <see langword="0"/> if they are equal.
        /// </returns>
        public static int CompareUnsignedLittleEndian(byte[] left, byte[] right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            var enumerable = new ConcurrentLittleEndianByteEnumerable(left, right).GetMsbToLsbEnumerable();
            foreach (var (leftByte, rightByte) in enumerable)
            {
                var comparison = leftByte.CompareTo(rightByte);

                if (comparison != 0)
                {
                    return comparison > 0 ? 1 : -1;
                }
            }

            return 0; // bytes are equal
        }

        /// <summary>
        ///     Gets the effective length (length if right most 0x00 bytes are trimmed) of a little endian byte array
        /// </summary>
        /// <param name="input">the input bytes</param>
        /// <returns>
        ///     The effective length of the byte array, which is the total length minus the number of trailing 0x00 bytes.
        /// </returns>
        public static int LittleEndianEffectiveLength(this byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var i = input.Length;
            while (i > 0 && input[i - 1] == 0x00)
            {
                i--;
            }

            return i;
        }

        /// <summary>
        ///     Trim all 0x00 valued bytes starting at the highest index
        /// </summary>
        /// <param name="input">the input bytes</param>
        /// <returns>
        ///     A new byte array containing the input bytes with all trailing 0x00 bytes removed.
        /// </returns>
        public static byte[] TrimLittleEndianLeadingZeroBytes(this byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var effectiveLength = LittleEndianEffectiveLength(input);
            var result = new byte[effectiveLength];
            Array.Copy(input, result, effectiveLength);

            return result;
        }

        #region PadBigEndianMostSignificantBytes

        /// <summary>
        ///     Pad a byte array, the given array is already larger than the final length it will return the original array
        /// </summary>
        /// <param name="source">the source bytes</param>
        /// <param name="finalLength">the final length of the padding</param>
        /// <param name="element">the byte to pad with</param>
        /// <returns>
        ///     The original byte array if its length is greater than or equal to <paramref name="finalLength"/>;
        ///     otherwise, a new byte array padded with <paramref name="element"/> to reach the specified
        ///     <paramref name="finalLength"/>.
        /// </returns>
        public static byte[] PadLittleEndianMostSignificantBytes(this byte[] source, int finalLength, byte element = 0x00)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (finalLength < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(finalLength),
                    finalLength,
                    $"{nameof(finalLength)} must be greater than or equal to 0"
                );
            }

            if (source.Length >= finalLength)
            {
                return source;
            }

            return AppendBytes(source, finalLength - source.Length, element);
        }

        #endregion // end: PadBigEndianMostSignificantBytes

        #region Arithmatic

        /// <summary>
        ///     Add / Subtract <paramref name="delta" /> to little endian <paramref name="source" />, handling the
        ///     system endianness as appropriate
        /// </summary>
        /// <param name="source">the source bytes</param>
        /// <param name="delta">the value to change by</param>
        /// <param name="result">the result</param>
        /// <returns>
        ///     <see langword="true"/> if the operation was successful and the result is valid,
        ///     <see langword="false"/> if the operation was a subtraction that would result in a negative value.
        /// </returns>
        public static bool TrySumLittleEndian(byte[] source, long delta, out byte[] result)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (delta == 0)
            {
                result = source.TrimLittleEndianLeadingZeroBytes();
                return true;
            }

            var rightOperand = GetLittleEndianAbsBytes(delta);

            if (delta < 0) // subtraction
            {
                if (CompareUnsignedLittleEndian(source, rightOperand) < 0)
                {
                    result = Array.Empty<byte>();
                    return false;
                }

                result = SubtractUnsignedLittleEndian(source, rightOperand);
                return true;
            }

            // addition
            result = AddUnsignedLittleEndian(source, rightOperand);
            return true;
        }

        private static byte[] GetLittleEndianAbsBytes(long input)
        {
            if (input == long.MinValue)
            {
                // long.MinValue (-9223372036854775808) is a special case
                // getting the absolute value of a the minimum value of a twos complement number is invalid and will cause a OverflowException
                // We must therefor build the equivalent of abs(long.MinValue) little endian bytes manually
                return new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80 };
            }

            var bytes = BitConverter.GetBytes(Math.Abs(input));

            return BitConverter.IsLittleEndian ? bytes : bytes.ReverseBytes();
        }

        /// <summary>
        ///     Add two byte[] treated as unsigned little endian
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        /// <returns>
        ///     A new byte array representing the sum of the two input byte arrays treated as unsigned little endian.
        /// </returns>
        public static byte[] AddUnsignedLittleEndian(byte[] left, byte[] right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            var enumerable = new ConcurrentLittleEndianByteEnumerable(left, right).GetLsbToMsbEnumerable();
            var resultQueue = new Queue<byte>(); // use stack for FIFO
            var carry = 0;
            foreach (var (leftByte, rightByte) in enumerable)
            {
                var sum = carry + leftByte + rightByte;

                resultQueue.Enqueue((byte)(sum & 0xFF)); // push the byte value of sum
                carry = sum >> 8; // new carry value is sum shifted by 8 bits (a byte)
            }

            if (carry != 0) // if a carry value exists it should be pushed as it is the new MSB
            {
                resultQueue.Enqueue((byte)carry); // enqueue carry
            }

            return resultQueue.ToArray();
        }

        /// <summary>
        ///     Subtract <paramref name="right" /> from <paramref name="left" /> each treated as unsigned big endian
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        /// <exception cref="InvalidOperationException">The operation would result in a non-unsigned value</exception>
        /// <returns>
        ///     A new byte array representing the result of subtracting <paramref name="right"/> from
        ///     <paramref name="left"/> treated as unsigned little endian. An empty array is returned if the result is
        ///     zero.
        /// </returns>
        public static byte[] SubtractUnsignedLittleEndian(byte[] left, byte[] right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            var comparison = CompareUnsignedLittleEndian(left, right);

            if (comparison == 0)
            {
                return Array.Empty<byte>();
            }

            if (comparison == -1)
            {
                throw new InvalidOperationException("The operation would result in a non-unsigned value");
            }

            var enumerable = new ConcurrentLittleEndianByteEnumerable(left, right).GetLsbToMsbEnumerable();
            var resultQueue = new Queue<byte>();
            var borrowed = false;

            foreach (var (leftByte, rightByte) in enumerable)
            {
                byte minuend; // min·u·end /ˈminyəˌwend/ (noun) a quantity or number from which another is to be subtracted. Not to be confused with the subtrahend. Duh.
                var canBorrow = leftByte > 0;

                if (borrowed && canBorrow) // previous operation needed to borrow, and a borrow is possible
                {
                    minuend = (byte)(leftByte - 1); // decrement as this value has been borrowed from in the previous iteration
                }
                else if (borrowed) // && !canBorrow (implied)
                {
                    minuend = 0xff; // 0 byte becomes 255
                }
                else
                {
                    minuend = leftByte;
                }

                if (minuend >= rightByte) // left is big enough to subtract right
                {
                    var difference = (byte)(minuend - rightByte);
                    resultQueue.Enqueue(difference);

                    borrowed = borrowed && !canBorrow; // set borrow if a borrow happened some time previously but it could not be accommodated in this iteration; borrow from next
                }
                else // left is less than right, automatically borrow from next iteration
                {
                    var difference = (byte)((minuend + 0x0100) - rightByte); // handle borrowed
                    resultQueue.Enqueue(difference);
                    borrowed = true;
                }
            }

            return resultQueue.ToArray().TrimLittleEndianLeadingZeroBytes(); // TODO, do without explicit trimming?
        }

        #endregion

        #region Bitwise

        /// <summary>
        ///     Bitwise LittleEndian AND two arrays of bytes of the same size
        /// </summary>
        /// <param name="left">first operand</param>
        /// <param name="right">second operand</param>
        /// <returns>bitwise AND of the two arrays of bytes</returns>
        /// <exception cref="ArgumentNullException"><paramref name="left" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="right" /> is <see langword="null" />.</exception>
        public static byte[] BitwiseAndLittleEndian(byte[] left, byte[] right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return new ConcurrentLittleEndianByteEnumerable(left, right, false)
                .Select(bytes => (byte)(bytes.leftByte & bytes.rightByte))
                .ToArray();
        }

        /// <summary>
        ///     Bitwise LittleEndian OR two arrays of bytes of the same size
        /// </summary>
        /// <param name="left">first operand</param>
        /// <param name="right">second operand</param>
        /// <returns>bitwise OR of the two arrays of bytes</returns>
        /// <exception cref="ArgumentNullException"><paramref name="left" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="right" /> is <see langword="null" />.</exception>
        public static byte[] BitwiseOrLittleEndian(byte[] left, byte[] right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return new ConcurrentLittleEndianByteEnumerable(left, right, false)
                .Select(bytes => (byte)(bytes.leftByte | bytes.rightByte))
                .ToArray();
        }

        /// <summary>
        ///     Bitwise LittleEndian XOR two arrays of bytes of the same size
        /// </summary>
        /// <param name="left">first operand</param>
        /// <param name="right">second operand</param>
        /// <returns>bitwise OR of the two arrays of bytes</returns>
        /// <exception cref="ArgumentNullException"><paramref name="left" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="right" /> is <see langword="null" />.</exception>
        public static byte[] BitwiseXorLittleEndian(byte[] left, byte[] right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return new ConcurrentLittleEndianByteEnumerable(left, right, false)
                .Select(bytes => (byte)(bytes.leftByte ^ bytes.rightByte))
                .ToArray();
        }

        #endregion
    }
}
