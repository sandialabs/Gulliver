using System;
using System.Collections.Generic;
using System.Linq;
using Gulliver.Enumerables;
using JetBrains.Annotations;

namespace Gulliver
{
    /// <summary>
    ///     Byte Array helper methods - BigEndian
    /// </summary>
    public static partial class ByteArrayUtils
    {
        /// <summary>
        ///     Compare two unsigned big endian <see cref="byte" /> arrays
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static int CompareUnsignedBigEndian([NotNull] byte[] left,
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

            //var rightEffectiveLength = BigEndianEffectiveLength(left);
            //var leftEffectiveLength = BigEndianEffectiveLength(right);

            //// left is a larger unsigned number
            //if (rightEffectiveLength > leftEffectiveLength)
            //{
            //    return 1;
            //}

            //// right is a larger unsigned number
            //if (rightEffectiveLength < leftEffectiveLength)
            //{
            //    return -1;
            //}

            //// left and right have the same effective width, must compare starting at right edge MSB
            //for (var i = rightEffectiveLength - 1; i >= 0; i--)
            //{
            //    var a = left[left.Length - i - 1];
            //    var b = right[right.Length - i - 1];

            //    var comparison = a.CompareTo(b);

            //    if (comparison != 0)
            //    {
            //        return comparison > 0
            //                   ? 1
            //                   : -1;
            //    }
            //}

            //return 0; // bytes are equal

            var enumerable = new ConcurrentBigEndianByteEnumerable(left, right).GetMsbToLsbEnumerable();
            foreach (var (leftByte, rightByte) in enumerable)
            {
                var comparison = leftByte.CompareTo(rightByte);

                if (comparison != 0)
                {
                    return comparison > 0
                               ? 1
                               : -1;
                }
            }

            return 0; // bytes are equal
        }

        /// <summary>
        ///     Gets the effective length (length if left most 0x00 bytes are trimmed) of a big endian byte array
        /// </summary>
        /// <param name="input">the input bytes</param>
        public static int BigEndianEffectiveLength([NotNull] this byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var i = 0;
            while (i < input.Length
                   && input[i] == 0x00)
            {
                i++;
            }

            return input.Length - i;
        }

        /// <summary>
        ///     Trim all 0x00 valued bytes starting at the 0th index
        /// </summary>
        /// <param name="input">the input bytes</param>
        public static byte[] TrimBigEndianLeadingZeroBytes([NotNull] this byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var effectiveLength = BigEndianEffectiveLength(input);
            var result = new byte[effectiveLength];
            Array.Copy(input, input.Length - effectiveLength, result, 0, effectiveLength);

            return result;
        }

        #region PadBigEndianMostSignificantBytes

        /// <summary>
        ///     Pad a byte array, the the given array is already larger than the final length it will return the original array
        /// </summary>
        /// <param name="source">the source bytes</param>
        /// <param name="finalLength">the final length of the padding</param>
        /// <param name="element">the byte to pad with</param>
        public static byte[] PadBigEndianMostSignificantBytes([NotNull] this byte[] source,
                                                              int finalLength,
                                                              byte element = 0x00)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (finalLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(finalLength), finalLength, $"{nameof(finalLength)} must be greater than or equal to 0");
            }

            if (source.Length >= finalLength)
            {
                return source;
            }

            return PrependBytes(source, finalLength - source.Length, element);
        }

        #endregion // end: PadBigEndianMostSignificantBytes

        #region Arithmatic

        /// <summary>
        ///     Add / Subtract <paramref name="delta" /> to bigendian <paramref name="source" />, handling the system endianness as
        ///     appropriate
        /// </summary>
        /// <param name="source">the source bytes</param>
        /// <param name="delta">the value to change by</param>
        /// <param name="result">the result</param>
        public static bool TrySumBigEndian([NotNull] byte[] source,
                                           long delta,
                                           [NotNull] out byte[] result)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (delta == 0)
            {
                result = source.TrimBigEndianLeadingZeroBytes();
                return true;
            }

            var rightOperand = GetBigEndianAbsBytes(delta);

            if (delta < 0) // subtraction
            {
                if (CompareUnsignedBigEndian(source, rightOperand) < 0)
                {
                    result = Array.Empty<byte>();
                    return false;
                }

                result = SubtractUnsignedBigEndian(source, rightOperand);
                return true;
            }

            // addition
            result = AddUnsignedBigEndian(source, rightOperand);
            return true;

            byte[] GetBigEndianAbsBytes(long input)
            {
                if (input == long.MinValue)
                {
                    // long.MinValue (-9223372036854775808) is a special case
                    // getting the absolute value of a the minimum value of a twos complement number is invalid and will cause a OverflowException
                    // We must therefor build the equivalent of abs(long.MinValue) big endian bytes manually
                    return new byte[] {0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
                }

                var bytes = BitConverter.GetBytes(Math.Abs(input));

                return BitConverter.IsLittleEndian
                           ? bytes.ReverseBytes()
                           : bytes;
            }
        }

        /// <summary>
        ///     Add two byte[] treated as unsigned big endian
        /// </summary>
        /// <param name="right">the right side operand</param>
        /// <param name="left">the left side operand</param>
        [NotNull]
        public static byte[] AddUnsignedBigEndian([NotNull] byte[] right,
                                                  [NotNull] byte[] left)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            //var resultStack = new Stack<byte>();

            //var length = right.Length > left.Length
            //                 ? right.Length
            //                 : left.Length;

            //var carry = 0x00; // start with no carry value

            //for (var i = 0; i < length; i++)
            //{
            //    var sum = carry; // sum is initialized to carry in order to always incorporate it

            //    var leftIndex = right.Length - i - 1; // select left byte in reverse index order (right most edge)

            //    if (leftIndex >= 0
            //        && leftIndex < right.Length)
            //    {
            //        sum += right[leftIndex];
            //    }

            //    var rightIndex = left.Length - i - 1; // right left byte in reverse index order

            //    if (rightIndex >= 0
            //        && rightIndex < left.Length)
            //    {
            //        sum += left[rightIndex];
            //    }

            //    resultStack.Push((byte) (sum & 0xFF)); // push the byte value of sum
            //    carry = sum >> 8;                      // new carry value is sum shifted by 8 bits (a byte)
            //}

            //if (carry == 0) // if there is no carry return it trimming the possible 0-valued most most significant bytes
            //{
            //    return resultStack.ToArray()
            //                      .TrimBigEndianLeadingZeroBytes();
            //}

            //resultStack.Push((byte) carry); // push carry

            //return resultStack.ToArray();

            var enumerable = new ConcurrentBigEndianByteEnumerable(left, right).GetLsbToMsbEnumerable();
            var resultStack = new Stack<byte>(); // use stack for filo
            var carry = 0;
            foreach (var (leftByte, rightByte) in enumerable)
            {
                var sum = carry + leftByte + rightByte;

                resultStack.Push((byte) (sum & 0xFF)); // push the byte value of sum
                carry = sum >> 8;                      // new carry value is sum shifted by 8 bits (a byte)
            }

            if (carry != 0) // if a carry value exists it should be pushed as it is the new MSB
            {
                resultStack.Push((byte) carry); // push carry
            }

            return resultStack.ToArray();
        }

        /// <summary>
        ///     Subtract <paramref name="right" /> from <paramref name="left" /> each treated as unsigned big endian
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        /// <exception cref="InvalidOperationException">The operation would result in a non-unsigned value</exception>
        [NotNull]
        public static byte[] SubtractUnsignedBigEndian([NotNull] byte[] left,
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

            var byteComparison = CompareUnsignedBigEndian(left, right);

            if (byteComparison == 0)
            {
                return Array.Empty<byte>();
            }

            if (byteComparison == -1)
            {
                throw new InvalidOperationException("This operation would create an unsupported signed result");
            }

            //var resultStack = new Stack<byte>();

            //var length = left.Length > right.Length
            //                 ? left.Length
            //                 : right.Length;

            //var borrowed = false;
            //for (var i = 0; i < length; i++)
            //{
            //    byte leftByte = 0x00;
            //    byte rightByte = 0x00;

            //    var leftIndex = left.Length - i - 1; // select left byte in reverse index order (right most edge)

            //    if (leftIndex >= 0
            //        && leftIndex < left.Length)
            //    {
            //        leftByte = left[leftIndex];
            //    }

            //    var rightIndex = right.Length - i - 1; // right left byte in reverse index order

            //    if (rightIndex >= 0
            //        && rightIndex < right.Length)
            //    {
            //        rightByte = right[rightIndex];
            //    }

            //    var canBorrow = leftByte > 0x00;
            //    if (borrowed && canBorrow) // previous operation needed to borrow, and a borrow is possible
            //    {
            //        leftByte--; // decrement as this value has been borrowed from in the previous iteration
            //    }

            //    if (borrowed && !canBorrow)
            //    {
            //        leftByte = 0xff; // 0 byte becomes 255
            //    }

            //    if (leftByte >= rightByte) // left is big enough to subtract right
            //    {
            //        var difference = (byte)(leftByte - rightByte);
            //        resultStack.Push(difference);

            //        borrowed = borrowed && !canBorrow; // set borrow if a borrow happened some time previously but it could not be accommodated in this iteration; borrow from next
            //    }
            //    else // left is less than right, automatically borrow from next iteration
            //    {
            //        var difference = (byte)(leftByte + 0x0100 - rightByte); // handle borrowed
            //        resultStack.Push(difference);

            //        borrowed = true;
            //    }
            //}

            //return resultStack.ToArray()
            //                  .TrimBigEndianLeadingZeroBytes();

            var enumerable = new ConcurrentBigEndianByteEnumerable(left, right).GetLsbToMsbEnumerable();
            var resultStack = new Stack<byte>();
            var borrowed = false;

            foreach (var (leftByte, rightByte) in enumerable)
            {
                byte minuend; // min·u·end /ˈminyəˌwend/ (noun) a quantity or number from which another is to be subtracted. Not to be confused with the subtrahend. Duh.
                var canBorrow = leftByte > 0;

                if (borrowed && canBorrow) // previous operation needed to borrow, and a borrow is possible
                {
                    minuend = (byte) (leftByte - 1); // decrement as this value has been borrowed from in the previous iteration
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
                    var difference = (byte) (minuend - rightByte);
                    resultStack.Push(difference);

                    borrowed = borrowed && !canBorrow; // set borrow if a borrow happened some time previously but it could not be accommodated in this iteration; borrow from next
                }
                else // left is less than right, automatically borrow from next iteration
                {
                    var difference = (byte) ((minuend + 0x0100) - rightByte); // handle borrowed
                    resultStack.Push(difference);
                    borrowed = true;
                }
            }

            return resultStack.ToArray()
                              .TrimBigEndianLeadingZeroBytes(); // TODO, do without explicit trimming?
        }

        #endregion

        #region Bitwise

        /// <summary>
        ///     Bitwise BigEndian AND two arrays of bytes of the same size
        /// </summary>
        /// <param name="left">first operand</param>
        /// <param name="right">second operand</param>
        /// <returns>bitwise AND of the two arrays of bytes</returns>
        /// <exception cref="ArgumentNullException"><paramref name="left" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="right" /> is <see langword="null" />.</exception>
        [NotNull]
        public static byte[] BitwiseAndBigEndian([NotNull] byte[] left,
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

            //var length = left.Length > right.Length
            //                 ? left.Length
            //                 : right.Length;

            //var result = new byte[length];

            //for (var i = 0; i < length; i++)
            //{
            //    var aIndex = left.Length - length + i;
            //    var bIndex = right.Length - length + i;

            //    var a = aIndex >= 0 && aIndex < left.Length
            //                ? left[aIndex]
            //                : 0x00;

            //    var b = bIndex >= 0 && bIndex < right.Length
            //                ? right[bIndex]
            //                : 0x00;

            //    result[i] = (byte) (a & b);
            //}

            //return result;

            return new ConcurrentBigEndianByteEnumerable(left, right, false)
                   .Select(bytes => (byte) (bytes.leftByte & bytes.rightByte))
                   .ToArray();
        }

        /// <summary>
        ///     Bitwise BigEndian OR two arrays of bytes of the same size
        /// </summary>
        /// <param name="left">first operand</param>
        /// <param name="right">second operand</param>
        /// <returns>bitwise OR of the two arrays of bytes</returns>
        /// <exception cref="ArgumentNullException"><paramref name="left" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="right" /> is <see langword="null" />.</exception>
        [NotNull]
        public static byte[] BitwiseOrBigEndian([NotNull] byte[] left,
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

            //var length = left.Length > right.Length
            //                 ? left.Length
            //                 : right.Length;

            //var result = new byte[length];

            //for (var i = 0; i < length; i++)
            //{
            //    var aIndex = left.Length - length + i;
            //    var bIndex = right.Length - length + i;

            //    var a = aIndex >= 0 && aIndex < left.Length
            //                ? left[aIndex]
            //                : 0x00;

            //    var b = bIndex >= 0 && bIndex < right.Length
            //                ? right[bIndex]
            //                : 0x00;

            //    result[i] = (byte) (a | b);
            //}

            //return result;

            return new ConcurrentBigEndianByteEnumerable(left, right, false)
                   .Select(bytes => (byte) (bytes.leftByte | bytes.rightByte))
                   .ToArray();
        }

        /// <summary>
        ///     Bitwise BigEndian XOR two arrays of bytes of the same size
        /// </summary>
        /// <param name="left">first operand</param>
        /// <param name="right">second operand</param>
        /// <returns>bitwise OR of the two arrays of bytes</returns>
        /// <exception cref="ArgumentNullException"><paramref name="left" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="right" /> is <see langword="null" />.</exception>
        [NotNull]
        public static byte[] BitwiseXorBigEndian([NotNull] byte[] left,
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

            //var length = left.Length > right.Length
            //                 ? left.Length
            //                 : right.Length;

            //var result = new byte[length];

            //for (var i = 0; i < length; i++)
            //{
            //    var aIndex = left.Length - length + i;
            //    var bIndex = right.Length - length + i;

            //    var a = aIndex >= 0 && aIndex < left.Length
            //                ? left[aIndex]
            //                : 0x00;

            //    var b = bIndex >= 0 && bIndex < right.Length
            //                ? right[bIndex]
            //                : 0x00;

            //    result[i] = (byte) (a ^ b);
            //}

            //return result;

            return new ConcurrentBigEndianByteEnumerable(left, right, false)
                   .Select(bytes => (byte) (bytes.leftByte ^ bytes.rightByte))
                   .ToArray();
        }

        #endregion
    }
}
