using System;
using System.Linq;

namespace Gulliver.DocExamples.GeneralByteArrayOperations
{
    /// <summary>
    ///     Byte Array Creation and Population examples
    /// </summary>
    public static class ByteArrayCreationAndPopulation
    {
        /// <summary>
        ///     Example usage of <see cref="ByteArrayUtils.CreateByteArray"/>
        /// </summary>
        public static void CreateByteArrayExample()
        {
            // Setup
            const int length = 10;
            const byte element = 0x42;  // optional, defaults to 0x00

            // Act
            var resultBytes = ByteArrayUtils.CreateByteArray(length, element);  // creates a byte array of length 10, filled with bytes of 0x42

            // Conclusion
            var asString = string.Join(", ", resultBytes.Select(b => $"0x{b:x2}"));
            Console.WriteLine($"[{asString}]"); // outputs "[0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42]"
        }
    }
}
