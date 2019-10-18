using System;

namespace Gulliver.DocExamples.GeneralByteArrayOperations
{
    /// <summary>
    ///     Miscellaneous Examples
    /// </summary>
    public static class Miscellaneous
    {
        #region Effective Length

        #region BigEndianEffectiveLength

        public static void BigEndianEffectiveLengthExample()
        {
            // Setup
            var input = new byte[] {0x00, 0x00, 0x00, 0xDA, 0xBD, 0xAD};

            // Act
            var result = input.BigEndianEffectiveLength();

            // Conclusion
            Console.WriteLine("BigEndianEffectiveLength Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: BigEndianEffectiveLength

        #region LittleEndianEffectiveLength

        public static void LittleEndianEffectiveLengthExample()
        {
            // Setup
            var input = new byte[] {0xDA, 0xB0, 0x00, 0x00, 0x00, 0x00};

            // Act
            var result = input.LittleEndianEffectiveLength();

            // Conclusion
            Console.WriteLine("LittleEndianEffectiveLength Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: LittleEndianEffectiveLength

        #endregion end: Effective Length
    }
}
