using System;

namespace Gulliver.DocExamples.GeneralByteArrayOperations
{
    /// <summary>
    ///     Byte Array Creation and Population examples
    /// </summary>
    public static class ByteArrayCreationAndPopulation
    {
        #region Byte Array Creation and Population

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
            Console.WriteLine(string.Empty);
        }

        #endregion end: CreateByteArray

        #endregion end: Byte Array Creation and Population
    }
}
