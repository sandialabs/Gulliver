using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Gulliver.Tests
{
    // ReSharper disable once TestClassNameDoesNotMatchFileNameWarning

    /// <summary>
    ///     General ByteArrayUtils Tests
    /// </summary>
    [Trait("Category", "ByteArrayUtils")]
    public partial class ByteArrayUtilsTests
    {
        #region Setup / Teardown

        public ByteArrayUtilsTests(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }

        private readonly ITestOutputHelper _testOutputHelper;

        #endregion

        #region Test Data Generation

        private static uint?[] GetUintValues()
        {
            return Values()
                   .OrderBy(i => i)
                   .ToArray();

            IEnumerable<uint?> Values()
            {
                var specialValues = new uint?[]
                                    {
                                        null,
                                        1U << 7,
                                        1U << 31,
                                        1U << 32,
                                        0x0055,
                                        0x00AA,
                                        0x00AC,
                                        0x00CA,
                                        0x00FF,
                                        0x5500,
                                        0x5555,
                                        0xAA00,
                                        0xAAAA,
                                        0xAC00,
                                        0xACCA,
                                        0xCA00,
                                        0xCAAC,
                                        0xFFFF,
                                        uint.MaxValue
                                    };

                foreach (var value in specialValues)
                {
                    yield return value;
                }

                foreach (var i in Enumerable.Range(0, 5))
                {
                    yield return (uint)i;
                }
            }
        }

        private static IEnumerable<(uint?, uint?)> GetUintPairValues()
        {
            var values = GetUintValues();

            foreach (var left in values)
            {
                foreach (var right in values)
                {
                    yield return (left, right);
                }
            }
        }

        #endregion

        #region CreateByteArray

        [Theory]
        [InlineData(0, 0x00)]
        [InlineData(0, 0xFF)]
        [InlineData(0, 0xAC)]
        [InlineData(2, 0x00)]
        [InlineData(2, 0xFF)]
        [InlineData(2, 0xAC)]
        public void CreateByteArray_Test(int length,
                                         byte element)
        {
            // Arrange
            // Act
            var result = ByteArrayUtils.CreateByteArray(length, element);

            // Assert
            Assert.IsType<byte[]>(result);
            Assert.Equal(length, result.Length);
            Assert.All(result, b => b.Equals(element));
        }

        [Fact]
        public void CreateByteArray_LengthLessThan0_ThrowsArgumentOutOfRangeException_Test()
        {
            // Arrange
            const int length = -1;

            // Act
            // Arrange
            Assert.Throws<ArgumentOutOfRangeException>(() => ByteArrayUtils.CreateByteArray(length));
        }

        [Fact]
        public void CreateByteArray_DefaultsTo0x00_Test()
        {
            // Arrange
            // Act
            var result = ByteArrayUtils.CreateByteArray(16);

            // Assert
            Assert.All(result, b => b.Equals(0x00));
        }

        #endregion

        #region BitwiseNot

        public static IEnumerable<object[]> BitwiseNot_Test_Values()
        {
            return TestCases()
                .Distinct();

            IEnumerable<object[]> TestCases()
            {
                foreach (var u in GetUintValues())
                {
                    var expected = u != null
                                       ? BitConverter.GetBytes(~u.Value)
                                       : Array.Empty<byte>();

                    var bytes = u != null
                                    ? BitConverter.GetBytes(u.Value)
                                    : Array.Empty<byte>();

                    yield return new object[] { expected, bytes };
                }
            }
        }

        [Theory]
        [MemberData(nameof(BitwiseNot_Test_Values))]
        public void BitwiseNot_Test(byte[] expected,
                                    byte[] input)
        {
            // Arrange
            var original = input.ToArray();

            // Act
            var result = ByteArrayUtils.BitwiseNot(input);

            // Assert
            Assert.Equal(expected, result);

            Assert.Equal(original, input); // input value not affected
        }

        [Fact]
        public void BitwiseNot_NullInput_ThrowsArgumentNullException_Test()
        {
            // Arrange
            // Act
            // Assert
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.BitwiseNot(null));
        }

        #endregion

        #region GetBits

        #endregion

        #region ShiftBitsLeft

        #endregion

        #region ShiftBitsLeft carry

        #endregion

        #region ShiftBitsRight

        #endregion

        #region ShiftBitsRight carry

        #endregion

        #region GetBits

        #endregion

        #region AddressBit

        public static IEnumerable<object[]> AddressBit_Values()
        {
            for (var i = 0; i < 8; i++)
            {
                yield return new object[] { true, i, new byte[] { (byte)(0x01 << i), 0x00 } };
                yield return new object[] { true, i + 8, new byte[] { 0x00, (byte)(0x01 << i) } };
                yield return new object[] { false, i, new[] { (byte)~(0x01 << i) } };
            }
        }

        [Theory]
        [MemberData(nameof(AddressBit_Values))]
        public void AddressBit_Test(bool expected,
                                    int index,
                                    byte[] input)
        {
            // Arrange
            var original = input.ToArray();

            // Act
            var result = input.AddressBit(index);

            // Assert
            Assert.Equal(expected, result);
            Assert.Equal(original, input);
        }

        [Fact]
        public void AddressBit_NullInput_ThrowsArgumentNullException_Test()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ((byte[])null).AddressBit(default));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(8)]
        public void AddressBit_IndexOutOfRange_ThrowsArgumentOutOfRangeException_Test(int index)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentOutOfRangeException>(() => new byte[] { 0xff }.AddressBit(index));
        }

        #endregion

        #region ShiftBitsRight

        public static IEnumerable<object[]> ShiftBitsRight_Test_Values()
        {
            // nothing to shift, no shifting
            yield return new object[] { Array.Empty<byte>(), Array.Empty<byte>(), Array.Empty<byte>(), 0 };

            // no shifting
            yield return new object[] { Array.Empty<byte>(), Array.Empty<byte>(), Array.Empty<byte>(), 8 };
            yield return new object[] { new byte[] { 0xac }, Array.Empty<byte>(), new byte[] { 0xac }, 0 };

            // shift across byte boundary
            yield return new object[] { new byte[] { 0x00 }, new byte[] { 0xac }, new byte[] { 0xac }, 8 };
            yield return new object[] { new byte[] { 0x00, 0xca }, new byte[] { 0xac }, new byte[] { 0xca, 0xac }, 8 };
            yield return new object[] { new byte[] { 0x00, 0x00 }, new byte[] { 0xca, 0xac }, new byte[] { 0xca, 0xac }, 16 };

            // far shifting
            yield return new object[] { new byte[] { 0x00 }, new byte[] { 0x00, 0x00, 0x00, 0xFF }, new byte[] { 0xFF }, 32 };

            // ragged shifting
            yield return new object[] { new byte[] { 0b0111_1111 }, new byte[] { 0b1000_0000 }, new byte[] { 0b1111_1111 }, 1 };
            yield return new object[] { new byte[] { 0b0011_1111 }, new byte[] { 0b1100_0000 }, new byte[] { 0b1111_1111 }, 2 };
            yield return new object[] { new byte[] { 0b0001_1111 }, new byte[] { 0b1110_0000 }, new byte[] { 0b1111_1111 }, 3 };
            yield return new object[] { new byte[] { 0b0000_1111 }, new byte[] { 0b1111_0000 }, new byte[] { 0b1111_1111 }, 4 };
            yield return new object[] { new byte[] { 0b0000_0111 }, new byte[] { 0b1111_1000 }, new byte[] { 0b1111_1111 }, 5 };
            yield return new object[] { new byte[] { 0b0000_0011 }, new byte[] { 0b1111_1100 }, new byte[] { 0b1111_1111 }, 6 };
            yield return new object[] { new byte[] { 0b0000_0001 }, new byte[] { 0b1111_1110 }, new byte[] { 0b1111_1111 }, 7 };

            yield return new object[] { new byte[] { 0b0000_0000 }, new byte[] { 0b0000_1111, 0b1111_0000 }, new byte[] { 0b1111_1111 }, 12 };
            yield return new object[] { new byte[] { 0b0000_0000 }, new byte[] { 0b0000_0001, 0b1111_1110 }, new byte[] { 0b1111_1111 }, 15 };

            yield return new object[] { new byte[] { 0b0000_0101 }, new byte[] { 0b1100_0000 }, new byte[] { 0b0101_1100 }, 4 };
            yield return new object[] { new byte[] { 0b0000_0101, 0b1100_1100 }, new byte[] { 0b0011_0000 }, new byte[] { 0b0101_1100, 0b1100_0011 }, 4 };
            yield return new object[] { new byte[] { 0b0000_0000, 0b0000_0001 }, new byte[] { 0b1011_1001, 0b1000_0110 }, new byte[] { 0b1101_1100, 0b1100_0011 }, 15 };

            // ragged far shift
            yield return new object[] { new byte[] { 0x00 }, new byte[] { 0x00, 0x00, 0x01, 0xFE }, new byte[] { 0xFF }, 31 };
        }

        [Theory]
        [MemberData(nameof(ShiftBitsRight_Test_Values))]
        public void ShiftBitsRight_Test(byte[] expected,
                                        byte[] expectedCarryOut,
                                        byte[] input,
                                        int shift)
        {
            // Arrange
            var original = input.ToArray();

            // Act
            var result = input.ShiftBitsRight(shift, out var carry);
            var resultIgnoringCarry = input.ShiftBitsRight(shift);

            // Assert
            this._testOutputHelper.WriteLine($"{input.ToString("b")} >> {shift}");
            this._testOutputHelper.WriteLine($"expected:\t{expected.ToString("b")} carry:{expectedCarryOut.ToString("b")}");
            this._testOutputHelper.WriteLine($"result:  \t{result.ToString("b")} carry:{carry.ToString("b")}");

            Assert.Equal(expected, result);
            Assert.Equal(expectedCarryOut, carry);
            Assert.Equal(expected, resultIgnoringCarry);

            Assert.Equal(original, input);
        }

        [Fact]
        public void ShiftBitsRight_NullInput_ThrowsArgumentNullException_Test()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ((byte[])null).ShiftBitsRight(42, out _));

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ((byte[])null).ShiftBitsRight(42));
        }

        [Fact]
        public void ShiftBitsRight_ShiftLessThanZero_ThrowsArgumentOutOfRangeException_Test()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentOutOfRangeException>(() => new byte[] { 0x00 }.ShiftBitsRight(-1, out _));

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentOutOfRangeException>(() => new byte[] { 0x00 }.ShiftBitsRight(-1));
        }

        #endregion

        #region ShiftBitsLeft

        public static IEnumerable<object[]> ShiftBitsLeft_Test_Values()
        {
            // nothing to shift, no shifting
            yield return new object[] { Array.Empty<byte>(), Array.Empty<byte>(), Array.Empty<byte>(), 0 };

            // no shifting
            yield return new object[] { Array.Empty<byte>(), Array.Empty<byte>(), Array.Empty<byte>(), 8 };
            yield return new object[] { new byte[] { 0xac }, Array.Empty<byte>(), new byte[] { 0xac }, 0 };

            // shift across byte boundary
            yield return new object[] { new byte[] { 0x00 }, new byte[] { 0xac }, new byte[] { 0xac }, 8 };
            yield return new object[] { new byte[] { 0xac, 0x00 }, new byte[] { 0xca }, new byte[] { 0xca, 0xac }, 8 };
            yield return new object[] { new byte[] { 0x00, 0x00 }, new byte[] { 0xca, 0xac }, new byte[] { 0xca, 0xac }, 16 };

            // far shifting
            yield return new object[] { new byte[] { 0x00 }, new byte[] { 0xFF, 0x00, 0x00, 0x00 }, new byte[] { 0xFF }, 32 };

            // ragged shifting
            yield return new object[] { new byte[] { 0b1111_1110 }, new byte[] { 0b0000_0001 }, new byte[] { 0b1111_1111 }, 1 };
            yield return new object[] { new byte[] { 0b1111_1100 }, new byte[] { 0b0000_0011 }, new byte[] { 0b1111_1111 }, 2 };
            yield return new object[] { new byte[] { 0b1111_1000 }, new byte[] { 0b0000_0111 }, new byte[] { 0b1111_1111 }, 3 };
            yield return new object[] { new byte[] { 0b1111_0000 }, new byte[] { 0b0000_1111 }, new byte[] { 0b1111_1111 }, 4 };
            yield return new object[] { new byte[] { 0b1110_0000 }, new byte[] { 0b0001_1111 }, new byte[] { 0b1111_1111 }, 5 };
            yield return new object[] { new byte[] { 0b1100_0000 }, new byte[] { 0b0011_1111 }, new byte[] { 0b1111_1111 }, 6 };
            yield return new object[] { new byte[] { 0b1000_0000 }, new byte[] { 0b0111_1111 }, new byte[] { 0b1111_1111 }, 7 };

            yield return new object[] { new byte[] { 0b0000_0000 }, new byte[] { 0b0000_1111, 0b1111_0000 }, new byte[] { 0b1111_1111 }, 12 };
            yield return new object[] { new byte[] { 0b0000_0000 }, new byte[] { 0b0111_1111, 0b1000_0000 }, new byte[] { 0b1111_1111 }, 15 };

            yield return new object[] { new byte[] { 0b1110_0000, 0b0000_0000 }, new byte[] { 0b00001_1111, 0b1111_1111 }, new byte[] { 0b1111_1111, 0b1111_1111 }, 13 };

            yield return new object[] { new byte[] { 0b1100_0000 }, new byte[] { 0b0000_0101 }, new byte[] { 0b101_1100 }, 4 };
            yield return new object[] { new byte[] { 0b1100_1100, 0b0011_0000 }, new byte[] { 0b0000_0101 }, new byte[] { 0b0101_1100, 0b1100_0011 }, 4 };
            yield return new object[] { new byte[] { 0b1000_0000, 0b0000_0000 }, new byte[] { 0b0110_1110, 0b0110_0001 }, new byte[] { 0b01101_1100, 0b1100_0011 }, 15 };

            // ragged far shift
            yield return new object[] { new byte[] { 0x00 }, new byte[] { 0x7F, 0x80, 0x00, 0x00 }, new byte[] { 0xFF }, 31 };
        }

        [Theory]
        [MemberData(nameof(ShiftBitsLeft_Test_Values))]
        public void ShiftBitsLeft_Test(byte[] expected,
                                       byte[] expectedCarryOut,
                                       byte[] input,
                                       int shift)
        {
            // Arrange
            var original = input.ToArray();

            // Act
            var result = input.ShiftBitsLeft(shift, out var carry);
            var resultIgnoringCarry = input.ShiftBitsLeft(shift);

            // Assert
            this._testOutputHelper.WriteLine($"{input.ToString("b")} << {shift}");
            this._testOutputHelper.WriteLine($"expected:\t{expected.ToString("b")} carry:{expectedCarryOut.ToString("b")}");
            this._testOutputHelper.WriteLine($"result:  \t{result.ToString("b")} carry:{carry.ToString("b")}");

            Assert.Equal(expected, result);
            Assert.Equal(expectedCarryOut, carry);
            Assert.Equal(expected, resultIgnoringCarry);

            Assert.Equal(original, input);
        }

        [Fact]
        public void ShiftBitsLeft_NullInput_ThrowsArgumentNullException_Test()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ((byte[])null).ShiftBitsLeft(42, out _));

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ((byte[])null).ShiftBitsLeft(42));
        }

        [Fact]
        public void ShiftBitsLeft_ShiftLessThanZero_ThrowsArgumentOutOfRangeException_Test()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentOutOfRangeException>(() => new byte[] { 0x00 }.ShiftBitsLeft(-1, out _));

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentOutOfRangeException>(() => new byte[] { 0x00 }.ShiftBitsLeft(-1));
        }

        #endregion

        #region Reverse

        [Theory]
        [InlineData(ulong.MaxValue)]
        [InlineData(ulong.MinValue)]
        [InlineData(42U)]
        [InlineData(0x000000000001U)]
        [InlineData(0xf00000000000U)]
        public void Reverse_Test(ulong input)
        {
            // Arrange
            var byteValue = BitConverter.GetBytes(input);

            // Act
            var result = byteValue.ReverseBytes();

            // Assert
            Assert.IsType<byte[]>(result);
            Assert.Equal(byteValue.Reverse(), result);

            Assert.Equal(BitConverter.GetBytes(input), byteValue);
        }

        [Fact]
        public void Reverse_InputNull_ThrowsArgumentNullException_Test()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ((byte[])null).ReverseBytes());
        }

        #endregion

        #region AppendShortest

        public static IEnumerable<object[]> AppendShortest_Test_Values()
        {
            var rand = new Random(42);
            const int maxWidth = 5;

            for (var i = 0; i < maxWidth; i++)
            {
                var left = new byte[i];

                rand.NextBytes(left);

                for (var j = 0; j < maxWidth; j++)
                {
                    var right = new byte[j];
                    rand.NextBytes(right);
                    yield return new object[] { left, right };
                }
            }
        }

        [Theory]
        [MemberData(nameof(AppendShortest_Test_Values))]
        public void AppendShortest_Test(byte[] left,
                                        byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            var leftLength = left.Length;
            var rightLength = right.Length;

            var expectedLength = Math.Max(leftLength, rightLength);

            // Act
            var result = ByteArrayUtils.AppendShortest(left, right);

            // Assert
            Assert.IsType<(byte[] left, byte[] right)>(result);

            var (leftResult, rightResult) = result;

            // left result

            Assert.Equal(expectedLength, leftResult.Length);

            if (leftResult.Length == leftLength)
            {
                Assert.Same(left, leftResult);
            }
            else
            {
                Assert.NotSame(left, leftResult);
            }

            Assert.Equal(left, leftResult.Take(leftLength));
            Assert.All(leftResult.Skip(expectedLength - leftLength), b => b.Equals(0x00));

            // right result
            Assert.Equal(expectedLength, rightResult.Length);

            if (rightResult.Length == rightLength)
            {
                Assert.Same(right, rightResult);
            }
            else
            {
                Assert.NotSame(right, rightResult);
            }

            Assert.Equal(right, rightResult.Take(rightLength));
            Assert.All(rightResult.Skip(expectedLength - rightLength), b => b.Equals(0x00));

            // input not mutated
            Assert.Equal(leftOriginal, left);
            Assert.Equal(rightOriginal, right);
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void AppendShortest_NullInput_ThrowsArgumentNullException_Test(byte[] left,
                                                                              byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.AppendShortest(left, right));
        }

        #endregion

        #region AppendBytes

        [Theory]
        [InlineData(new byte[] { }, 0, 0x00)]
        [InlineData(new byte[] { 0x01, 0x02 }, 0, 0x00)]
        [InlineData(new byte[] { 0x01, 0x02 }, 2, 0x00)]
        [InlineData(new byte[] { }, 0, 0xAC)]
        [InlineData(new byte[] { 0x34, 0x12 }, 0, 0xAC)]
        [InlineData(new byte[] { 0x34, 0x12 }, 2, 0xAC)]
        public void AppendBytes_Test(byte[] source,
                                     int count,
                                     byte element)
        {
            // Act
            var original = source.ToArray();

            // Arrange
            var result = source.AppendBytes(count, element);

            // Assert
            Assert.IsType<byte[]>(result);
            Assert.Equal(source.Length + count, result.Length);
            Assert.Equal(source, result.Take(source.Length));
            Assert.All(result.Skip(source.Length), b => b.Equals(element));

            Assert.Equal(original, source); // input value not affected
        }

        [Fact]
        public void AppendBytes_NullSource_ThrowsArgumentNullException_Test()
        {
            // Arrange
            // Act
            // Arrange
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.AppendBytes(null, 1));
        }

        [Fact]
        public void AppendBytes_CountLessThan0_ThrowsArgumentOutOfRangeException_Test()
        {
            // Arrange
            var source = new byte[] { 0xFF, 0xFF };
            const int count = -1;

            // Act
            // Arrange
            Assert.Throws<ArgumentOutOfRangeException>(() => source.AppendBytes(count));
        }

        [Fact]
        public void AppendBytes_DefaultsTo0x00_Test()
        {
            // Arrange
            var source = new byte[] { 0xFF, 0xFF };
            const int count = 4;

            // Act
            var result = source.AppendBytes(count);

            // Assert
            Assert.All(result.Take(count), b => b.Equals(0x00));
        }

        #endregion

        #region PrependBytes

        [Theory]
        [InlineData(new byte[] { }, 0, 0x00)]
        [InlineData(new byte[] { 0x01, 0x02 }, 0, 0x00)]
        [InlineData(new byte[] { 0x01, 0x02 }, 2, 0x00)]
        [InlineData(new byte[] { }, 0, 0xAC)]
        [InlineData(new byte[] { 0x34, 0x12 }, 0, 0xAC)]
        [InlineData(new byte[] { 0x34, 0x12 }, 2, 0xAC)]
        public void PrependBytes_Test(byte[] source,
                                      int count,
                                      byte element)
        {
            // Act
            var original = source.ToArray();

            // Arrange
            var result = source.PrependBytes(count, element);

            // Assert
            Assert.IsType<byte[]>(result);
            Assert.Equal(source.Length + count, result.Length);
            Assert.All(result.Take(count), b => b.Equals(element));
            Assert.Equal(source, result.Skip(count));

            Assert.Equal(original, source); // input value not affected
        }

        [Fact]
        public void PrependBytes_NullSource_ThrowsArgumentNullException_Test()
        {
            // Arrange
            // Act
            // Arrange
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.PrependBytes(null, 1));
        }

        [Fact]
        public void PrependBytes_CountLessThan0_ThrowsArgumentOutOfRangeException_Test()
        {
            // Arrange
            var source = new byte[] { 0xFF, 0xFF };
            const int count = -1;

            // Act
            // Arrange
            Assert.Throws<ArgumentOutOfRangeException>(() => source.PrependBytes(count));
        }

        [Fact]
        public void PrependBytes_DefaultsTo0x00_Test()
        {
            // Arrange
            var source = new byte[] { 0xFF, 0xFF };
            const int count = 4;

            // Act
            var result = source.PrependBytes(count);

            // Assert
            Assert.All(result.Take(count), b => b.Equals(0x00));
        }

        #endregion

        #region PrependShortest

        public static IEnumerable<object[]> PrependShortest_Test_Values()
        {
            var rand = new Random(42);
            const int maxWidth = 5;

            for (var i = 0; i < maxWidth; i++)
            {
                var left = new byte[i];

                rand.NextBytes(left);

                for (var j = 0; j < maxWidth; j++)
                {
                    var right = new byte[j];
                    rand.NextBytes(right);
                    yield return new object[] { left, right };
                }
            }
        }

        [Theory]
        [MemberData(nameof(PrependShortest_Test_Values))]
        public void PrependShortest_Test(byte[] left,
                                         byte[] right)
        {
            // Arrange
            var leftOriginal = left.ToArray();
            var rightOriginal = right.ToArray();

            var leftLength = left.Length;
            var rightLength = right.Length;

            var expectedLength = Math.Max(leftLength, rightLength);

            // Act
            var result = ByteArrayUtils.PrependShortest(left, right);

            // Assert
            Assert.IsType<(byte[] left, byte[] right)>(result);

            var (leftResult, rightResult) = result;

            // left result

            Assert.Equal(expectedLength, leftResult.Length);

            if (leftResult.Length == leftLength)
            {
                Assert.Same(left, leftResult);
            }
            else
            {
                Assert.NotSame(left, leftResult);
            }

            Assert.Equal(left, leftResult.Skip(expectedLength - leftLength));
            Assert.All(leftResult.Take(expectedLength - leftLength), b => b.Equals(0x00));

            // right result
            Assert.Equal(expectedLength, rightResult.Length);

            if (rightResult.Length == rightLength)
            {
                Assert.Same(right, rightResult);
            }
            else
            {
                Assert.NotSame(right, rightResult);
            }

            Assert.Equal(right, rightResult.Skip(expectedLength - rightLength));
            Assert.All(rightResult.Take(expectedLength - rightLength), b => b.Equals(0x00));

            // input not mutated
            Assert.Equal(leftOriginal, left);
            Assert.Equal(rightOriginal, right);
        }

        [Theory]
        [InlineData(null, new byte[] { 0x00 })]
        [InlineData(new byte[] { 0x00 }, null)]
        public void PrependShortest_NullInput_ThrowsArgumentNullException_Test(byte[] left,
                                                                               byte[] right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ByteArrayUtils.PrependShortest(left, right));
        }

        #endregion
    }
}
