using System;
using System.Linq;

namespace Gulliver.DocExamples.GeneralByteArrayOperations
{

    /// <summary>
    ///     Byte Array Creation and Population examples
    /// </summary>
    public static class ByteArrayCreationAndPopulation
    {

        #region CreateByteArray

        /// <summary>
        ///     Example usage of <see cref="ByteArrayUtils.CreateByteArray" />
        /// </summary>
        public static void CreateByteArrayExample()
        {
            // Setup
            const int length = 10;
            const byte element = 0x42; // optional, defaults to 0x00

            // Act
            var result = ByteArrayUtils.CreateByteArray(length, element); // creates a byte array of length 10, filled with bytes of 0x42

            // Conclusion
            Console.WriteLine("CreateByteArray example");
            Console.WriteLine($"length:\t{length}");
            Console.WriteLine($"element:\t{element}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: CreateByteArray

        #region TrimLeadingZeroBytes

        #region TrimBigEndianLeadingZeroBytes

        /// <summary>
        ///     Example usage of <see cref="ByteArrayUtils.TrimBigEndianLeadingZeroBytes" />
        /// </summary>
        public static void TrimBigEndianLeadingZeroBytes()
        {
            // Setup
            var input = new byte[] { 0x00, 0x00, 0x2A, 0x00 };

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
        public static void TrimLittleEndianLeadingZeroBytes()
        {
            // Setup
            var input = new byte[] { 0x2A, 0xFF, 0x2A, 0x00 };

            // Act
            var result = input.TrimLittleEndianLeadingZeroBytes();

            // Conclusion
            Console.WriteLine("TrimLittleEndianLeadingZeroBytes");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: TrimLittleEndianLeadingZeroBytes

        #endregion end: TrimLeadingZeroBytes
    }
}
