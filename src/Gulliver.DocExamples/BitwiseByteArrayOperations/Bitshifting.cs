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
            var input = new byte[] {0xAD, 0x0B, 0xEC, 0x0F, 0xFE, 0xE0};
            const int shift = 5;

            // Act
            var result = input.ShiftBitsRight(shift);

            // Conclusion
            Console.WriteLine("ShiftBitsRight Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"input:\t{input.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        public static void ShiftBitsRightCarryExample()
        {
            // Setup
            var input = new byte[] {0xAD, 0x0B, 0xEC, 0x0F, 0xFE, 0xE0};
            const int shift = 5;

            // Act
            var result = input.ShiftBitsRight(shift, out var carry);

            // Conclusion
            Console.WriteLine("ShiftBitsRight Carry Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine($"carry:\t{carry.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"input:\t{input.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine($"carry:\t{carry.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: ShiftBitsRight

        #region ShiftBitsLeft

        public static void ShiftBitsLeftExample()
        {
            // Setup
            var input = new byte[] {0xAD, 0x0B, 0xEC, 0x0F, 0xFE, 0xE0};
            const int shift = 5;

            // Act
            var result = input.ShiftBitsLeft(shift);

            // Conclusion
            Console.WriteLine("ShiftBitsLeft Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"input:\t{input.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        public static void ShiftBitsLeftCarryExample()
        {
            // Setup
            var input = new byte[] {0xAD, 0x0B, 0xEC, 0x0F, 0xFE, 0xE0};
            const int shift = 5;

            // Act
            var result = input.ShiftBitsLeft(shift, out var carry);

            // Conclusion
            Console.WriteLine("ShiftBitsLeft Carry Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine($"carry:\t{carry.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"input:\t{input.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine($"carry:\t{carry.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: ShiftBitsLeft
    }
}
