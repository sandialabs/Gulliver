using System;

namespace Gulliver.DocExamples.GeneralByteArrayOperations
{
    /// <summary>
    ///     Byte Array Mutation examples
    /// </summary>
    public static class ByteArrayMutation
    {
        #region ReverseBytes

        public static void ReverseBytesExample() { }

        #endregion end: ReverseBytes

        #region Trimming

        #region TrimBigEndianLeadingZeroBytes

        /// <summary>
        ///     Example usage of <see cref="ByteArrayUtils.TrimBigEndianLeadingZeroBytes" />
        /// </summary>
        public static void TrimBigEndianLeadingZeroBytesExample()
        {
            // Setup
            var input = new byte[] {0x00, 0x00, 0x2A, 0x00};

            // Act
            var result = input.TrimBigEndianLeadingZeroBytes();

            // Conclusion
            Console.WriteLine("TrimBigEndianLeadingZeroBytes example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: TrimBigEndianLeadingZeroBytes

        #region TrimLittleEndianLeadingZeroBytes

        /// <summary>
        ///     Example usage of <see cref="ByteArrayUtils.TrimLittleEndianLeadingZeroBytes" />
        /// </summary>
        public static void TrimLittleEndianLeadingZeroBytesExample()
        {
            // Setup
            var input = new byte[] {0x2A, 0xFF, 0x2A, 0x00};

            // Act
            var result = input.TrimLittleEndianLeadingZeroBytes();

            // Conclusion
            Console.WriteLine("TrimLittleEndianLeadingZeroBytes Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: TrimLittleEndianLeadingZeroBytes

        #endregion end: Trimming

        #region Padding

        #region PadBigEndianMostSignificantBytes

        public static void PadBigEndianMostSignificantBytesExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("PadBigEndianMostSignificantBytes Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: PadBigEndianMostSignificantBytes

        #region PadLittleEndianMostSignificantBytes

        public static void PadLittleEndianMostSignificantBytesExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("PadLittleEndianMostSignificantBytes Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: PadLittleEndianMostSignificantBytes

        #endregion end: Padding

        #region Appending

        #region AppendBytes

        public static void AppendBytesExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("AppendBytes Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: AppendBytes

        #region AppendShortest

        public static void AppendShortestExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("AppendShortest Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: AppendShortest

        #endregion end: Appending

        #region Prepend

        #region PrependBytes

        public static void PrependBytesExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("PrependBytes Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: PrependBytes

        #region PrependShortest

        public static void PrependShortestExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("PrependShortest Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: PrependShortest

        #endregion end: Prepend
    }
}
