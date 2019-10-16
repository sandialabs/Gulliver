using System;

namespace Gulliver.DocExamples.UnsignedMathematicalOperations
{
    /// <summary>
    ///     Addition Examples
    /// </summary>
    public static class Addition
    {
        #region AddUnsigned

        public static void AddUnsignedBigEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("AddUnsignedBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        public static void AddUnsignedLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("AddUnsignedLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: AddUnsigned

        #region TrySum

        public static void TrySumBigEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("TrySumBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        public static void TrySumLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("TrySumLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: TrySum
    }
}
