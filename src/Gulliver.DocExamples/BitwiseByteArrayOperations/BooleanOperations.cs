using System;

namespace Gulliver.DocExamples.BitwiseByteArrayOperations
{
    /// <summary>
    ///     Boolean Operation examples
    /// </summary>
    public static class BooleanOperations
    {
        public static void BitwiseNotExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("BitwiseNot Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #region BitwiseAnd

        public static void BitwiseAndBigEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("BitwiseAndBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        public static void BitwiseAndLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("BitwiseAndLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: BitwiseAnd

        #region BitwiseOr

        public static void BitwiseOrBigEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("BitwiseOrBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        public static void BitwiseOrLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("BitwiseOrLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: BitwiseOr

        #region BitwiseXorB

        public static void BitwiseXorBigEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("BitwiseXorBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        public static void BitwiseXorLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0x00};
            var rhs = new byte[] {0x00};

            // Act
            var result = rhs;

            // Conclusion
            Console.WriteLine("BitwiseXorLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: BitwiseXorB
    }
}
