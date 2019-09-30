using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

// ReSharper disable MemberCanBePrivate.Global

namespace Gulliver.Tests
{
    public class FixedBytesTests
    {
        #region Type

        [Fact]
        public void Assignability_Test()
        {
            // Arrange
            var type = typeof(FixedBytes);

            // Act
            // Assert
            Assert.True(typeof(IFormattable).IsAssignableFrom(type));

            Assert.True(typeof(IEnumerable<byte>).IsAssignableFrom(type));
            Assert.True(typeof(IReadOnlyCollection<byte>).IsAssignableFrom(type));

            Assert.True(typeof(IEquatable<FixedBytes>).IsAssignableFrom(type));
            Assert.True(typeof(IEquatable<IEnumerable<byte>>).IsAssignableFrom(type));

            Assert.True(typeof(IComparable).IsAssignableFrom(type));
            Assert.True(typeof(IComparable<FixedBytes>).IsAssignableFrom(type));
            Assert.True(typeof(IComparable<IEnumerable<byte>>).IsAssignableFrom(type));
        }

        #endregion

        #region Underlying Bytes

        #region GetBytes

        [Fact]
        public void GetBytes_Test()
        {
            // Arrange
            var bytes = new byte[] {0xAC, 0xCA};
            var fixedBytes = new FixedBytes(bytes);

            // Act
            var result = fixedBytes.GetBytes();

            // Assert
            Assert.IsType<byte[]>(result);
            Assert.True(bytes.SequenceEqual(result));
            Assert.NotSame(fixedBytes.GetBytes(), result);
        }

        #endregion

        #region GetBytesLittleEndian

        [Fact]
        public void GetBytesLittleEndian_Test()
        {
            // Arrange
            var bytes = new byte[] {0xAC, 0xCA};
            var fixedBytes = new FixedBytes(bytes);

            // Act
            var result = fixedBytes.GetBytesLittleEndian();

            // Assert
            Assert.IsType<byte[]>(result);
            Assert.True(bytes.Reverse()
                             .SequenceEqual(result));
            Assert.NotSame(fixedBytes.GetBytesLittleEndian(), result);
        }

        #endregion

        #endregion

        #region Static Members

        #region Ctor

        [Fact]
        public void Construct_ProvidedBytesAtLength_Test()
        {
            // Arrange
            var input = new byte[] {0xAC, 0x00, 0xCA};

            // Act
            var fixedBytes = new FixedBytes(input, input.Length);

            // Assert
            Assert.Equal(input, fixedBytes.GetBytes());
        }

        [Fact]
        public void Construct_NullBytes_Throws_ArgumentNullException_Test()
        {
            // Arrange
            byte[] input = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
                                                 {
                                                     // ReSharper disable once AssignNullToNotNullAttribute
                                                     // ReSharper disable once UnusedVariable
                                                     var fixedBytes = new FixedBytes(input);
                                                 });
        }

        [Fact]
        public void Construct_LengthLessThanBytesLength_Throws_ArgumentException_Test()
        {
            // Arrange
            var input = new byte[] {0xAC, 0x00, 0xCA};

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() =>
                                             {
                                                 // ReSharper disable once AssignNullToNotNullAttribute
                                                 // ReSharper disable once UnusedVariable
                                                 var fixedBytes = new FixedBytes(input, input.Length - 1);
                                             });
        }

        [Fact]
        public void Construct_LengthNegative_Throws_ArgumentException_Test()
        {
            // Arrange
            var input = new byte[] {0xAC, 0x00, 0xCA};

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() =>
                                             {
                                                 // ReSharper disable once AssignNullToNotNullAttribute
                                                 // ReSharper disable once UnusedVariable
                                                 var fixedBytes = new FixedBytes(input, -1);
                                             });
        }

        [Fact]
        public void Construct_ProvidedBytesShorterThanLength_Test()
        {
            // Arrange
            var input = new byte[] {0xAC, 0x00, 0xCA};
            const int length = 10;

            // Act
            var fixedBytes = new FixedBytes(input, length);

            // Assert
            var bytes = fixedBytes.GetBytes();
            Assert.Equal(length, bytes.Length);
            Assert.Equal(Enumerable.Repeat((byte) 0x00, length - input.Length)
                                   .Concat(input),
                         bytes);
        }

        #endregion

        #region Empty

        [Fact]
        public void Empty_Test()
        {
            // Arrange
            // Act
            var fixedBytes = FixedBytes.Empty();

            // Assert
            Assert.NotNull(fixedBytes);
            Assert.Empty(fixedBytes);
            Assert.NotSame(FixedBytes.Empty(), fixedBytes);
        }

        #endregion

        #endregion

        #region Equals(IEnumerable<byte>)

        public static IEnumerable<object[]> Equal_IEnumerableBytes_Test_Values()
        {
            // equality compare to null
            yield return new object[] {false, new FixedBytes(Array.Empty<byte>()), null};

            // equality compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {true, new FixedBytes(sameBytes), sameBytes};

            // equality compare non-equal bytes by length
            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new byte[] {0xFF, 0xFF}};

            // equality compare non-equal bytes
            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new byte[] {0xFF}};
        }

        [Theory]
        [MemberData(nameof(Equal_IEnumerableBytes_Test_Values))]
        public void Equal_IEnumerableBytes_Test(bool expected,
                                                FixedBytes self,
                                                IEnumerable<byte> other)
        {
            // Arrange
            // Act
            var result = self.Equals(other);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion

        #region Equals(FixedBytes)

        public static IEnumerable<object[]> Equal_FixedBytes_Test_Values()
        {
            // equality compare to null
            yield return new object[] {false, new FixedBytes(Array.Empty<byte>()), null};

            // equality compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {true, new FixedBytes(sameBytes), new FixedBytes(sameBytes)};

            // equality compare non-equal bytes by length
            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0xFF, 0xFF})};

            // equality compare non-equal bytes
            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0xFF})};
        }

        [Theory]
        [MemberData(nameof(Equal_FixedBytes_Test_Values))]
        public void Equal_FixedBytes_Test(bool expected,
                                          FixedBytes self,
                                          FixedBytes other)
        {
            // Arrange
            // Act
            var result = self.Equals(other);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion

        #region Operators

        #region Arithmetic Operations

        #region Addition (+) operator

        public static IEnumerable<object[]> Addition_Test_Values()
        {
            foreach (var (left, right) in Values())
            {
                var expected = new FixedBytes(ByteArrayUtils.AddUnsignedBigEndian(left, right));
                yield return new object[] {expected, new FixedBytes(left), new FixedBytes(right)};
            }

            yield return new object[] {null, null, new FixedBytes(new byte[] {0xFF})};
            yield return new object[] {null, new FixedBytes(new byte[] {0xFF}), null};
            yield return new object[] {null, null, null};

            IEnumerable<(byte[] left, byte[] right)> Values()
            {
                yield return (new byte[] {0xFF}, Array.Empty<byte>());
                yield return (new byte[] {0x00}, new byte[] {0x00});
                yield return (new byte[] {0xF0}, new byte[] {0x0F});
                yield return (new byte[] {0x42}, new byte[] {0xBD});
                yield return (new byte[] {0xFF}, new byte[] {0xBD});
            }
        }

        [Theory]
        [MemberData(nameof(Addition_Test_Values))]
        public void Addition_Test(FixedBytes expected,
                                  FixedBytes left,
                                  FixedBytes right)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, left + right);
            Assert.Equal(expected, right + left);
        }

        #endregion

        #region Subtraction (-) operator

        public static IEnumerable<object[]> Subtraction_Test_Values()
        {
            foreach (var (left, right) in Values())
            {
                var expected = new FixedBytes(ByteArrayUtils.SubtractUnsignedBigEndian(left, right));
                yield return new object[] {expected, new FixedBytes(left), new FixedBytes(right)};
            }

            yield return new object[] {null, null, new FixedBytes(new byte[] {0xFF})};
            yield return new object[] {null, new FixedBytes(new byte[] {0xFF}), null};
            yield return new object[] {null, null, null};

            IEnumerable<(byte[] left, byte[] right)> Values()
            {
                yield return (new byte[] {0xFF}, Array.Empty<byte>());
                yield return (new byte[] {0x00}, new byte[] {0x00});
                yield return (new byte[] {0xF0}, new byte[] {0x0F});
                yield return (new byte[] {0x42}, new byte[] {0xBD});
                yield return (new byte[] {0xFF, 0xFF}, new byte[] {0xFF, 0x00});
                yield return (new byte[] {0xFF, 0xFF}, new byte[] {0x00, 0xFF});
                yield return (new byte[] {0xFF, 0xFF}, new byte[] {0x0A, 0xC0});
            }
        }

        [Theory(Skip = "Subtraction not yet implemented")]
        [MemberData(nameof(Subtraction_Test_Values))]
        public void Subtraction_Test(FixedBytes expected,
                                     FixedBytes left,
                                     FixedBytes right)
        {
            // Arrange
            // Act
            var result = left - right;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact(Skip = "Subtraction not yet implemented")]
        public void Subtraction_LeftSmallerThanRight_Throws_InvalidOperationException_Test()
        {
            // Arrange
            var left = new FixedBytes(new byte[] {0x00, 0xAC});
            var right = new FixedBytes(new byte[] {0xCA});

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
                                                     {
                                                         // ReSharper disable once UnusedVariable
                                                         var result = left - right;
                                                     });
        }

        #endregion

        #endregion

        #region Logical Operations

        #region Logical Or (|) operator

        public static IEnumerable<object[]> LogicalOr_Test_Values()
        {
            foreach (var (right, left) in Values())
            {
                var expected = new FixedBytes(ByteArrayUtils.BitwiseOrBigEndian(left, right));
                yield return new object[] {expected, new FixedBytes(right), new FixedBytes(left)};
            }

            yield return new object[] {null, null, new FixedBytes(new byte[] {0xFF})};
            yield return new object[] {null, new FixedBytes(new byte[] {0xFF}), null};
            yield return new object[] {null, null, null};

            IEnumerable<(byte[] left, byte[] right)> Values()
            {
                yield return (new byte[] {0xFF}, Array.Empty<byte>());
                yield return (new byte[] {0x00}, new byte[] {0x00});
                yield return (new byte[] {0xF0}, new byte[] {0x0F});
                yield return (new byte[] {0x42}, new byte[] {0xBD});
                yield return (new byte[] {0xFF}, new byte[] {0xBD});
                yield return (new byte[] {0xFF, 0xFF}, new byte[] {0xBD});
            }
        }

        [Theory]
        [MemberData(nameof(LogicalOr_Test_Values))]
        public void LogicalOr_Test(FixedBytes expected,
                                   FixedBytes right,
                                   FixedBytes left)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, left | right);
            Assert.Equal(expected, right | left);
        }

        #endregion

        #region Logical And (&) operator

        public static IEnumerable<object[]> LogicalAnd_Test_Values()
        {
            foreach (var (left, right) in Values())
            {
                var expected = new FixedBytes(ByteArrayUtils.BitwiseAndBigEndian(left, right));
                yield return new object[] {expected, new FixedBytes(left), new FixedBytes(right)};
            }

            yield return new object[] {null, null, new FixedBytes(new byte[] {0xFF})};
            yield return new object[] {null, new FixedBytes(new byte[] {0xFF}), null};
            yield return new object[] {null, null, null};

            IEnumerable<(byte[] left, byte[] right)> Values()
            {
                yield return (new byte[] {0xFF}, Array.Empty<byte>());
                yield return (new byte[] {0x00}, new byte[] {0x00});
                yield return (new byte[] {0xF0}, new byte[] {0x0F});
                yield return (new byte[] {0x42}, new byte[] {0xBD});
                yield return (new byte[] {0xFF}, new byte[] {0xBD});
                yield return (new byte[] {0xFF, 0xFF}, new byte[] {0xBD});
            }
        }

        [Theory]
        [MemberData(nameof(LogicalAnd_Test_Values))]
        public void LogicalAnd_Test(FixedBytes expected,
                                    FixedBytes right,
                                    FixedBytes left)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, left & right);
            Assert.Equal(expected, right & left);
        }

        #endregion

        #region Logical Xor (^) operator

        public static IEnumerable<object[]> LogicalXor_Test_Values()
        {
            foreach (var (left, right) in Values())
            {
                var expected = new FixedBytes(ByteArrayUtils.BitwiseXorBigEndian(left, right));
                yield return new object[] {expected, new FixedBytes(left), new FixedBytes(right)};
            }

            yield return new object[] {null, null, new FixedBytes(new byte[] {0xFF})};
            yield return new object[] {null, new FixedBytes(new byte[] {0xFF}), null};
            yield return new object[] {null, null, null};

            IEnumerable<(byte[] left, byte[] right)> Values()
            {
                yield return (new byte[] {0xFF}, Array.Empty<byte>());
                yield return (new byte[] {0x00}, new byte[] {0x00});
                yield return (new byte[] {0xF0}, new byte[] {0x0F});
                yield return (new byte[] {0x42}, new byte[] {0xBD});
                yield return (new byte[] {0xFF}, new byte[] {0xBD});
                yield return (new byte[] {0xFF, 0xFF}, new byte[] {0xBD});
            }
        }

        [Theory]
        [MemberData(nameof(LogicalXor_Test_Values))]
        public void LogicalXor_Test(FixedBytes expected,
                                    FixedBytes right,
                                    FixedBytes left)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, left ^ right);
            Assert.Equal(expected, right ^ left);
        }

        #endregion

        #region Logical Not (~) operator

        public static IEnumerable<object[]> LogicalNot_Test_Values()
        {
            foreach (var bytes in Values())
            {
                var expected = new FixedBytes(ByteArrayUtils.BitwiseNot(bytes));
                yield return new object[] {expected, new FixedBytes(bytes)};
            }

            yield return new object[] {null, null};

            IEnumerable<byte[]> Values()
            {
                yield return Array.Empty<byte>();
                yield return new byte[] {0x00};
                yield return new byte[] {0x0F};
                yield return new byte[] {0xAC, 0xCA};
            }
        }

        [Theory]
        [MemberData(nameof(LogicalNot_Test_Values))]
        public void LogicalNot_Test(FixedBytes expected,
                                    FixedBytes operand)
        {
            // Arrange
            // Act
            var result = !operand;

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion

        #region Logical Left Shift (<<) operator

        public static IEnumerable<object[]> LogicalLeftShift_Test_Values()
        {
            foreach (var shift in new[] {0, 1, 8, 32})
            {
                foreach (var bytes in Values())
                {
                    var expected = new FixedBytes(bytes.ShiftBitsLeft(shift));
                    yield return new object[] {expected, new FixedBytes(bytes), shift};
                }
            }

            yield return new object[] {null, null, 42};

            IEnumerable<byte[]> Values()
            {
                yield return Array.Empty<byte>();
                yield return new byte[] {0x00};
                yield return new byte[] {0x0F};
                yield return new byte[] {0xAC, 0xCA};
            }
        }

        [Theory]
        [MemberData(nameof(LogicalLeftShift_Test_Values))]
        public void LogicalLeftShift_Test(FixedBytes expected,
                                          FixedBytes operand,
                                          int shift)
        {
            // Arrange
            // Act
            var result = operand << shift;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LogicalLeftShift_ShiftLessThan0_Throws_ArgumentException_TestTest()
        {
            // Arrange
            var fixedBytes = new FixedBytes(new byte[] {0x00});

            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                                                       {
                                                           // ReSharper disable once UnusedVariable
                                                           var result = fixedBytes << -1;
                                                       });
        }

        #endregion

        #region Logical Right Shift (>>) operator

        public static IEnumerable<object[]> LogicalRightShift_Test_Values()
        {
            foreach (var shift in new[] {0, 1, 8, 32})
            {
                foreach (var bytes in Values())
                {
                    var expected = new FixedBytes(bytes.ShiftBitsRight(shift));
                    yield return new object[] {expected, new FixedBytes(bytes), shift};
                }
            }

            yield return new object[] {null, null, 42};

            IEnumerable<byte[]> Values()
            {
                yield return Array.Empty<byte>();
                yield return new byte[] {0x00};
                yield return new byte[] {0x0F};
                yield return new byte[] {0xAC, 0xCA};
            }
        }

        [Theory]
        [MemberData(nameof(LogicalRightShift_Test_Values))]
        public void LogicalRightShift_Test(FixedBytes expected,
                                           FixedBytes operand,
                                           int shift)
        {
            // Arrange
            // Act
            var result = operand >> shift;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LogicalRightShift_ShiftLessThan0_Throws_ArgumentException_TestTest()
        {
            // Arrange
            var fixedBytes = new FixedBytes(new byte[] {0x00});

            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                                                       {
                                                           // ReSharper disable once UnusedVariable
                                                           var result = fixedBytes >> -1;
                                                       });
        }

        #endregion

        #endregion

        #region Conversion Operators

        #region Explicit Operator

        [Fact]
        public void Explicit_FixedBytes_To_ByteArray_Test()
        {
            // Arrange
            var bytes = new byte[] {0x00, 0xAC, 0xCA};
            var fixedBytes = new FixedBytes(bytes);

            // Act
            byte[] asBytes = fixedBytes;

            // Assert
            Assert.IsType<byte[]>(asBytes);
            Assert.True(asBytes.SequenceEqual(bytes));
        }

        [Fact]
        public void Explicit_FixedBytes_To_ByteArray_Null_Returns_Null_Test()
        {
            // Arrange
            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            byte[] asBytes = (FixedBytes) null;

            // Assert
            Assert.Null(asBytes);
        }

        #endregion

        #region Implicit Operator

        [Fact]
        public void Explicit_ByteArray_To_FixedBytes_Test()
        {
            // Arrange
            var bytes = new byte[] {0x00, 0xAC, 0xCA};

            // Act
            var fixedBytes = (FixedBytes) bytes;

            // Assert
            Assert.NotNull(fixedBytes);
            Assert.IsType<FixedBytes>(fixedBytes);
            Assert.True(fixedBytes.Exactly(bytes));
        }

        [Fact]
        public void Explicit_ByteArray_To_FixedBytes_Null_Returns_Null_Test()
        {
            // Arrange
            // Act
            var fixedBytes = (FixedBytes) (byte[]) null;

            // Assert
            Assert.Null(fixedBytes);
        }

        [Fact]
        public void Explicit_ByteList_To_FixedBytes_Test()
        {
            // Arrange
            var bytes = new byte[] {0x00, 0xAC, 0xCA}.ToList();

            // Act
            var fixedBytes = (FixedBytes) bytes;

            // Assert
            Assert.NotNull(fixedBytes);
            Assert.IsType<FixedBytes>(fixedBytes);
            Assert.True(fixedBytes.Exactly(bytes));
        }

        [Fact]
        public void Explicit_ByteList_To_FixedBytes_Null_Returns_Null_Test()
        {
            // Arrange
            // Act
            var fixedBytes = (FixedBytes) (List<byte>) null;

            // Assert
            Assert.Null(fixedBytes);
        }

        #endregion

        #region Comparisions Operators

        public static IEnumerable<object[]> Comparision_GreaterThan_Test_Values()
        {
            // expected: Left > Right
            // compare to self
            var self = new FixedBytes(Array.Empty<byte>());
            yield return new object[] {false, self, self};

            // compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {false, new FixedBytes(sameBytes), new FixedBytes(sameBytes)};

            // compare less than value
            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0xFF})};

            // compare to greater than value
            yield return new object[] {true, new FixedBytes(new byte[] {0xFF}), new FixedBytes(new byte[] {0x00})};

            // compare to greater than value of different sizes
            yield return new object[] {true, new FixedBytes(new byte[] {0x00, 0xFF}), new FixedBytes(new byte[] {0x00})};
            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0x00, 0xFF})};
        }

        [Theory]
        [MemberData(nameof(Comparision_GreaterThan_Test_Values))]
        public void Comparision_GreaterThan_FixedBytes_Test(bool expected,
                                                            FixedBytes left,
                                                            FixedBytes right)
        {
            // Arrange
            // Act
            var result = left > right;

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> Comparision_LessThan_Test_Values()
        {
            // expected: Left < Right
            // compare to self
            var self = new FixedBytes(Array.Empty<byte>());
            yield return new object[] {false, self, self};

            // compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {false, new FixedBytes(sameBytes), new FixedBytes(sameBytes)};

            // compare less than value
            yield return new object[] {true, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0xFF})};

            // compare to greater than value
            yield return new object[] {false, new FixedBytes(new byte[] {0xFF}), new FixedBytes(new byte[] {0x00})};

            // compare to greater than value of different sizes
            yield return new object[] {false, new FixedBytes(new byte[] {0x00, 0xFF}), new FixedBytes(new byte[] {0x00})};
            yield return new object[] {true, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0x00, 0xFF})};
        }

        [Theory]
        [MemberData(nameof(Comparision_LessThan_Test_Values))]
        public void Comparision_LessThan_FixedBytes_Test(bool expected,
                                                         FixedBytes left,
                                                         FixedBytes right)
        {
            // Arrange
            // Act
            var result = left < right;

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> Comparision_GreaterThanOrEquals_FixedBytes_Test_Values()
        {
            // expected: Left >= Right
            // compare to self
            var self = new FixedBytes(Array.Empty<byte>());
            yield return new object[] {true, self, self};

            // compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {true, new FixedBytes(sameBytes), new FixedBytes(sameBytes)};

            // compare less than value
            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0xFF})};

            // compare to greater than value
            yield return new object[] {true, new FixedBytes(new byte[] {0xFF}), new FixedBytes(new byte[] {0x00})};

            // compare to greater than value of different sizes
            yield return new object[] {true, new FixedBytes(new byte[] {0x00, 0xFF}), new FixedBytes(new byte[] {0x00})};
            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0x00, 0xFF})};
        }

        [Theory]
        [MemberData(nameof(Comparision_GreaterThanOrEquals_FixedBytes_Test_Values))]
        public void Comparision_GreaterThanOrEquals_FixedBytes_Test(bool expected,
                                                                    FixedBytes left,
                                                                    FixedBytes right)
        {
            // Arrange
            // Act
            var result = left >= right;

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> Comparision_LessThanOrEquals_FixedBytes_Test_Values()
        {
            // expected: Left <= Right
            // compare to self
            var self = new FixedBytes(Array.Empty<byte>());
            yield return new object[] {true, self, self};

            // compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {true, new FixedBytes(sameBytes), new FixedBytes(sameBytes)};

            // compare less than value
            yield return new object[] {true, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0xFF})};

            // compare to greater than value
            yield return new object[] {false, new FixedBytes(new byte[] {0xFF}), new FixedBytes(new byte[] {0x00})};

            // compare to greater than value of different sizes
            yield return new object[] {false, new FixedBytes(new byte[] {0x00, 0xFF}), new FixedBytes(new byte[] {0x00})};
            yield return new object[] {true, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0x00, 0xFF})};
        }

        [Theory]
        [MemberData(nameof(Comparision_LessThanOrEquals_FixedBytes_Test_Values))]
        public void Comparision_LessThanOrEquals_FixedBytes_Test(bool expected,
                                                                 FixedBytes left,
                                                                 FixedBytes right)
        {
            // Arrange
            // Act
            var result = left <= right;

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> Comparision_Equal_Test_Values()
        {
            // equality compare to self
            var self = new FixedBytes(Array.Empty<byte>());
            yield return new object[] {true, self, self};

            // equality compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {true, new FixedBytes(sameBytes), new FixedBytes(sameBytes)};

            yield return new object[] {true, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0x00, 0x00})};
            yield return new object[] {true, new FixedBytes(new byte[] {0xFF}), new FixedBytes(new byte[] {0x00, 0xFF})};
        }

        [Theory]
        [MemberData(nameof(Comparision_Equal_Test_Values))]
        public void Comparision_Equal_Test(bool expected,
                                           FixedBytes right,
                                           FixedBytes left)
        {
            // Arrange
            // Act

            // Assert == operator
            Assert.Equal(expected, right == left); // forwards
            Assert.Equal(expected, left == right); // reverse

            // Assert != operator
            Assert.Equal(!expected, right != left); // inverted forward
            Assert.Equal(!expected, left != right); // inverted reverse

            // Assert >= operator
            Assert.Equal(expected, right >= left);
            Assert.Equal(expected, left >= right);

            // Assert <= operator
            Assert.Equal(expected, left <= right);
            Assert.Equal(expected, right >= left);
        }

        [Fact]
        public void Comparision_ToNull_Test()
        {
            // Arrange
            var fixedByte = new FixedBytes(new byte[] {0x00});

            // Act
            // Assert operator ==
#pragma warning disable SA1131  // purposely testing ordering
            Assert.False(fixedByte == null);
            Assert.False(null == fixedByte);

            // Assert operator !=
            Assert.True(fixedByte != null);
            Assert.True(null != fixedByte);

            // Assert operator >
            Assert.True(fixedByte > null);
            Assert.False(null > fixedByte);

            // Assert operator <
            Assert.True(null < fixedByte);
            Assert.False(fixedByte < null);

            // Assert operator >=
            Assert.False(fixedByte <= null);
            Assert.True(null <= fixedByte);

            // Assert operator <=
            Assert.True(fixedByte >= null);
            Assert.False(null >= fixedByte);
#pragma warning restore SA1131
        }

        #endregion

        #endregion

        #endregion

        #region CompareTo

        #region CompareTo(FixedBytes)

        public static IEnumerable<object[]> CompareTo_FixedBytes_Test_Values()
        {
            // compare to null
            yield return new object[] {1, new FixedBytes(Array.Empty<byte>()), null};

            // compare to self
            var self = new FixedBytes(Array.Empty<byte>());
            yield return new object[] {0, self, self};

            // compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {0, new FixedBytes(sameBytes), new FixedBytes(sameBytes)};

            yield return new object[] { -1, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0xFF})};
            yield return new object[] {1, new FixedBytes(new byte[] {0xFF}), new FixedBytes(new byte[] {0x00})};

            yield return new object[] { -1, new FixedBytes(new byte[] {0x00, 0x00}), new FixedBytes(new byte[] {0xFF})};
            yield return new object[] {1, new FixedBytes(new byte[] {0xFF}), new FixedBytes(new byte[] {0x00, 0x00})};

            yield return new object[] { -1, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0x00, 0xFF})};
            yield return new object[] {1, new FixedBytes(new byte[] {0x00, 0xFF}), new FixedBytes(new byte[] {0x00})};
        }

        [Theory]
        [MemberData(nameof(CompareTo_FixedBytes_Test_Values))]
        public void CompareTo_FixedBytes_Test(int expected,
                                              FixedBytes fixedBytes,
                                              object comparedTo)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, fixedBytes.CompareTo(comparedTo));
            Assert.Equal(expected, fixedBytes.CompareTo((FixedBytes) comparedTo));
        }

        #endregion

        #region CompareTo(IEnumerabe<byte>)

        public static IEnumerable<object[]> CompareTo_Bytes_Test_Values()
        {
            // compare to null
            yield return new object[] {1, new FixedBytes(Array.Empty<byte>()), null};

            // compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {0, new FixedBytes(sameBytes), sameBytes};

            yield return new object[] { -1, new FixedBytes(new byte[] {0x00}), new byte[] {0xFF}};
            yield return new object[] {1, new FixedBytes(new byte[] {0xFF}), new byte[] {0x00}};

            yield return new object[] { -1, new FixedBytes(new byte[] {0x00, 0x00}), new byte[] {0xFF}};
            yield return new object[] {1, new FixedBytes(new byte[] {0xFF}), new byte[] {0x00, 0x00}};

            yield return new object[] { -1, new FixedBytes(new byte[] {0x00}), new byte[] {0x00, 0xFF}};
            yield return new object[] {1, new FixedBytes(new byte[] {0x00, 0xFF}), new byte[] {0x00}};
        }

        [Theory]
        [MemberData(nameof(CompareTo_Bytes_Test_Values))]
        public void CompareTo_Bytes_Test(int expected,
                                         FixedBytes fixedBytes,
                                         byte[] bytes)
        {
            // Arrange
            // Act
            var result = fixedBytes.CompareTo(bytes);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion

        #region Addition

        #endregion

        #endregion

        #region Exactly

        #region Exactly (FixedBytes)

        public static IEnumerable<object[]> Comparision_Exactly_Test_Values()
        {
            // equality compare to self
            var self = new FixedBytes(Array.Empty<byte>());
            yield return new object[] {true, self, self};

            // equality compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {true, new FixedBytes(sameBytes), new FixedBytes(sameBytes)};

            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new FixedBytes(new byte[] {0x00, 0x00})};
            yield return new object[] {false, new FixedBytes(new byte[] {0xFF}), new FixedBytes(new byte[] {0x00, 0xFF})};
        }

        [Theory]
        [MemberData(nameof(Comparision_Exactly_Test_Values))]
        public void Comparision_Exactly_Test(bool expected,
                                             FixedBytes right,
                                             FixedBytes left)
        {
            // Arrange
            // Act
            // Assert Equals
            Assert.Equal(expected, right.Exactly(left)); // forwards
            Assert.Equal(expected, left.Exactly(right)); // reverse
        }

        #endregion

        #region Exactly(IEnumerable<byte>)

        public static IEnumerable<object[]> Exactly_Bytes_Test_Values()
        {
            // equality compare to null
            yield return new object[] {false, new FixedBytes(Array.Empty<byte>()), null};

            // equality compare to equal valued bytes
            var sameBytes = new byte[] {0xAC, 0xCA};
            yield return new object[] {true, new FixedBytes(sameBytes), sameBytes};

            // equality compare non-equal bytes by length
            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new byte[] {0xFF, 0xFF}};

            // equality compare non-equal bytes
            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new byte[] {0xFF}};

            yield return new object[] {false, new FixedBytes(new byte[] {0x00}), new byte[] {0x00, 0x00}};
            yield return new object[] {false, new FixedBytes(new byte[] {0xFF}), new byte[] {0x00, 0xFF}};
        }

        [Theory]
        [MemberData(nameof(Exactly_Bytes_Test_Values))]
        public void Exactly_Bytes_Test(bool expected,
                                       FixedBytes self,
                                       byte[] other)
        {
            // Arrange
            // Act
            var result = self.Exactly(other);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion

        #endregion

        #region GetHashCode

        public static IEnumerable<object[]> GetHashCode_SameByteValue_Collides_Test_Values()
        {
            yield return new object[] { Array.Empty<byte>() };
            yield return new object[] {new byte[] {0x00}};
            yield return new object[] {new byte[] {0xAC, 0xCA}};
        }

        [Theory]
        [MemberData(nameof(GetHashCode_SameByteValue_Collides_Test_Values))]
        public void GetHashCode_SameByteValue_Collides_Test(byte[] bytes)
        {
            // Arrange
            var fixedBytes = new FixedBytes(bytes);

            // Act
            var hashCode = fixedBytes.GetHashCode();

            // Assert
            Assert.Equal(new FixedBytes(bytes).GetHashCode(), hashCode);
        }

        [Fact]
        public void GetHashCode_LargeBytes_Test()
        {
            // Arrange
            var lotsOfBytes = Enumerable.Repeat((byte) 0xCA, 100);
            var fixedBytes = new FixedBytes(lotsOfBytes);

            // Act
            // ReSharper disable once UnusedVariable
            var hashCode = fixedBytes.GetHashCode();

            // Assert
            Assert.True(true, "No overflow failure detected"); // the value doesn't really matter to as so much as we don't thrown an overflow exception
        }

        #endregion

        #region Indexer

        [Fact]
        public void Indexer_Test()
        {
            // Arrange
            var bytes = new byte[] {0x00, 0xAC, 0xCA, 0xFF};
            var fixedBytes = new FixedBytes(bytes);

            // Act
            // Assert
            for (var i = 0; i < bytes.Length; i++)
            {
                var b = fixedBytes[i];
                Assert.Equal(bytes[i], b);
            }
        }

        [Fact]
        public void Indexer_Index_LessThan0_Throws_ArgumentOutOfRangeException_Test()
        {
            // Arrange
            var fixedBytes = new FixedBytes(Array.Empty<byte>());

            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                                                       {
                                                           // ReSharper disable once UnusedVariable
                                                           var b = fixedBytes[-1];
                                                       });
        }

        [Fact]
        public void Indexer_Index_GreaterThanCount_Throws_ArgumentOutOfRangeException_Test()
        {
            // Arrange
            var fixedBytes = new FixedBytes(new byte[] {0xAC, 0xCA});

            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                                                       {
                                                           // ReSharper disable once UnusedVariable
                                                           var b = fixedBytes[fixedBytes.Count];
                                                       });
        }

        #endregion

        #region ToString

        [Fact]
        public void ToString_Default_Test()
        {
            // Arrange
            var fixedBytes = new FixedBytes(new byte[] {0xAC, 0xFF, 0xCA});

            // Act
            var result = fixedBytes.ToString();

            // Assert
            Assert.Equal(fixedBytes.ToString("G", CultureInfo.InvariantCulture), result);
        }

        public static IEnumerable<object[]> ToString_Format_Test_Values()
        {
            var formatProvider = CultureInfo.InvariantCulture;

            foreach (var bytes in Values())
            {
                foreach (var format in Formats())
                {
                    var expected = bytes.ToString(format, formatProvider);
                    yield return new object[] {expected, new FixedBytes(bytes), format, formatProvider};
                }
            }

            IEnumerable<string> Formats()
            {
                return new[] {null, string.Empty, "g", "G", "H", "h", "HC", "hc", "d", "D", "o", "O", "b", "B", "bc", "BC", "I", "IBE", "ILE"};
            }

            IEnumerable<byte[]> Values()
            {
                yield return Array.Empty<byte>();
                yield return new byte[] {0x00};
                yield return new byte[] {0x0F};
                yield return new byte[] {0xAC, 0xCA};
            }
        }

        [Theory]
        [MemberData(nameof(ToString_Format_Test_Values))]
        public void ToString_Format_Test(string expected,
                                         FixedBytes fixedBytes,
                                         string format,
                                         IFormatProvider formatProvider)
        {
            // Arrange
            // Act
            var result = fixedBytes.ToString(format, formatProvider);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion
    }
}
