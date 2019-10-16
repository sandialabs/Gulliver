using Gulliver.DocExamples.BitwiseByteArrayOperations;
using Gulliver.DocExamples.GeneralByteArrayOperations;
using Gulliver.DocExamples.UnsignedMathematicalOperations;

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
            // General Byte Array Operations
            ByteArrayCreationAndPopulation.CreateByteArrayExample();

            ByteArrayMutation.TrimBigEndianLeadingZeroBytesExample();
            ByteArrayMutation.TrimLittleEndianLeadingZeroBytesExample();

            ByteArrayMutation.PadBigEndianMostSignificantBytesExample();
            ByteArrayMutation.PadLittleEndianMostSignificantBytesExample();

            ByteArrayMutation.AppendBytesExample();
            ByteArrayMutation.AppendShortestExample();

            ByteArrayMutation.PrependBytesExample();
            ByteArrayMutation.PrependShortestExample();

            Stringification.StringificationExample();

            Miscellaneous.BigEndianEffectiveLengthExample();
            Miscellaneous.LittleEndianEffectiveLengthExample();

            // Bitwise Byte Array Operations
            Addressing.AddressBitExample();

            BooleanOperations.BitwiseNotExample();

            BooleanOperations.BitwiseAndBigEndianExample();
            BooleanOperations.BitwiseAndLittleEndianExample();

            BooleanOperations.BitwiseOrBigEndianExample();
            BooleanOperations.BitwiseOrLittleEndianExample();

            BooleanOperations.BitwiseXorBigEndianExample();
            BooleanOperations.BitwiseXorLittleEndianExample();

            Bitshifting.ShiftBitsRightExample();
            Bitshifting.ShiftBitsRightCarryExample();

            Bitshifting.ShiftBitsLeftExample();
            Bitshifting.ShiftBitsLeftCarryExample();

            // Unsigned Mathematical Operations
            Addition.AddUnsignedBigEndianExample();
            Addition.AddUnsignedLittleEndianExample();

            Addition.TrySumBigEndianExample();
            Addition.TrySumLittleEndianExample();

            Subtraction.SubtractUnsignedBigEndianExample();
            Subtraction.SubtractUnsignedLittleEndianExample();

            Comparison.CompareUnsignedBigEndianExample();
            Comparison.CompareUnsignedLittleEndianExample();
        }
    }
}
