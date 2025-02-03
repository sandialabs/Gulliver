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
    public partial class ByteArrayUtilsTests
    {
        #region Arithmatic

        #region TrySumBigEndian

        public static IEnumerable<object[]> TrySumBigEndian_Test_Values()
        {
            var deltas = new[] { long.MinValue, -255, -129, -127, -1, 0, 1, 127, 129, 255, long.MaxValue };
            var uints = new uint[] { uint.MinValue, 1, 127, 129, 255, uint.MaxValue };

            foreach (var delta in deltas)
            {
                foreach (var input in uints)
                {
                    var inputBytes = BitConverter.IsLittleEndian
                        ? BitConverter.GetBytes(input).ReverseBytes()
                        : BitConverter.GetBytes(input);

                    var deltaBytes = BitConverter.IsLittleEndian
                        ? BitConverter.GetBytes(delta).ReverseBytes()
                        : BitConverter.GetBytes(delta);

                    if (delta < 0 && input - delta < 0)
                    {
                        yield return new object[] { false, Array.Empty<byte>(), inputBytes, delta };
                        yield break;
                    }

                    var expectedBytes =
                        delta < 0
                            ? ByteArrayUtils.SubtractUnsignedBigEndian(inputBytes, deltaBytes)
                            : ByteArrayUtils.AddUnsignedBigEndian(inputBytes, deltaBytes);

                    yield return new object[] { true, expectedBytes, inputBytes, delta };
                }
            }
        }

        [Theory]
        [MemberData(nameof(TrySumBigEndian_Test_Values))]
        public void TrySumBigEndian_Long_Test(bool expectedSuccess, byte[] expectedBytes, byte[] input, long delta)
        {
            // Arrange

            // Act
            var success = ByteArrayUtils.TrySumBigEndian(input, delta, out var result);

            // Assert
            Assert.Equal(expectedSuccess, success);
            Assert.Equal(expectedBytes, result);
        }

        [Fact]
        public void TrySumBigEndian_NullSource_Throws_ArgumentNullException_Test()
        {
            // Arrange
            // Act
            // Assert

            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.TrySumBigEndian(null, 42, out _));
        }

        #endregion // end: TrySumBigEndian

        #region AddUnsignedBigEndian

        public static IEnumerable<object[]> AddUnsignedBigEndian_Test_Values()
        {
            return TestCases();

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

                    if (systemIsLittleEndian)
                    {
                        additionResult = additionResult.ReverseBytes();
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    var expected = additionResult.TrimBigEndianLeadingZeroBytes();
                    var leftBytesTrimmed = leftBytes.TrimBigEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimBigEndianLeadingZeroBytes();

                    yield return new object[] { expected, leftBytes, rightBytes };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytes };
                    yield return new object[] { expected, leftBytes, rightBytesTrimmed };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytesTrimmed };
                }
            }
        }

        [Theory]
        [MemberData(nameof(AddUnsignedBigEndian_Test_Values))]
        public void AddUnsignedBigEndian_Test(byte[] expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.AddUnsignedBigEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void AddUnsignedBigEndian_NullInput_ThrowsArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.AddUnsignedBigEndian(left, right));
        }

        #endregion

        #region SubtractUnsignedBigEndian

        public static IEnumerable<object[]> SubtractUnsignedBigEndian_Test_Values()
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

                    if (systemIsLittleEndian)
                    {
                        differenceResult = differenceResult.ReverseBytes();
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    if (ByteArrayUtils.CompareUnsignedBigEndian(leftBytes, rightBytes) == -1) // invalid test, skip
                    {
                        continue;
                    }

                    var expected = differenceResult.TrimBigEndianLeadingZeroBytes();
                    var leftBytesTrimmed = leftBytes.TrimBigEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimBigEndianLeadingZeroBytes();

                    yield return new object[] { expected, leftBytes, rightBytes };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytes };
                    yield return new object[] { expected, leftBytes, rightBytesTrimmed };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytesTrimmed };
                }
            }
        }

        [Theory]
        [MemberData(nameof(SubtractUnsignedBigEndian_Test_Values))]
        public void SubtractUnsignedBigEndian_Test(byte[] expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.SubtractUnsignedBigEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void SubtractUnsignedBigEndian_NullInput_Throws_ArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.SubtractUnsignedBigEndian(left, right));
        }

        [Fact]
        public void SubtractUnsignedBigEndian_SignedOperation_Throws_InvalidOperationException_Test()
        {
            // Arrange
            var left = new byte[] { 0x00 };
            var right = new byte[] { 0x00, 0x0F };

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => ByteArrayUtils.SubtractUnsignedBigEndian(left, right));
        }

        #endregion

        #endregion

        #region CompareUnsignedBigEndian

        public static IEnumerable<object[]> CompareUnsignedBigEndian_Test_Values()
        {
            return TestCases().Distinct();

            IEnumerable<object[]> TestCases()
            {
                var systemIsLittleEndian = BitConverter.IsLittleEndian;

                foreach (var (left, right) in GetUintPairValues())
                {
                    var leftBytes = left != null ? BitConverter.GetBytes(left.Value) : Array.Empty<byte>();
                    var rightBytes = right != null ? BitConverter.GetBytes(right.Value) : Array.Empty<byte>();

                    if (systemIsLittleEndian)
                    {
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    var leftBytesTrimmed = leftBytes.TrimBigEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimBigEndianLeadingZeroBytes();

                    var expected = (left ?? 0).CompareTo(right ?? 0);

                    yield return new object[] { expected, leftBytes, rightBytes };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytes };
                    yield return new object[] { expected, leftBytes, rightBytesTrimmed };
                    yield return new object[] { expected, leftBytesTrimmed, rightBytesTrimmed };
                }
            }
        }

        [Theory]
        [MemberData(nameof(CompareUnsignedBigEndian_Test_Values))]
        public void CompareUnsignedBigEndian_Test(int expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.CompareUnsignedBigEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void CompareUnsignedBigEndian_NullInput_ThrowsArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.CompareUnsignedBigEndian(left, right));
        }

        #endregion

        #region BigEndianEffectiveLength

        [Theory]
        [InlineData(0, new byte[] { })]
        [InlineData(1, new byte[] { 0xff })]
        [InlineData(1, new byte[] { 0x00, 0xff })]
        [InlineData(2, new byte[] { 0x00, 0xff, 0xff })]
        [InlineData(2, new byte[] { 0xff, 0xff })]
        [InlineData(3, new byte[] { 0xff, 0xff, 0x00 })]
        [InlineData(3, new byte[] { 0x00, 0xff, 0xff, 0x00 })]
        public void BigEndianEffectiveLength_Test(int expected, byte[] input)
        {
            // Arrange

            // Act
            var result = input.BigEndianEffectiveLength();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BigEndianEffectiveLength_NullInput_ThrowsArgumentNullException_Test()
        {
            // Arrange
            // Act
            // Assert

            Assert.Throws<ArgumentNullException>(() => ((byte[])null).BigEndianEffectiveLength());
        }

        #endregion

        #region Bitwise

        #region BitwiseAndBigEndian

        public static IEnumerable<object[]> BitwiseAndBigEndian_Test_Values()
        {
            return TestCases().ToList().Distinct();

            IEnumerable<object[]> TestCases()
            {
                var systemIsLittleEndian = BitConverter.IsLittleEndian;

                foreach (var (left, right) in GetUintPairValues())
                {
                    var operationResult = BitConverter.GetBytes((left ?? 0) & (right ?? 0));

                    var leftBytes = left != null ? BitConverter.GetBytes(left.Value) : Array.Empty<byte>();
                    var rightBytes = right != null ? BitConverter.GetBytes(right.Value) : Array.Empty<byte>();

                    if (systemIsLittleEndian)
                    {
                        operationResult = operationResult.ReverseBytes();
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    var leftBytesTrimmed = leftBytes.TrimBigEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimBigEndianLeadingZeroBytes();

                    yield return new object[]
                    {
                        operationResult.Reverse().Take(Math.Max(leftBytes.Length, rightBytes.Length)).Reverse().ToArray(),
                        leftBytes,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        operationResult
                            .Reverse()
                            .Take(Math.Max(leftBytesTrimmed.Length, rightBytes.Length))
                            .Reverse()
                            .ToArray(),
                        leftBytesTrimmed,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        operationResult
                            .Reverse()
                            .Take(Math.Max(leftBytes.Length, rightBytesTrimmed.Length))
                            .Reverse()
                            .ToArray(),
                        leftBytes,
                        rightBytesTrimmed,
                    };

                    yield return new object[]
                    {
                        operationResult
                            .Reverse()
                            .Take(Math.Max(leftBytesTrimmed.Length, rightBytesTrimmed.Length))
                            .Reverse()
                            .ToArray(),
                        leftBytesTrimmed,
                        rightBytesTrimmed,
                    };
                }
            }
        }

        [Theory]
        [MemberData(nameof(BitwiseAndBigEndian_Test_Values))]
        public void BitwiseAndBigEndian_Test(byte[] expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.BitwiseAndBigEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void BitwiseAndBigEndian_NullInput_ThrowsArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.BitwiseAndBigEndian(left, right));
        }

        #endregion

        #region BitwiseOrBigEndian

        public static IEnumerable<object[]> BitwiseOrBigEndian_Test_Values()
        {
            return TestCases().ToList().Distinct();

            IEnumerable<object[]> TestCases()
            {
                var systemIsLittleEndian = BitConverter.IsLittleEndian;

                foreach (var (left, right) in GetUintPairValues())
                {
                    var operationResult = BitConverter.GetBytes((left ?? 0) | (right ?? 0));

                    var leftBytes = left != null ? BitConverter.GetBytes(left.Value) : Array.Empty<byte>();
                    var rightBytes = right != null ? BitConverter.GetBytes(right.Value) : Array.Empty<byte>();

                    if (systemIsLittleEndian)
                    {
                        operationResult = operationResult.ReverseBytes();
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    var leftBytesTrimmed = leftBytes.TrimBigEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimBigEndianLeadingZeroBytes();

                    yield return new object[]
                    {
                        operationResult.Reverse().Take(Math.Max(leftBytes.Length, rightBytes.Length)).Reverse().ToArray(),
                        leftBytes,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        operationResult
                            .Reverse()
                            .Take(Math.Max(leftBytesTrimmed.Length, rightBytes.Length))
                            .Reverse()
                            .ToArray(),
                        leftBytesTrimmed,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        operationResult
                            .Reverse()
                            .Take(Math.Max(leftBytes.Length, rightBytesTrimmed.Length))
                            .Reverse()
                            .ToArray(),
                        leftBytes,
                        rightBytesTrimmed,
                    };

                    yield return new object[]
                    {
                        operationResult
                            .Reverse()
                            .Take(Math.Max(leftBytesTrimmed.Length, rightBytesTrimmed.Length))
                            .Reverse()
                            .ToArray(),
                        leftBytesTrimmed,
                        rightBytesTrimmed,
                    };
                }
            }
        }

        [Theory]
        [MemberData(nameof(BitwiseOrBigEndian_Test_Values))]
        public void BitwiseOrBigEndian_Test(byte[] expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.BitwiseOrBigEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void BitwiseOrBigEndian_NullInput_ThrowsArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.BitwiseOrBigEndian(left, right));
        }

        #endregion

        #region BitwiseXorBigEndian

        public static IEnumerable<object[]> BitwiseXorBigEndian_Test_Values()
        {
            return TestCases().ToList().Distinct();

            IEnumerable<object[]> TestCases()
            {
                var systemIsLittleEndian = BitConverter.IsLittleEndian;

                foreach (var (left, right) in GetUintPairValues())
                {
                    var operationResult = BitConverter.GetBytes((left ?? 0) ^ (right ?? 0));

                    var leftBytes = left != null ? BitConverter.GetBytes(left.Value) : Array.Empty<byte>();
                    var rightBytes = right != null ? BitConverter.GetBytes(right.Value) : Array.Empty<byte>();

                    if (systemIsLittleEndian)
                    {
                        operationResult = operationResult.ReverseBytes();
                        leftBytes = leftBytes.ReverseBytes();
                        rightBytes = rightBytes.ReverseBytes();
                    }

                    var leftBytesTrimmed = leftBytes.TrimBigEndianLeadingZeroBytes();
                    var rightBytesTrimmed = rightBytes.TrimBigEndianLeadingZeroBytes();

                    yield return new object[]
                    {
                        operationResult.Reverse().Take(Math.Max(leftBytes.Length, rightBytes.Length)).Reverse().ToArray(),
                        leftBytes,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        operationResult
                            .Reverse()
                            .Take(Math.Max(leftBytesTrimmed.Length, rightBytes.Length))
                            .Reverse()
                            .ToArray(),
                        leftBytesTrimmed,
                        rightBytes,
                    };

                    yield return new object[]
                    {
                        operationResult
                            .Reverse()
                            .Take(Math.Max(leftBytes.Length, rightBytesTrimmed.Length))
                            .Reverse()
                            .ToArray(),
                        leftBytes,
                        rightBytesTrimmed,
                    };

                    yield return new object[]
                    {
                        operationResult
                            .Reverse()
                            .Take(Math.Max(leftBytesTrimmed.Length, rightBytesTrimmed.Length))
                            .Reverse()
                            .ToArray(),
                        leftBytesTrimmed,
                        rightBytesTrimmed,
                    };
                }
            }
        }

        [Theory]
        [MemberData(nameof(BitwiseXorBigEndian_Test_Values))]
        public void BitwiseXorBigEndian_Test(byte[] expected, byte[] left, byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            // Act
            var result = ByteArrayUtils.BitwiseXorBigEndian(left, right);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(leftOriginal, left); // input value not affected
            Assert.Equal(rightOriginal, right); // input value not affected
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void BitwiseXorBigEndian_NullInput_ThrowsArgumentNullException_Test(byte[] left, byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.BitwiseXorBigEndian(left, right));
        }

        #endregion

        #endregion

        #region TrimBigEndianLeadingZeros

        public static IEnumerable<object[]> TrimBigEndianLeadingZeros_Test_Values()
        {
            // trimmed to empty
            for (var i = 0; i < 5; i++)
            {
                var expected = Array.Empty<byte>();

                var input = Enumerable.Range(0, i).Select(_ => (byte)0x00).Concat(expected.ToList()).ToArray();

                yield return new object[] { expected, input };
            }

            // trim left
            for (var i = 0; i < 5; i++)
            {
                var expected = new byte[] { 0x0A, 0xF0 };

                var input = Enumerable.Range(0, i).Select(_ => (byte)0x00).Concat(expected.ToList()).ToArray();

                yield return new object[] { expected, input };
            }

            // don't trim right
            for (var i = 0; i < 5; i++)
            {
                var expected = new byte[] { 0x0A, 0xF0, 0x00 };

                var input = Enumerable.Range(0, i).Select(_ => (byte)0x00).Concat(expected.ToList()).ToArray();

                yield return new object[] { expected, input };
            }
        }

        [Theory]
        [MemberData(nameof(TrimBigEndianLeadingZeros_Test_Values))]
        public void TrimBigEndianLeadingZeros_Test(byte[] expected, byte[] input)
        {
            // Arrange
            var original = input.ToArray();

            // Act
            var result = input.TrimBigEndianLeadingZeroBytes();

            // Assert
            Assert.Equal(expected, result);
            Assert.Equal(original, input);
        }

        [Fact]
        public void TrimBigEndianLeadingZeros_NullInput_ThrowsArgumentNullException_Test()
        {
            Assert.Throws<ArgumentNullException>(() => ((byte[])null).TrimBigEndianLeadingZeroBytes());
        }

        #endregion

        #region PadBigEndianMostSignificantBytes

        public static IEnumerable<object[]> PadBigEndianMostSignificantBytes_Test_Vales()
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
            yield return new object[] { new byte[] { 0xfc, 0xfc, 0x00, 0x00 }, new byte[] { 0x00, 0x00 }, 4, 0xfc };
            yield return new object[] { new byte[] { 0xfc, 0xfc, 0xac, 0xac }, new byte[] { 0xac, 0xac }, 4, 0xfc };
        }

        [Theory]
        [MemberData(nameof(PadBigEndianMostSignificantBytes_Test_Vales))]
        public void PadBigEndianMostSignificantBytes_Test(byte[] expected, byte[] input, int finalLength, byte element)
        {
            // Arrange
            // Act
            var result = input.PadBigEndianMostSignificantBytes(finalLength, element);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Math.Max(input.Length, finalLength), result.Length);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PadBigEndianMostSignificantBytes_NullSource_Throws_ArgumentNullException_Test()
        {
            // Arrange
            // Act
            // Assert

            Assert.Throws<ArgumentNullException>(() => ((byte[])null).PadBigEndianMostSignificantBytes(42));
        }

        [Fact]
        public void PadBigEndianMostSignificantBytes_FinalLength_LessThanZero_Throws_ArgumentOutOfRangeException_Test()
        {
            // Arrange
            // Act
            // Assert

            Assert.Throws<ArgumentOutOfRangeException>(() => new byte[1].PadBigEndianMostSignificantBytes(-1));
        }

        #endregion // end: PadBigEndianMostSignificantBytes
    }
}
