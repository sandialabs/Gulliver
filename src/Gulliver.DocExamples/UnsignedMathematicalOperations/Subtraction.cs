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
            var lhs = new byte[] { 0xDE, 0x1E, 0x7E, 0xD0 };
            var rhs = new byte[] { 0xC0, 0xDE };

            // Act
            var result = ByteArrayUtils.SubtractUnsignedBigEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("SubtractUnsignedBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);

            Console.WriteLine($"lhs:\t{lhs.ToString("IBE")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("IBE")}");
            Console.WriteLine($"result:\t{result.ToString("IBE")}");
            Console.WriteLine(string.Empty);
        }

        public static void SubtractUnsignedLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] { 0xDE, 0x1E, 0x7E, 0xD0 };
            var rhs = new byte[] { 0xC0, 0xDE };

            // Act
            var result = ByteArrayUtils.SubtractUnsignedLittleEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("SubtractUnsignedLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);

            Console.WriteLine($"lhs:\t{lhs.ToString("ILE")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("ILE")}");
            Console.WriteLine($"result:\t{result.ToString("ILE")}");
            Console.WriteLine(string.Empty);
        }
    }
}
