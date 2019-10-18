using System;

namespace Gulliver.DocExamples.BitwiseByteArrayOperations
{
    /// <summary>
    ///     Addressing examples
    /// </summary>
    public static class Addressing
    {
        #region AddressBit

        public static void AddressBitExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("AddressBit Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: AddressBit
    }
}
