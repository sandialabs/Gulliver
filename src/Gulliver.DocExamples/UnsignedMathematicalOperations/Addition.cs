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
            var lhs = new byte[] {0xAD, 0xDE, 0xD0};
            var rhs = new byte[] {0xC0, 0xDE};

            // Act
            var result = ByteArrayUtils.AddUnsignedBigEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("AddUnsignedBigEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        public static void AddUnsignedLittleEndianExample()
        {
            // Setup
            var lhs = new byte[] {0xAD, 0xDE, 0xD0};
            var rhs = new byte[] {0xC0, 0xDE};

            // Act
            var result = ByteArrayUtils.AddUnsignedLittleEndian(lhs, rhs);

            // Conclusion
            Console.WriteLine("AddUnsignedLittleEndian Example");
            Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
            Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: AddUnsigned

        #region TrySum

        public static void TrySumBigEndianExample()
        {
            // Setup
            var bytes = new byte[] {0xAD, 0xDE, 0xD0};
            const long delta = 42L;

            // Act
            var success = ByteArrayUtils.TrySumBigEndian(bytes, delta, out var result);

            // Conclusion
            Console.WriteLine("TrySumBigEndian Example");
            Console.WriteLine($"bytes:\t{bytes.ToString("H")}");
            Console.WriteLine($"delta:\t{delta}");
            Console.WriteLine($"success:\t{success}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        public static void TrySumBigEndianSubtractionExample()
        {
            // Setup
            var bytes = new byte[] {0xAD, 0xDE, 0xD0};
            const long delta = -42L;

            // Act
            var success = ByteArrayUtils.TrySumBigEndian(bytes, delta, out var result);

            // Conclusion
            Console.WriteLine("TrySumBigEndian Example");
            Console.WriteLine($"bytes:\t{bytes.ToString("H")}");
            Console.WriteLine($"delta:\t{delta}");
            Console.WriteLine($"success:\t{success}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        public static void TrySumLittleEndianExample()
        {
            // Setup
            var bytes = new byte[] {0xAD, 0xDE, 0xD0};
            const long delta = 42L;

            // Act
            var success = ByteArrayUtils.TrySumLittleEndian(bytes, delta, out var result);

            // Conclusion
            Console.WriteLine("TrySumBigEndian Subtraction Example");
            Console.WriteLine($"bytes:\t{bytes.ToString("H")}");
            Console.WriteLine($"delta:\t{delta}");
            Console.WriteLine($"success:\t{success}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        public static void TrySumLittleEndianSubtractionExample()
        {
            // Setup
            var bytes = new byte[] {0xAD, 0xDE, 0xD0};
            const long delta = -42L;

            // Act
            var success = ByteArrayUtils.TrySumLittleEndian(bytes, delta, out var result);

            // Conclusion
            Console.WriteLine("TrySumBigEndian Subtraction Example");
            Console.WriteLine($"bytes:\t{bytes.ToString("H")}");
            Console.WriteLine($"delta:\t{delta}");
            Console.WriteLine($"success:\t{success}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: TrySum
    }
}
