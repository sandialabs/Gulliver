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
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("BigEndianEffectiveLength Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: BigEndianEffectiveLength

        #region LittleEndianEffectiveLength

        public static void LittleEndianEffectiveLengthExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("LittleEndianEffectiveLength Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: LittleEndianEffectiveLength

        #endregion end: Effective Length
    }
}
