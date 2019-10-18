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
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("CompareUnsignedBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        public static void CompareUnsignedLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("CompareUnsignedLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }
    }
}
