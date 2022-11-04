using System;
using System.Collections.Generic;
using System.Linq;

namespace Gulliver.DocExamples.BitwiseByteArrayOperations
{
    /// <summary>
    ///     Addressing examples
    /// </summary>
    public static class Addressing
    {
        #region AddressBit

        public static void AddressBitExample()
        {
            // Setup
            var input = new byte[] { 0xC0, 0x1D };
            var bitLength = input.Length * 8;

            // Act
            IEnumerable<string> result = Enumerable
                .Range(0, bitLength - 1)
                .Select(i =>
                {
                    var bit = input.AddressBit(i);
                    return (i, b: bit ? 1 : 0);
                })
                .Select(x => $"[{x.i}]:{x.b}")
                .Skip(4)
                .Take(bitLength - 8)
                .ToList();

            // Conclusion
            Console.WriteLine("AddressBit Example");
            Console.WriteLine($"input:\t{input.ToString("H")}");
            Console.WriteLine($"input:\t{input.ToString("b")}");
            Console.WriteLine($"result:\t{string.Join(", ", result)}");
            Console.WriteLine(string.Empty);
        }

        #endregion end: AddressBit
    }
}
