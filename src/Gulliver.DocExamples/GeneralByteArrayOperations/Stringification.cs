using System;

namespace Gulliver.DocExamples.GeneralByteArrayOperations
{
    /// <summary>
    ///     Stringification examples
    /// </summary>
    public static class Stringification
    {
        #region Stringification

        public static void StringificationExample()
        {
            // Setup
            var input = new byte[] {0x00};

            // Act
            var result = input;

            // Conclusion
            Console.WriteLine("Stringification Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"result:\t{result.ToString("H")}");
        }

        #endregion end: Stringification
    }
}
