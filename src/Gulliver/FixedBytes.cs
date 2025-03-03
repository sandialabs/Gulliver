using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Gulliver
{
    /// <summary>
    ///     An unsigned big-endian integer aligned representation of a readonly fixed length enumerable of bytes.
    /// </summary>
    [Obsolete("Candidate for removal in future major version")]
    public class FixedBytes
        : IFormattable,
            IReadOnlyCollection<byte>,
            IEquatable<FixedBytes>,
            IEquatable<IEnumerable<byte>>,
            IComparable<FixedBytes>,
            IComparable<IEnumerable<byte>>,
            IComparable
    {
        /// <summary>
        ///     Gets underlying bytes, the hero of our journey
        /// </summary>
        /// <value>
        ///     Underlying bytes
        /// </value>
        protected byte[] UnderlyingBytes { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FixedBytes" /> class.
        /// </summary>
        /// <param name="bytes">the enumerable of bytes to be copied</param>
        /// <param name="length">the optional final </param>
        public FixedBytes(IEnumerable<byte> bytes, int? length = null)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var byteArray = bytes is byte[] array ? array : bytes.ToArray();

            var byteLength = byteArray.Length;

            if (length.HasValue && length.Value < byteLength)
            {
                throw new ArgumentException(
                    $"When defined {nameof(length)} must not be less than the length of {nameof(bytes)}",
                    nameof(length)
                );
            }

            this.UnderlyingBytes = new byte[length ?? byteLength]; // create a new array of appropriate size

            // copy bytes as appropriate from the original source
            Array.Copy(byteArray, 0, this.UnderlyingBytes, length - byteLength ?? 0, byteLength);
        }

        /// <inheritdoc />
        public int Count => this.UnderlyingBytes.Length;

        /// <summary>
        ///     Indexer
        /// </summary>
        /// <param name="idx">index</param>
        public byte this[int idx] =>
            idx < this.UnderlyingBytes.Length && idx >= 0
                ? this.UnderlyingBytes[idx]
                : throw new ArgumentOutOfRangeException(
                    nameof(idx),
                    idx,
                    $"provided index must be 0 or greater, and less than {this.UnderlyingBytes.Length}"
                );

        #region From Interface IComparable

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return 1;
            }

            if (ReferenceEquals(this, obj))
            {
                return 0;
            }

            return obj is FixedBytes other
                ? this.CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(FixedBytes)}");
        }

        #endregion

        #region From Interface IComparable<FixedBytes>

        /// <inheritdoc />
        public int CompareTo(FixedBytes other)
        {
            return other == null ? 1 : ByteArrayUtils.CompareUnsignedBigEndian(this.UnderlyingBytes, other.UnderlyingBytes);
        }

        #endregion

        #region From Interface IComparable<IEnumerable<byte>>

        /// <inheritdoc />
        public int CompareTo(IEnumerable<byte> other)
        {
            return other == null
                ? 1
                : ByteArrayUtils.CompareUnsignedBigEndian(
                    this.UnderlyingBytes,
                    other is byte[] byteArray ? byteArray : other.ToArray()
                );
        }

        #endregion

        #region From Interface IEnumerable

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region From Interface IEnumerable<byte>

        /// <inheritdoc />
        public IEnumerator<byte> GetEnumerator()
        {
            return this.UnderlyingBytes.ToList().AsReadOnly().GetEnumerator();
        }

        #endregion

        #region From Interface IEquatable<FixedBytes>

        /// <inheritdoc />
        public bool Equals(FixedBytes other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || this.Equals(other.UnderlyingBytes));
        }

        #endregion

        #region From Interface IEquatable<IEnumerable<byte>>

        /// <inheritdoc />
        public bool Equals(IEnumerable<byte> other)
        {
            return other != null
                && ByteArrayUtils.CompareUnsignedBigEndian(
                    this.UnderlyingBytes,
                    other is byte[] byteArray ? byteArray : other.ToArray()
                ) == 0;
        }

        #endregion

        #region From Interface IFormattable

        /// <summary>
        ///     <para>The following formats are provided.</para>
        ///     <list type="bullet">
        ///         <item>
        ///             <term>g (default) G, or H</term>
        ///             <description>formats as upper case hexadecimal digits with individual bytes delimited by a space character</description>
        ///         </item>
        ///         <item>
        ///             <term>HC</term>
        ///             <description>formats as upper case contiguous hexadecimal digits</description>
        ///         </item>
        ///         <item>
        ///             <term>h</term>
        ///             <description>formats as lower case hexadecimal digits with individual bytes delimited by a space character</description>
        ///         </item>
        ///         <item>
        ///             <term>h</term>
        ///             <description>formats as lower case contiguous hexadecimal digits</description>
        ///         </item>
        ///         <item>
        ///             <term>b, or B</term>
        ///             <description>formats as binary digits with individual bytes delimited by a space character</description>
        ///         </item>
        ///         <item>
        ///             <term>bc, or BC</term>
        ///             <description>formats as contiguous binary digits</description>
        ///         </item>
        ///         <item>
        ///             <term>d, or D</term>
        ///             <description>formats as decimal digits with individual bytes delimited by a space character</description>
        ///         </item>
        ///         <item>
        ///             <term>I, or IBE</term>
        ///             <description>formats as big endian unsigned decimal integer</description>
        ///         </item>
        ///         <item>
        ///             <term>ILE</term>
        ///             <description>formats as little endian unsigned decimal integer</description>
        ///         </item>
        ///     </list>
        /// </summary>
        /// <param name="format">the format</param>
        /// <param name="formatProvider">the format provider</param>
        /// <returns>
        ///     A string representation of the underlying bytes formatted according to the specified <paramref name="format"/>
        ///     and using the provided <paramref name="formatProvider"/>.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.UnderlyingBytes.ToString(format, formatProvider);
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Similar to <see cref="Equals(IEnumerable{byte})" />, but checks exact byte value (does not ignore 0 valued most
        ///     significant bytes)
        /// </summary>
        /// <param name="other">the value to compare to</param>
        /// <returns>
        ///     <see langword="true"/> if the underlying bytes are equal to the specified <paramref name="other"/> bytes
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public bool Exactly(IEnumerable<byte> other)
        {
            return other != null && this.UnderlyingBytes.SequenceEqual(other);
        }

        /// <summary>
        ///     Similar to <see cref="Equals(FixedBytes)" />, but checks exact byte value (does not ignore 0 valued most
        ///     significant bytes)
        /// </summary>
        /// <param name="other">the value to compare to</param>
        /// <returns>
        ///     <see langword="true"/> if the underlying bytes are equal to the specified <paramref name="other"/> bytes
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public bool Exactly(FixedBytes other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || this.Exactly(other.UnderlyingBytes));
        }

        /// <summary>
        ///     Get a copy of the bytes as little-endian (least significant byte at 0th index)
        ///     intrinsically reversing the bytes
        /// </summary>
        /// <returns>
        ///     A new byte array containing the bytes in little-endian order, with the least significant byte at the 0th
        ///     index.
        /// </returns>
        public byte[] GetBytesLittleEndian()
        {
            return this.UnderlyingBytes.ReverseBytes();
        }

        /// <summary>
        ///     Creates a new instance of an empty <see cref="FixedBytes" />
        /// </summary>
        /// <returns>A new instance of an empty <see cref="FixedBytes"/></returns>
        public static FixedBytes Empty()
        {
            return Array.Empty<byte>().ToFixedBytes();
        }

        /// <summary>
        ///     Get a copy of the bytes
        /// </summary>
        /// <returns>A new instance of an empty <see cref="FixedBytes"/>.</returns>
        public byte[] GetBytes()
        {
            var destinationArray = new byte[this.UnderlyingBytes.Length];
            Array.Copy(this.UnderlyingBytes, destinationArray, this.UnderlyingBytes.Length);
            return destinationArray;
        }

        #region Equality

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj)
                && (ReferenceEquals(this, obj) || (obj.GetType() == GetType() && this.Equals((FixedBytes)obj)));
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.UnderlyingBytes != null ? ByteHash() : 0;

            int ByteHash()
            {
                // unchecked to avoid overflow of addition / multiplication operations
                unchecked
                {
                    return this.UnderlyingBytes.Aggregate(19, (current, b) => (current * 31) + b.GetHashCode());
                }
            }
        }

        #endregion

        #region Operators

        #region implicit to FixedBytes

        /// <summary>
        ///     Explicit conversion of <see cref="byte" /> array to <see cref="FixedBytes" />
        /// </summary>
        /// <param name="bytes">the source bytes</param>
        public static explicit operator FixedBytes(byte[] bytes)
        {
            return ToFixedBytes(bytes);
        }

        /// <summary>
        ///     Explicit conversion of <see cref="byte" /> List to <see cref="FixedBytes" />
        /// </summary>
        /// <param name="bytes">the source bytes</param>
        public static explicit operator FixedBytes(List<byte> bytes)
        {
            return ToFixedBytes(bytes);
        }

        /// <summary>
        ///     Explicit conversion of <see cref="byte" /> array to <see cref="FixedBytes" />
        /// </summary>
        /// <param name="bytes">the source bytes</param>
        /// <returns>
        ///     A new instance of <see cref="FixedBytes"/> created from the specified <paramref name="bytes"/>
        ///     or <see langword="null"/> if the input is <see langword="null"/>
        /// </returns>
        public static FixedBytes ToFixedBytes(byte[] bytes)
        {
            return bytes == null ? null : new FixedBytes(bytes);
        }

        /// <summary>
        ///     Explicit conversion of <see cref="byte" /> List to <see cref="FixedBytes" />
        /// </summary>
        /// <param name="bytes">the source bytes</param>
        /// <returns>
        ///     A new instance of <see cref="FixedBytes"/> created from the specified <paramref name="bytes"/>
        ///     or <see langword="null"/> if the input is <see langword="null"/>
        /// </returns>
        public static FixedBytes ToFixedBytes(List<byte> bytes)
        {
            return bytes == null ? null : new FixedBytes(bytes);
        }

        #endregion

        #region implicit from FixtBytes

        /// <summary>
        ///     Implicit conversion from <see cref="FixedBytes" /> to <see cref="byte" /> array
        /// </summary>
        /// <param name="fixedBytes">input</param>
        public static implicit operator byte[](FixedBytes fixedBytes)
        {
            return FromFixedBytes(fixedBytes);
        }

        /// <summary>
        ///     Conversion from <see cref="FixedBytes" /> to <see cref="byte" /> array
        /// </summary>
        /// <param name="fixedBytes">input</param>
        /// <returns>
        ///     A byte array containing the bytes from the specified <paramref name="fixedBytes"/>
        ///     or <see langword="null"/> if the input is <see langword="null"/>
        /// </returns>
        public static byte[] FromFixedBytes(FixedBytes fixedBytes)
        {
            return fixedBytes == null ? null : fixedBytes.GetBytes();
        }

        #endregion

        #region Arithmetic

        /// <summary>
        ///     Addition
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static FixedBytes operator +(FixedBytes left, FixedBytes right)
        {
            return Add(left, right);
        }

        /// <summary>
        ///     Addition
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        /// <returns>
        ///     A new instance of <see cref="FixedBytes"/> representing the sum of the specified <paramref name="left"/>
        ///     and <paramref name="right"/> operands or <see langword="null"/> if either operand is <see langword="null"/>
        /// </returns>
        public static FixedBytes Add(FixedBytes left, FixedBytes right)
        {
            if (left == null || right == null)
            {
                return null;
            }

            return ByteArrayUtils.AddUnsignedBigEndian(left.UnderlyingBytes, right.UnderlyingBytes).ToFixedBytes();
        }

        /// <summary>
        ///     Subtraction
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static FixedBytes operator -(FixedBytes left, FixedBytes right)
        {
            return Subtract(left, right);
        }

        /// <summary>
        ///     Subtraction
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        /// <returns>
        ///     A new instance of <see cref="FixedBytes"/> representing the result of subtracting the specified
        ///     <paramref name="right"/>  operand from the <paramref name="left"/> operand or <see langword="null"/>
        ///     if either operand is <see langword="null"/>
        /// </returns>
        public static FixedBytes Subtract(FixedBytes left, FixedBytes right)
        {
            if (left == null || right == null)
            {
                return null;
            }

            return ByteArrayUtils.SubtractUnsignedBigEndian(left.UnderlyingBytes, right.UnderlyingBytes).ToFixedBytes();
        }

        #endregion

        #region Logical

        /// <summary>
        ///     Logical Or
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static FixedBytes operator |(FixedBytes left, FixedBytes right)
        {
            return BitwiseOr(left, right);
        }

        /// <summary>
        ///     Logical Or
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        /// <returns>
        ///     A new instance of <see cref="FixedBytes"/> representing the result of the bitwise OR operation
        ///     between the specified <paramref name="left"/> and <paramref name="right"/> operands or
        ///     <see langword="null"/> if either operand is <see langword="null"/>
        /// </returns>
        public static FixedBytes BitwiseOr(FixedBytes left, FixedBytes right)
        {
            if (left == null || right == null)
            {
                return null;
            }

            return ByteArrayUtils.BitwiseOrBigEndian(left.UnderlyingBytes, right.GetBytes()).ToFixedBytes();
        }

        /// <summary>
        ///     Logical And
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static FixedBytes operator &(FixedBytes left, FixedBytes right)
        {
            return BitwiseAnd(left, right);
        }

        /// <summary>
        ///     Logical And
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        /// <returns>
        ///     A new instance of <see cref="FixedBytes"/> representing the result of the bitwise AND operation
        ///     between the specified <paramref name="left"/> and <paramref name="right"/> operands or
        ///     <see langword="null"/> if either operand is <see langword="null"/>
        /// </returns>
        public static FixedBytes BitwiseAnd(FixedBytes left, FixedBytes right)
        {
            if (left == null || right == null)
            {
                return null;
            }

            return ByteArrayUtils.BitwiseAndBigEndian(left.UnderlyingBytes, right.GetBytes()).ToFixedBytes();
        }

        /// <summary>
        ///     Logical Xor
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static FixedBytes operator ^(FixedBytes left, FixedBytes right)
        {
            return Xor(left, right);
        }

        /// <summary>
        ///     Logical Xor
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        /// <returns>
        ///     A new instance of <see cref="FixedBytes"/> representing the result of the bitwise XOR operation
        ///     between the specified <paramref name="left"/> and <paramref name="right"/> operands or
        ///     <see langword="null"/> if either operand is <see langword="null"/>
        /// </returns>
        public static FixedBytes Xor(FixedBytes left, FixedBytes right)
        {
            if (left == null || right == null)
            {
                return null;
            }

            return ByteArrayUtils.BitwiseXorBigEndian(left.UnderlyingBytes, right.GetBytes()).ToFixedBytes();
        }

        /// <summary>
        ///     Logical Not
        /// </summary>
        /// <param name="operand">the operand</param>
        public static FixedBytes operator !(FixedBytes operand)
        {
            return LogicalNot(operand);
        }

        /// <summary>
        ///     Logical Not
        /// </summary>
        /// <param name="operand">the operand</param>
        /// <returns>
        ///     A new instance of <see cref="FixedBytes"/> representing the result of the logical NOT operation
        ///     on the specified <paramref name="operand"/> or <see langword="null"/> if the operand is <see langword="null"/>
        /// </returns>
        public static FixedBytes LogicalNot(FixedBytes operand)
        {
            if (operand == null)
            {
                return null;
            }

            return ByteArrayUtils.BitwiseNot(operand.UnderlyingBytes).ToFixedBytes();
        }

        /// <summary>
        ///     Left Shift
        /// </summary>
        /// <param name="fixedBytes">input</param>
        /// <param name="shift">the shift count</param>
        public static FixedBytes operator <<(FixedBytes fixedBytes, int shift)
        {
            return LeftShift(fixedBytes, shift);
        }

        /// <summary>
        ///     Left Shift
        /// </summary>
        /// <param name="fixedBytes">input</param>
        /// <param name="shift">the shift count</param>
        /// <returns>
        ///     A new instance of <see cref="FixedBytes"/> representing the result of left shifting the specified
        ///     <paramref name="fixedBytes"/> by the given <paramref name="shift"/> count or <see langword="null"/>
        ///     if the input is <see langword="null"/>
        /// </returns>
        public static FixedBytes LeftShift(FixedBytes fixedBytes, int shift)
        {
            if (fixedBytes == null)
            {
                return null;
            }

            if (shift < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(shift), $"{nameof(shift)} must be 0 or greater");
            }

            return fixedBytes.UnderlyingBytes.ShiftBitsLeft(shift).ToFixedBytes();
        }

        /// <summary>
        ///     Right Shift
        /// </summary>
        /// <param name="fixedBytes">input</param>
        /// <param name="shift">the shift count</param>
        public static FixedBytes operator >>(FixedBytes fixedBytes, int shift)
        {
            return RightShift(fixedBytes, shift);
        }

        /// <summary>
        ///     Right Shift
        /// </summary>
        /// <param name="fixedBytes">input</param>
        /// <param name="shift">the shift count</param>
        /// <returns>
        ///     A new instance of <see cref="FixedBytes"/> representing the result of right shifting the specified
        ///     <paramref name="fixedBytes"/> by the given <paramref name="shift"/> count or <see langword="null"/>
        ///     if the input is <see langword="null"/>
        /// </returns>
        public static FixedBytes RightShift(FixedBytes fixedBytes, int shift)
        {
            if (fixedBytes == null)
            {
                return null;
            }

            if (shift < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(shift), $"{nameof(shift)} must be 0 or greater");
            }

            return fixedBytes.UnderlyingBytes.ShiftBitsRight(shift).ToFixedBytes();
        }

        #endregion

        #region Comparrison operators

        /// <summary>
        ///     Less than operator
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static bool operator <(FixedBytes left, FixedBytes right)
        {
            return Comparer<FixedBytes>.Default.Compare(left, right) < 0;
        }

        /// <summary>
        ///     Greater than operator
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static bool operator >(FixedBytes left, FixedBytes right)
        {
            return Comparer<FixedBytes>.Default.Compare(left, right) > 0;
        }

        /// <summary>
        ///     Less than or equal to operator
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static bool operator <=(FixedBytes left, FixedBytes right)
        {
            return Comparer<FixedBytes>.Default.Compare(left, right) <= 0;
        }

        /// <summary>
        ///     Greater than or equal to operator
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static bool operator >=(FixedBytes left, FixedBytes right)
        {
            return Comparer<FixedBytes>.Default.Compare(left, right) >= 0;
        }

        #endregion

        #region Equality Operator

        /// <summary>
        ///     Equal
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static bool operator ==(FixedBytes left, FixedBytes right)
        {
            return ReferenceEquals(left, right) || (left?.Equals(right) ?? false);
        }

        /// <summary>
        ///     Not Equal
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public static bool operator !=(FixedBytes left, FixedBytes right)
        {
            return !ReferenceEquals(left, right) && !(left?.Equals(right) ?? false);
        }

        #endregion

        #endregion
    }
}
