using System;

namespace Gulliver.DocExamples.GeneralByteArrayOperations
{
    /// <summary>
    ///     Byte Array Mutation examples
    /// </summary>
    public static class ByteArrayMutation
    {
        #region ReverseBytes

        public static void ReverseBytesExample()
        {
            // Setup
            var input = new byte[] { 0xC0, 0x1D, 0xC0, 0xFF, 0xEE };

            // Act
            var result = input.ReverseBytes();

            // Conclusion
            Console.WriteLine("ReverseBytes example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: ReverseBytes

        #region Trimming

        #region TrimBigEndianLeadingZeroBytes

        /// <summary>
        ///     Example usage of <see cref="ByteArrayUtils.TrimBigEndianLeadingZeroBytes" />
        /// </summary>
        public static void TrimBigEndianLeadingZeroBytesExample()
        {
            // Setup
            var input = new byte[] { 0x00, 0x00, 0x2A, 0x00 };

            // Act
            var result = input.TrimBigEndianLeadingZeroBytes();

            // Conclusion
            Console.WriteLine("TrimBigEndianLeadingZeroBytes example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: TrimBigEndianLeadingZeroBytes

        #region TrimLittleEndianLeadingZeroBytes

        /// <summary>
        ///     Example usage of <see cref="ByteArrayUtils.TrimLittleEndianLeadingZeroBytes" />
        /// </summary>
        public static void TrimLittleEndianLeadingZeroBytesExample()
        {
            // Setup
            var input = new byte[] { 0x2A, 0xFF, 0x2A, 0x00 };

            // Act
            var result = input.TrimLittleEndianLeadingZeroBytes();

            // Conclusion
            Console.WriteLine("TrimLittleEndianLeadingZeroBytes Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: TrimLittleEndianLeadingZeroBytes

        #endregion end: Trimming

        #region Padding

        #region PadBigEndianMostSignificantBytes

        public static void PadBigEndianMostSignificantBytesShortExample()
        {
            // Setup
            var bytes = new byte[] { 0xDE, 0xFA, 0xCE, 0xC0, 0xDE };
            const int finalLength = 3;

            // Act

            var result = bytes.PadBigEndianMostSignificantBytes(finalLength);

            // Conclusion
            Console.WriteLine("PadBigEndianMostSignificantBytes Example");
            Console.WriteLine($"input:\t{bytes.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        public static void PadBigEndianMostSignificantBytesExample()
        {
            // Setup
            var bytes = new byte[] { 0xDE, 0xFA, 0xCE, 0xC0, 0xDE };
            const int finalLength = 6;

            // Act
            var result = bytes.PadBigEndianMostSignificantBytes(finalLength);

            // Conclusion
            Console.WriteLine("PadBigEndianMostSignificantBytes Short Example");
            Console.WriteLine($"input:\t{bytes.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: PadBigEndianMostSignificantBytes

        #region PadLittleEndianMostSignificantBytes

        public static void PadLittleEndianMostSignificantBytesShortExample()
        {
            // Setup
            var bytes = new byte[] { 0xDE, 0xFA, 0xCE, 0xC0, 0xDE };
            const int finalLength = 3;

            // Act
            var result = bytes.PadLittleEndianMostSignificantBytes(finalLength);

            // Conclusion
            Console.WriteLine("PadLittleEndianMostSignificantBytes Short Example");
            Console.WriteLine($"input:\t{bytes.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        public static void PadLittleEndianMostSignificantBytesExample()
        {
            // Setup
            var input = new byte[] { 0xDE, 0xFA, 0xCE, 0xC0, 0xDE };
            const int finalLength = 6;

            // Act
            var result = input.PadLittleEndianMostSignificantBytes(finalLength);

            // Conclusion
            Console.WriteLine("PadLittleEndianMostSignificantBytes Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: PadLittleEndianMostSignificantBytes

        #endregion end: Padding

        #region Appending

        #region AppendBytes

        public static void AppendBytesExample()
        {
            // Setup
            var input = new byte[] { 0xC0, 0xC0, 0xCA, 0xFE };
            const int count = 4;

            // Act
            var result = input.AppendBytes(count);

            // Conclusion
            Console.WriteLine("AppendBytes Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        public static void AppendBytesElementExample()
        {
            // Setup
            var input = new byte[] { 0xC0, 0xC0, 0xCA, 0xFE };
            const int count = 2;
            const byte element = 0xFF;

            // Act
            var result = input.AppendBytes(count, element);

            // Conclusion
            Console.WriteLine("AppendBytes Element Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: AppendBytes

        #region AppendShortest

        public static void AppendShortestExample()
        {
            // Setup
            var lhs = new byte[] { 0xDE, 0xCA, 0xF0 };
            var rhs = new byte[] { 0xCA, 0xFE, 0xC0, 0xFF, 0xEE };

            // Act
            var (lhsResult, rhsResult) = ByteArrayUtils.AppendShortest(lhs, rhs);

            // Conclusion
            Console.WriteLine("AppendShortest Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"lhsResult:\t{lhsResult.ToString("H")}");
            Console.WriteLine($"lhsResult:\t{rhsResult.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: AppendShortest

        #endregion end: Appending

        #region Prepend

        #region PrependBytes

        public static void PrependBytesExample()
        {
            // Setup
            var input = new byte[] { 0xC0, 0xC0, 0xCA, 0xFE };
            const int count = 4;

            // Act
            var result = input.PrependBytes(count);

            // Conclusion
            Console.WriteLine("PrependBytes Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        public static void PrependBytesElementExample()
        {
            // Setup
            var input = new byte[] { 0xC0, 0xC0, 0xCA, 0xFE };
            const int count = 2;
            const byte element = 0xFF;

            // Act
            var result = input.PrependBytes(count, element);

            // Conclusion
            Console.WriteLine("PrependBytes Element Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: PrependBytes

        #region PrependShortest

        public static void PrependShortestExample()
        {
            // Setup
            var lhs = new byte[] { 0xDE, 0xCA, 0xF0 };
            var rhs = new byte[] { 0xCA, 0xFE, 0xC0, 0xFF, 0xEE };

            // Act
            var (lhsResult, rhsResult) = ByteArrayUtils.PrependShortest(lhs, rhs);

            // Conclusion
            Console.WriteLine("PrependShortest Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"lhsResult:\t{lhsResult.ToString("H")}");
            Console.WriteLine($"lhsResult:\t{rhsResult.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: PrependShortest

        #endregion end: Prepend
    }
}
