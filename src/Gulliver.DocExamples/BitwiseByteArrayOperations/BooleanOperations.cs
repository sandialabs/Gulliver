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
            var input = new byte[] {0x00, 0x11, 0xAC, 0xFF};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("BitwiseNot Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"input:\t{input.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        #region BitwiseAnd

        public static void BitwiseAndBigEndianExample()
        {
            // Setup
            var lhs = new byte[] {0xC0, 0xDE};
            var rhs = new byte[] {0xC0, 0xFF, 0xEE};

            // Act
            var result = ByteArrayUtils.BitwiseAndBigEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("BitwiseAndBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        public static void BitwiseAndLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0xC0, 0xDE};
            var rhs = new byte[] {0xC0, 0xFF, 0xEE};

            // Act
            var result = ByteArrayUtils.BitwiseAndLittleEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("BitwiseAndLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: BitwiseAnd

        #region BitwiseOr

        public static void BitwiseOrBigEndianExample()
        {
            // Setup
            var lhs = new byte[] {0xC0, 0xDE};
            var rhs = new byte[] {0xC0, 0xFF, 0xEE};

            // Act
            var result = ByteArrayUtils.BitwiseOrBigEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("BitwiseOrBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        public static void BitwiseOrLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0xC0, 0xDE};
            var rhs = new byte[] {0xC0, 0xFF, 0xEE};

            // Act
            var result = ByteArrayUtils.BitwiseOrLittleEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("BitwiseOrLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: BitwiseOr

        #region BitwiseXorB

        public static void BitwiseXorBigEndianExample()
        {
            // Setup
            var lhs = new byte[] {0xC0, 0xDE};
            var rhs = new byte[] {0xC0, 0xFF, 0xEE};

            // Act
            var result = ByteArrayUtils.BitwiseXorBigEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("BitwiseXorBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        public static void BitwiseXorLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0xC0, 0xDE};
            var rhs = new byte[] {0xC0, 0xFF, 0xEE};

            // Act
            var result = ByteArrayUtils.BitwiseXorLittleEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("BitwiseXorLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
            Console.WriteLine($"result:\t{result.ToString("b")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: BitwiseXorB
    }
}
