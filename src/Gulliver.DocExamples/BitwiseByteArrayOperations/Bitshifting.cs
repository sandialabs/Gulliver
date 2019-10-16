using System;

namespace Gulliver.DocExamples.BitwiseByteArrayOperations
{
    /// <summary>
    ///     Bitshifting examples
    /// </summary>
    public static class Bitshifting
    {
        #region ShiftBitsRight

        public static void ShiftBitsRightExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("ShiftBitsRight Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        public static void ShiftBitsRightCarryExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("ShiftBitsRight Carry Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: ShiftBitsRight

        #region ShiftBitsLeft

        public static void ShiftBitsLeftExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("ShiftBitsLeft Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        public static void ShiftBitsLeftCarryExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("ShiftBitsLeft Carry Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: ShiftBitsLeft
    }
}
