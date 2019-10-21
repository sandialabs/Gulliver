using System;

namespace Gulliver.DocExamples.UnsignedMathematicalOperations
{
    /// <summary>
    ///     Comparison examples
    /// </summary>
    public static class Comparison
    {
        public static void CompareUnsignedBigEndianExample()
        {
            // Setup
            var lhs = new byte[] { 0xB1, 0x66, 0xE5, 0x70 };
            var rhs = new byte[] { 0x5A, 0x11 };

            // Act
            var result = ByteArrayUtils.CompareUnsignedBigEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("CompareUnsignedBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result}");
            Console.WriteLine(string.Empty);

            Console.WriteLine($"lhs:\t{lhs.ToString("IBE")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("IBE")}");
            Console.WriteLine($"result:\t{result}");
            Console.WriteLine(string.Empty);
        }

        public static void CompareUnsignedLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] { 0xB1, 0x66, 0xE5, 0x70 };
            var rhs = new byte[] { 0x5A, 0x11 };

            // Act
            var result = ByteArrayUtils.CompareUnsignedLittleEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("CompareUnsignedLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result}");
            Console.WriteLine(string.Empty);

            Console.WriteLine($"lhs:\t{lhs.ToString("ILE")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("ILE")}");
            Console.WriteLine($"result:\t{result}");
            Console.WriteLine(string.Empty);
        }
    }
}
