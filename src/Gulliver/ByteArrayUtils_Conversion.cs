using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using JetBrains.Annotations;

namespace Gulliver
{
    /// <summary>
    ///     Byte Array helper methods - Conversions
    /// </summary>
    public static partial class ByteArrayUtils
    {
        /// <summary>
        ///     <para>Convert a <c>byte[]</c>  to string
        ///     The following formats are provided. Note, that if the input is null the result will be en empty string</para>
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
        /// <param name="bytes">the byte array</param>
        /// <param name="format">the optional format specifier</param>
        /// <param name="formatProvider">the format provider</param>
        /// <returns>a byte array represented as a string</returns>
        [NotNull]
        public static string ToString(this byte[] bytes,
                                      string format = "g",
                                      IFormatProvider formatProvider = null)
        {
            if (formatProvider == null)
            {
                formatProvider = CultureInfo.InvariantCulture;
            }

            if (bytes == null)
            {
                return string.Empty;
            }

            switch (format?.Trim())
            {
                case null:
                case "":

                // general formats
                case "g":
                case "G":

                // Hexadecimal formats
                case "H": // upper hexadecimal
                    return DelimitedBaseConverter(bytes, 16)
                        .ToUpperInvariant();
                case "h": // lower hexadecimal
                    return DelimitedBaseConverter(bytes, 16);
                case "HC": // upper hexadecimal contiguous
                    return DelimitedBaseConverter(bytes, 16, string.Empty)
                        .ToUpperInvariant();
                case "hc": // lower hexadecimal contiguous
                    return DelimitedBaseConverter(bytes, 16, string.Empty);

                // decimal format
                case "d":
                case "D":
                    return DelimitedBaseConverter(bytes, 10);

                // octal formats
                case "o":
                case "O":
                    return DelimitedBaseConverter(bytes, 8);

                // binary formats
                case "b":
                case "B":
                    return DelimitedBaseConverter(bytes, 2);
                case "bc": // binary contiguous
                case "BC":
                    return DelimitedBaseConverter(bytes, 2, string.Empty);

                // big endian decimal integer formats
                case "I":
                case "IBE":
                    return BigEndianByteArrayToUnsignedDecimalInteger(bytes);
                case "ILE":
                    return LittleEndianByteArrayToUnsignedDecimalInteger(bytes);
                default:
                    throw new FormatException($"The \"{format}\" format string is not supported.");
            }

            string BigEndianByteArrayToUnsignedDecimalInteger(byte[] input)
            {
                // this is ugly and probably be internally replaced eventually so as not to use BigInteger
                // reverse to make handle endian, and toss a 0x00 at the end to avoid sign
                var inputLength = input.Length;
                var unsignedBigEndianBytes = new byte[inputLength + 1]; // one greater so trailing byte is always 0x00

                for (var i = 0; i < inputLength; i++)
                {
                    unsignedBigEndianBytes[inputLength - 1 - i] = input[i];
                }

                return new BigInteger(unsignedBigEndianBytes).ToString(formatProvider);
            }

            string LittleEndianByteArrayToUnsignedDecimalInteger(byte[] input)
            {
                // this is ugly and probably be internally replaced eventually so as not to use BigInteger
                var unsignedLittleEndianBytes = new byte[input.Length + 1]; // prefix with 0 byte to guarantee unsigned
                Array.Copy(input, 0, unsignedLittleEndianBytes, 0, input.Length);

                return new BigInteger(unsignedLittleEndianBytes).ToString(formatProvider);
            }

            string DelimitedBaseConverter(byte[] input,
                                          int @base,
                                          string delimiter = " ",
                                          int? paddingWidth = null,
                                          char paddingChar = '0')
            {
                if (!new[] { 2, 8, 10, 16 }.Contains(@base))
                {
                    throw new ArgumentException($"{nameof(@base)} must be 2, 8, 10, or 16 (binary, octal, decimal, or hexadecimal respectively)", nameof(@base));
                }

                if (paddingWidth.HasValue
                    && paddingWidth.Value < 0)
                {
                    throw new ArgumentException($"{nameof(paddingWidth)} may not be negative", nameof(paddingWidth));
                }

                // use padding width if defined, otherwise use a width of 8 for binary, 3 for octal and decimal, and 2 for hexadecimal
                var width = paddingWidth
                            ?? (@base == 2
                                    ? 8
                                    : @base == 8 || @base == 10
                                        ? 3
                                        : 2);

                return string.Join(delimiter ?? string.Empty,
                                   input.Select(b => Convert.ToString(b, @base)
                                                            .PadLeft(width, paddingChar)));
            }
        }
    }
}
