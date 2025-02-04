using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Gulliver.Tests
{
#if NET6_0_OR_GREATER
#pragma warning disable IDE0062 // Make local function static (IDE0062); purposely allowing non-static functions that could be static for .net4.8 compatibility
#endif

    /// <summary>
    ///     Little Endian ByteArrayUtils Tests
    /// </summary>
    public partial class ByteArrayUtilsTests
    {
        #region Arithmatic

        #region TrySumLittleEndian

        public static IEnumerable<object[]> TrySumLittleEndian_Test_Values()
        {
            var deltas = new[] { long.MinValue, -255, -129, -127, -1, 0, 1, 127, 129, 255, long.MaxValue };
            var uints = new uint[] { uint.MinValue, 1, 127, 129, 255, uint.MaxValue };

            foreach (var delta in deltas)
            {
                foreach (var input in uints)
                {
                    var inputBytes = !BitConverter.IsLittleEndian
                        ? BitConverter.GetBytes(input).ReverseBytes()
                        : BitConverter.GetBytes(input);

                    var deltaBytes = !BitConverter.IsLittleEndian
                        ? BitConverter.GetBytes(delta).ReverseBytes()
                        : BitConverter.GetBytes(delta);

                    if (delta < 0 && input - delta < 0)
                    {
                        yield return new object[] { false, Array.Empty<byte>(), inputBytes, delta };
                        yield break;
                    }

                    var expectedBytes =
                        delta < 0
                            ? ByteArrayUtils.SubtractUnsignedLittleEndian(inputBytes, deltaBytes)
                            : ByteArrayUtils.AddUnsignedLittleEndian(inputBytes, deltaBytes);

                    yield return new object[] { true, expectedBytes, inputBytes, delta };
                }
            }
        }

        [Theory]
        [MemberData(nameof(TrySumLittleEndian_Test_Values))]
        public void TrySumLittleEndian_Long_Test(bool expectedSuccess, byte[] expectedBytes, byte[] input, long delta)
        {
            // Arrange

            // Act
            var success = ByteArrayUtils.TrySumLittleEndian(input, delta, out var result);

            // Assert
            Assert.Equal(expectedSuccess, success);
            Assert.Equal(expectedBytes, result);
        }

        [Fact]
        public void TrySumLittleEndian_NullSource_Throws_ArgumentNullException_Test()
        {
            // Arrange
            // Act
            // Assert

            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.TrySumLittleEndian(null, 42, out _));
        }

        #endregion // end: TrySumLittleEndian

        #region AddUnsignedLittleEndian

        public static IEnumerable<object[]> AddUnsignedLittleEndian_Test_Values()
        {
            return TestCases().Distinct();

            IEnumerable<object[]> TestCases()
            {
                var systemIsLittleEndian = BitConverter.IsLittleEndian;

                foreach (var (left, right) in GetUintPairValues())
                {
                    var additionResult = BitConverter.GetBytes(
                        Convert.ToUInt64(left, CultureInfo.InvariantCulture)
                            + Convert.ToUInt64(right, CultureInfo.InvariantCulture)
                    );

                    var leftBytes = left != null ? BitConverter.GetBytes(left.Value) : Array.Empty<byte>();
                    var rightBytes = right != null ? BitConverter.GetBytes(right.Value) : Array.Empty<byte>();

                    if (!systemIsLittleEndian)
                    {
                        additionResult = additionResult.ReverseBytes();
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    var expected = additionResult.TrimLittleEndianLeadingZeroBytes();
                    var leftBytesTrimmed = leftBytes.TrimLittleEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimLittleEndianLeadingZeroBytes();

                    yield return new object[] { expected, leftBytes, rightBytes };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytes };
                    yield return new object[] { expected, leftBytes, rightBytesTrimmed };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytesTrimmed };
                }
            }
        }

        [Theory]
        [MemberData(nameof(AddUnsignedLittleEndian_Test_Values))]
        public void AddUnsignedLittleEndian_Test(byte[] expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.AddUnsignedLittleEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void AddUnsignedLittleEndian_NullInput_ThrowsArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.AddUnsignedLittleEndian(left, right));
        }

        #endregion

        #region SubtractUnsignedLittleEndian

        public static IEnumerable<object[]> SubtractUnsignedLittleEndian_Test_Values()
        {
            return TestCases().Distinct();

            IEnumerable<object[]> TestCases()
            {
                var systemIsLittleEndian = BitConverter.IsLittleEndian;

                foreach (var (left, right) in GetUintPairValues())
                {
                    var differenceResult = BitConverter.GetBytes(
                        Convert.ToUInt64(left, CultureInfo.InvariantCulture)
                            - Convert.ToUInt64(right, CultureInfo.InvariantCulture)
                    );

                    var leftBytes = left != null ? BitConverter.GetBytes(left.Value) : Array.Empty<byte>();
                    var rightBytes = right != null ? BitConverter.GetBytes(right.Value) : Array.Empty<byte>();

                    if (!systemIsLittleEndian)
                    {
                        differenceResult = differenceResult.ReverseBytes();
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    if (ByteArrayUtils.CompareUnsignedLittleEndian(leftBytes, rightBytes) == -1) // invalid test, skip
                    {
                        continue;
                    }

                    var expected = differenceResult.TrimLittleEndianLeadingZeroBytes();
                    var leftBytesTrimmed = leftBytes.TrimLittleEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimLittleEndianLeadingZeroBytes();

                    yield return new object[] { expected, leftBytes, rightBytes };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytes };
                    yield return new object[] { expected, leftBytes, rightBytesTrimmed };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytesTrimmed };
                }
            }
        }

        [Theory]
        [MemberData(nameof(SubtractUnsignedLittleEndian_Test_Values))]
        public void SubtractUnsignedLittleEndian_Test(byte[] expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.SubtractUnsignedLittleEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void SubtractUnsignedLittleEndian_NullInput_Throws_ArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.SubtractUnsignedLittleEndian(left, right));
        }

        [Fact]
        public void SubtractUnsignedLittleEndian_SignedOperation_Throws_InvalidOperationException_Test()
        {
            // Arrange
            var left = new byte[] { 0x00 };
            var right = new byte[] { 0x00, 0x0F };

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => ByteArrayUtils.SubtractUnsignedLittleEndian(left, right));
        }

        #endregion

        #endregion

        #region CompareUnsignedLittleEndian

        public static IEnumerable<object[]> CompareUnsignedLittleEndian_Test_Values()
        {
            return TestCases().Distinct();

            IEnumerable<object[]> TestCases()
            {
                var systemIsLittleEndian = BitConverter.IsLittleEndian;

                foreach (var (left, right) in GetUintPairValues())
                {
                    var leftBytes = left != null ? BitConverter.GetBytes(left.Value) : Array.Empty<byte>();
                    var rightBytes = right != null ? BitConverter.GetBytes(right.Value) : Array.Empty<byte>();

                    if (!systemIsLittleEndian)
                    {
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    var leftBytesTrimmed = leftBytes.TrimLittleEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimLittleEndianLeadingZeroBytes();

                    var expected = (left ?? 0).CompareTo(right ?? 0);

                    yield return new object[] { expected, leftBytes, rightBytes };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytes };
                    yield return new object[] { expected, leftBytes, rightBytesTrimmed };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytesTrimmed };
                }
            }
        }

        [Theory]
        [MemberData(nameof(CompareUnsignedLittleEndian_Test_Values))]
        public void CompareUnsignedLittleEndian_Test(int expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.CompareUnsignedLittleEndian(left, right);

            this._testOutputHelper.WriteLine("dec:");
            this._testOutputHelper.WriteLine($"expected:\t{left.ToString("ILE")} <=> {right.ToString("ILE")} = {expected}");
            this._testOutputHelper.WriteLine($"result:  \t{left.ToString("ILE")} <=> {right.ToString("ILE")} = {result}");

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void CompareUnsignedLittleEndian_NullInput_ThrowsArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.CompareUnsignedLittleEndian(left, right));
        }

        #endregion

        #region LittleEndianEffectiveLength

        [Theory]
        [InlineData(0, new byte[] { })]
        [InlineData(1, new byte[] { 0xff })]
        [InlineData(1, new byte[] { 0xff, 0x00 })]
        [InlineData(2, new byte[] { 0xff, 0xff, 0x00 })]
        [InlineData(2, new byte[] { 0xff, 0xff })]
        [InlineData(3, new byte[] { 0x00, 0xff, 0xff })]
        [InlineData(3, new byte[] { 0x00, 0xff, 0xff, 0x00 })]
        public void LittleEndianEffectiveLength_Test(int expected, byte[] input)
        {
            // Arrange

            // Act
            var result = input.LittleEndianEffectiveLength();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LittleEndianEffectiveLength_NullInput_ThrowsArgumentNullException_Test()
        {
            // Arrange
            // Act
            // Assert

            Assert.Throws<ArgumentNullException>(() => ((byte[])null).LittleEndianEffectiveLength());
        }

        #endregion

        #region Bitwise

        #region BitwiseAndLittleEndian

        public static IEnumerable<object[]> BitwiseAndLittleEndian_Test_Values()
        {
            return TestCases().Distinct();

            IEnumerable<object[]> TestCases()
            {
                var systemIsLittleEndian = BitConverter.IsLittleEndian;

                foreach (var (left, right) in GetUintPairValues())
                {
                    var operationResult = BitConverter.GetBytes((left ?? 0) & (right ?? 0));

                    var leftBytes = left != null ? BitConverter.GetBytes(left.Value) : Array.Empty<byte>();
                    var rightBytes = right != null ? BitConverter.GetBytes(right.Value) : Array.Empty<byte>();

                    if (!systemIsLittleEndian)
                    {
                        operationResult = operationResult.ReverseBytes();
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    var leftBytesTrimmed = leftBytes.TrimLittleEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimLittleEndianLeadingZeroBytes();

                    yield return new object[]
                    {
                        operationResult.Take(Math.Max(leftBytes.Length, rightBytes.Length)).ToArray(),
                        leftBytes,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        operationResult.Take(Math.Max(leftBytesTrimmed.Length, rightBytes.Length)).ToArray(),
                        leftBytesTrimmed,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        operationResult.Take(Math.Max(leftBytes.Length, rightBytesTrimmed.Length)).ToArray(),
                        leftBytes,
                        rightBytesTrimmed,
                    };

                    yield return new object[]
                    {
                        operationResult.Take(Math.Max(leftBytesTrimmed.Length, rightBytesTrimmed.Length)).ToArray(),
                        leftBytesTrimmed,
                        rightBytesTrimmed,
                    };
                }
            }
        }

        [Theory]
        [MemberData(nameof(BitwiseAndLittleEndian_Test_Values))]
        public void BitwiseAndLittleEndian_Test(byte[] expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.BitwiseAndLittleEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void BitwiseAndLittleEndian_NullInput_ThrowsArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.BitwiseAndLittleEndian(left, right));
        }

        #endregion

        #region BitwiseOrLittleEndian

        public static IEnumerable<object[]> BitwiseOrLittleEndian_Test_Values()
        {
            return TestCases().Distinct();

            IEnumerable<object[]> TestCases()
            {
                var systemIsLittleEndian = BitConverter.IsLittleEndian;

                foreach (var (left, right) in GetUintPairValues())
                {
                    var operationResult = BitConverter.GetBytes((left ?? 0) | (right ?? 0));

                    var leftBytes = left != null ? BitConverter.GetBytes(left.Value) : Array.Empty<byte>();
                    var rightBytes = right != null ? BitConverter.GetBytes(right.Value) : Array.Empty<byte>();

                    if (!systemIsLittleEndian)
                    {
                        operationResult = operationResult.ReverseBytes();
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    var leftBytesTrimmed = leftBytes.TrimLittleEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimLittleEndianLeadingZeroBytes();

                    yield return new object[]
                    {
                        operationResult.Take(Math.Max(leftBytes.Length, rightBytes.Length)).ToArray(),
                        leftBytes,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        operationResult.Take(Math.Max(leftBytesTrimmed.Length, rightBytes.Length)).ToArray(),
                        leftBytesTrimmed,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        operationResult.Take(Math.Max(leftBytes.Length, rightBytesTrimmed.Length)).ToArray(),
                        leftBytes,
                        rightBytesTrimmed,
                    };

                    yield return new object[]
                    {
                        operationResult.Take(Math.Max(leftBytesTrimmed.Length, rightBytesTrimmed.Length)).ToArray(),
                        leftBytesTrimmed,
                        rightBytesTrimmed,
                    };
                }
            }
        }

        [Theory]
        [MemberData(nameof(BitwiseOrLittleEndian_Test_Values))]
        public void BitwiseOrLittleEndian_Test(byte[] expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.BitwiseOrLittleEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void BitwiseOrLittleEndian_NullInput_ThrowsArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.BitwiseOrLittleEndian(left, right));
        }

        #endregion

        #region BitwiseXorLittleEndian

        public static IEnumerable<object[]> BitwiseXorLittleEndian_Test_Values()
        {
            return TestCases().ToList();

            IEnumerable<object[]> TestCases()
            {
                var systemIsLittleEndian = BitConverter.IsLittleEndian;

                foreach (var (left, right) in GetUintPairValues())
                {
                    var expected = BitConverter.GetBytes((left ?? 0) ^ (right ?? 0));

                    var leftBytes = left != null ? BitConverter.GetBytes(left.Value) : Array.Empty<byte>();

                    var rightBytes = right != null ? BitConverter.GetBytes(right.Value) : Array.Empty<byte>();

                    if (!systemIsLittleEndian)
                    {
                        expected = expected.ReverseBytes();
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    var leftBytesTrimmed = leftBytes.TrimLittleEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimLittleEndianLeadingZeroBytes();

                    yield return new object[]
                    {
                        expected.Take(Math.Max(leftBytes.Length, rightBytes.Length)).ToArray(),
                        leftBytes,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        expected.Take(Math.Max(leftBytesTrimmed.Length, rightBytes.Length)).ToArray(),
                        leftBytesTrimmed,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        expected.Take(Math.Max(leftBytes.Length, rightBytesTrimmed.Length)).ToArray(),
                        leftBytes,
                        rightBytesTrimmed,
                    };

                    yield return new object[]
                    {
                        expected.Take(Math.Max(leftBytesTrimmed.Length, rightBytesTrimmed.Length)).ToArray(),
                        leftBytesTrimmed,
                        rightBytesTrimmed,
                    };
                }
            }
        }

        [Theory]
        [MemberData(nameof(BitwiseXorLittleEndian_Test_Values))]
        public void BitwiseXorLittleEndian_Test(byte[] expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.BitwiseXorLittleEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void BitwiseXorLittleEndian_NullInput_ThrowsArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.BitwiseXorLittleEndian(left, right));
        }

        #endregion

        #endregion

        #region TrimLittleEndianLeadingZeros

        public static IEnumerable<object[]> TrimLittleEndianLeadingZeros_Test_Values()
        {
            // trimmed to empty
            for (var i = 0; i < 5; i++)
            {
                var expected = Array.Empty<byte>();

                var input = Enumerable.Range(0, i).Select(_ => (byte)0x00).Concat(expected.ToList()).ToArray();

                yield return new object[] { expected, input };
            }

            // don't trim left
            for (var i = 0; i < 5; i++)
            {
                var expected = new byte[] { 0x00, 0x0A, 0xF0 };

                var input = expected.Concat(Enumerable.Range(0, i).Select(_ => (byte)0x00)).ToArray();

                yield return new object[] { expected, input };
            }

            // trim right
            for (var i = 0; i < 5; i++)
            {
                var expected = new byte[] { 0x0A, 0xF0 };

                var input = expected.Concat(Enumerable.Range(0, i).Select(_ => (byte)0x00)).ToArray();

                yield return new object[] { expected, input };
            }
        }

        [Theory]
        [MemberData(nameof(TrimLittleEndianLeadingZeros_Test_Values))]
        public void TrimLittleEndianLeadingZeros_Test(byte[] expected, byte[] input)
        {
            // Arrange
            var original = input.ToArray();

            // Act
            var result = input.TrimLittleEndianLeadingZeroBytes();

            // Assert
            Assert.Equal(expected, result);
            Assert.Equal(original, input);
        }

        [Fact]
        public void TrimLittleEndianLeadingZeros_NullInput_ThrowsArgumentNullException_Test()
        {
            Assert.Throws<ArgumentNullException>(() => ((byte[])null).TrimLittleEndianLeadingZeroBytes());
        }

        #endregion

        #region PadBigEndianMostSignificantBytes

        public static IEnumerable<object[]> PadLittleEndianMostSignificantBytes_Test_Vales()
        {
            // no padding, not padded
            yield return new object[] { Array.Empty<byte>(), Array.Empty<byte>(), 0, 0x00 };
            yield return new object[] { new byte[] { 0xfc }, new byte[] { 0xfc }, 0, 0x00 };

            // input length equal to final length, not padded
            yield return new object[] { new byte[] { 0x00, 0x00 }, new byte[] { 0x00, 0x00 }, 2, 0x00 };
            yield return new object[] { new byte[] { 0x00, 0x00 }, new byte[] { 0x00, 0x00 }, 2, 0xfc };

            // input length greater than final length, not padded
            yield return new object[] { new byte[] { 0x00, 0x00 }, new byte[] { 0x00, 0x00 }, 1, 0x00 };
            yield return new object[] { new byte[] { 0xfc, 0xac }, new byte[] { 0xfc, 0xac }, 1, 0xfc };

            // empty input, padded
            yield return new object[] { new byte[] { 0x00, 0x00 }, Array.Empty<byte>(), 2, 0x00 };
            yield return new object[] { new byte[] { 0xfc, 0xfc }, Array.Empty<byte>(), 2, 0xfc };

            // input length less than final length, padded
            yield return new object[] { new byte[] { 0x00, 0x00, 0x00, 0x00 }, new byte[] { 0x00, 0x00 }, 4, 0x00 };
            yield return new object[] { new byte[] { 0x00, 0x00, 0xfc, 0xfc }, new byte[] { 0x00, 0x00 }, 4, 0xfc };
            yield return new object[] { new byte[] { 0xac, 0xac, 0xfc, 0xfc }, new byte[] { 0xac, 0xac }, 4, 0xfc };
        }

        [Theory]
        [MemberData(nameof(PadLittleEndianMostSignificantBytes_Test_Vales))]
        public void PadLittleEndianMostSignificantBytes_Test(byte[] expected, byte[] input, int finalLength, byte element)
        {
            // Arrange
            // Act
            var result = input.PadLittleEndianMostSignificantBytes(finalLength, element);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Math.Max(input.Length, finalLength), result.Length);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PadLittleEndianMostSignificantBytes_NullSource_Throws_ArgumentNullException_Test()
        {
            // Arrange
            // Act
            // Assert

            Assert.Throws<ArgumentNullException>(() => ((byte[])null).PadLittleEndianMostSignificantBytes(42));
        }

        [Fact]
        public void PadLittleEndianMostSignificantBytes_FinalLength_LessThanZero_Throws_ArgumentOutOfRangeException_Test()
        {
            // Arrange
            // Act
            // Assert

            Assert.Throws<ArgumentOutOfRangeException>(() => new byte[1].PadLittleEndianMostSignificantBytes(-1));
        }

        #endregion // end: PadBigEndianMostSignificantBytes
    }
}
