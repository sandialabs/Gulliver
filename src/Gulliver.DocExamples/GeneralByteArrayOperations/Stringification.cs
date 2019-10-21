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
            var input = new byte[] { 0xC0, 0xFF, 0xEE, 0xC0, 0xDE };

            // Conclusion
            Console.WriteLine("Stringification Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");

            Console.WriteLine("Hexadecimal Formats");
            Console.WriteLine($"H:\t\"{input.ToString("H")}\"");
            Console.WriteLine($"h:\t\"{input.ToString("h")}\"");
            Console.WriteLine($"HC:\t\"{input.ToString("HC")}\"");
            Console.WriteLine($"hc:\t\"{input.ToString("hc")}\"");

            Console.WriteLine("Binary Formats");
            Console.WriteLine($"b:\t\"{input.ToString("b")}\"");
            Console.WriteLine($"bc:\t\"{input.ToString("bc")}\"");

            Console.WriteLine("Integer Formats");
            Console.WriteLine($"d:\t\"{input.ToString("d")}\"");
            Console.WriteLine($"IBE:\t\"{input.ToString("IBE")}\"");
            Console.WriteLine($"ILE:\t\"{input.ToString("ILE")}\"");

            Console.WriteLine(string.Empty);
        }

        #endregion end: Stringification
    }
}
