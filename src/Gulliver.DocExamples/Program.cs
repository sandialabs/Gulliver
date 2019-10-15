using System;
using Gulliver.DocExamples.GeneralByteArrayOperations;

namespace Gulliver.DocExamples
{
    /// <summary>
    ///     Entry point for Gulliver Documentation Examples
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     PSVM
        /// </summary>
        public static void Main()
        {
            // ByteArrayCreationAndPopulation
            ByteArrayCreationAndPopulation.CreateByteArrayExample();

            ByteArrayCreationAndPopulation.TrimBigEndianLeadingZeroBytes();
            ByteArrayCreationAndPopulation.TrimLittleEndianLeadingZeroBytes();
        }
    }
}
