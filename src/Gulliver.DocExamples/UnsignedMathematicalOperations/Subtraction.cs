using System;

namespace Gulliver.DocExamples.UnsignedMathematicalOperations
{
    /// <summary>
    ///     Subtraction Examples
    /// </summary>
    public static class Subtraction
    {
        public static void SubtractUnsignedBigEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("SubtractUnsignedBigEndian Example");
            Console.WriteLine($"input:\t{lhs.ToString("H")}");
            Console.WriteLine($"input:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        public static void SubtractUnsignedLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("SubtractUnsignedLittleEndian Example");
            Console.WriteLine($"input:\t{lhs.ToString("H")}");
            Console.WriteLine($"input:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }
    }
}
