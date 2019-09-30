using System;
using System.Collections.Generic;
using System.Linq;
using Gulliver.Enumerables;
using Xunit;
using Xunit.Abstractions;

namespace Gulliver.Tests.Enumerables
{
    public class ConcurrentLittleEndianByteEnumerableTests
    {
        #region Setup / Teardown

        public ConcurrentLittleEndianByteEnumerableTests(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }

        private readonly ITestOutputHelper _testOutputHelper;

        #endregion

        #region Class

        [Fact]
        public void AssignabilityTest()
        {
            // Arrange
            var type = typeof(ConcurrentLittleEndianByteEnumerable);

            // Act
            // Assert
            Assert.True(typeof(IReadOnlyCollection<(byte leftByte, byte rightByte)>).IsAssignableFrom(type));
        }

        #endregion

        #region other members

        public static IEnumerable<object[]> Byte_Values()
        {
            var byteValues = ByteValues()
                .ToList();

            foreach (var left in byteValues)
            {
                foreach (var right in byteValues)
                {
                    yield return new object[] {left, right};
                }
            }

            IEnumerable<byte[]> ByteValues()
            {
                return new[]
                       {
                           Array.Empty<byte>(),
                           new byte[] {0x00},
                           new byte[] {0xFF, 0xAC},
                           new byte[] {0x00, 0x00, 0xFF, 0xAC},
                           new byte[] {0xFF, 0xAC, 0x00, 0x00},
                           new byte[] {0x00, 0x00, 0xFF, 0xAC, 0x00, 0x00}
                       };
            }
        }

        #endregion

        #region Iteration

        [Theory]
        [MemberData(nameof(Byte_Values))]
        public void Iterate_FromByteEnumerable_Trimmed_Test(byte[] leftBytes,
                                                            byte[] rightBytes)
        {
            // Arrange
            this._testOutputHelper.WriteLine($"left: [{leftBytes.ToString("h")}]");
            this._testOutputHelper.WriteLine($"right: [{rightBytes.ToString("h")}]");

            var expectedTrimmedLeftLength = leftBytes.Reverse()
                                                     .SkipWhile(b => b == 0x00)
                                                     .Count();
            var expectedTrimmedRightLength = rightBytes.Reverse()
                                                       .SkipWhile(b => b == 0x00)
                                                       .Count();
            var length = Math.Max(expectedTrimmedLeftLength, expectedTrimmedRightLength);
            this._testOutputHelper.WriteLine($"expectedLength: {length}");

            var expectedLeft = leftBytes.Take(expectedTrimmedLeftLength)
                                        .Concat(Enumerable.Repeat((byte) 0x00, length - expectedTrimmedLeftLength))
                                        .ToArray();

            var expectedRight = rightBytes.Take(expectedTrimmedRightLength)
                                          .Concat(Enumerable.Repeat((byte) 0x00, length - expectedTrimmedRightLength))
                                          .ToArray();

            this._testOutputHelper.WriteLine($"expected left: [{expectedLeft.ToString("h")}]");
            this._testOutputHelper.WriteLine($"expected right: [{expectedRight.ToString("h")}]");

            var enumerable = new ConcurrentLittleEndianByteEnumerable(leftBytes, rightBytes);

            // Act
            var result = enumerable.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IReadOnlyCollection<(byte leftByte, byte rightByte)>>(result);

            Assert.Equal(length, result.Count);

            var leftResult = result.Select(tuple => tuple.leftByte)
                                   .ToArray();
            var rightResult = result.Select(tuple => tuple.rightByte)
                                    .ToArray();

            this._testOutputHelper.WriteLine($"resulting left: [{leftResult.ToString("h")}]");
            this._testOutputHelper.WriteLine($"resulting right: [{rightResult.ToString("h")}]");

            Assert.Equal(expectedLeft, leftResult);
            Assert.Equal(expectedRight, rightResult);
            Assert.Equal(length, enumerable.Count);
        }

        [Theory]
        [MemberData(nameof(Byte_Values))]
        public void Iterate_FromByteEnumerable_NonTrimmed_Test(byte[] leftBytes,
                                                               byte[] rightBytes)
        {
            // Arrange
            this._testOutputHelper.WriteLine($"left: [{leftBytes.ToString("h")}]");
            this._testOutputHelper.WriteLine($"right: [{rightBytes.ToString("h")}]");

            var length = Math.Max(leftBytes.Length, rightBytes.Length);

            this._testOutputHelper.WriteLine($"expectedLength: {length}");

            var expectedLeft = leftBytes.Concat(Enumerable.Repeat((byte) 0x00, length - leftBytes.Length))
                                        .ToArray();

            var expectedRight = rightBytes.Concat(Enumerable.Repeat((byte) 0x00, length - rightBytes.Length))
                                          .ToArray();

            this._testOutputHelper.WriteLine($"expected left: [{expectedLeft.ToString("h")}]");
            this._testOutputHelper.WriteLine($"expected right: [{expectedRight.ToString("h")}]");

            var enumerable = new ConcurrentLittleEndianByteEnumerable(leftBytes, rightBytes, false);

            // Act
            var result = enumerable.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IReadOnlyCollection<(byte leftByte, byte rightByte)>>(result);

            Assert.Equal(length, result.Count);

            var leftResult = result.Select(tuple => tuple.leftByte)
                                   .ToArray();
            var rightResult = result.Select(tuple => tuple.rightByte)
                                    .ToArray();

            this._testOutputHelper.WriteLine($"resulting left: [{leftResult.ToString("h")}]");
            this._testOutputHelper.WriteLine($"resulting right: [{rightResult.ToString("h")}]");

            Assert.Equal(expectedLeft, leftResult);
            Assert.Equal(expectedRight, rightResult);
            Assert.Equal(length, enumerable.Count);
        }

        [Theory]
        [MemberData(nameof(Byte_Values))]
        public void ReverseIterate_FromByteEnumerable_Trimmed_Test(byte[] leftBytes,
                                                                   byte[] rightBytes)
        {
            // Arrange
            this._testOutputHelper.WriteLine($"left: [{leftBytes.ToString("h")}]");
            this._testOutputHelper.WriteLine($"right: [{rightBytes.ToString("h")}]");

            var expectedTrimmedLeftLength = leftBytes.Reverse()
                                                     .SkipWhile(b => b == 0x00)
                                                     .Count();
            var expectedTrimmedRightLength = rightBytes.Reverse()
                                                       .SkipWhile(b => b == 0x00)
                                                       .Count();
            var length = Math.Max(expectedTrimmedLeftLength, expectedTrimmedRightLength);
            this._testOutputHelper.WriteLine($"expectedLength: {length}");

            var expectedLeft = leftBytes.Take(expectedTrimmedLeftLength)
                                        .Concat(Enumerable.Repeat((byte) 0x00, length - expectedTrimmedLeftLength))
                                        .Reverse()
                                        .ToArray();

            var expectedRight = rightBytes.Take(expectedTrimmedRightLength)
                                          .Concat(Enumerable.Repeat((byte) 0x00, length - expectedTrimmedRightLength))
                                          .Reverse()
                                          .ToArray();

            this._testOutputHelper.WriteLine($"expected left: [{expectedLeft.ToString("h")}]");
            this._testOutputHelper.WriteLine($"expected right: [{expectedRight.ToString("h")}]");

            var enumerable = new ConcurrentLittleEndianByteEnumerable(leftBytes, rightBytes);

            // Act
            var result = enumerable.ReverseEnumerable()
                                   .ToList();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IReadOnlyCollection<(byte leftByte, byte rightByte)>>(result);

            Assert.Equal(length, result.Count);

            var leftResult = result.Select(tuple => tuple.leftByte)
                                   .ToArray();
            var rightResult = result.Select(tuple => tuple.rightByte)
                                    .ToArray();

            this._testOutputHelper.WriteLine($"resulting left: [{leftResult.ToString("h")}]");
            this._testOutputHelper.WriteLine($"resulting right: [{rightResult.ToString("h")}]");

            Assert.Equal(expectedLeft, leftResult);
            Assert.Equal(expectedRight, rightResult);
            Assert.Equal(length, enumerable.Count);
        }

        [Theory]
        [MemberData(nameof(Byte_Values))]
        public void ReverseIterate_FromByteEnumerable_NonTrimmed_Test(byte[] leftBytes,
                                                                      byte[] rightBytes)
        {
            // Arrange
            this._testOutputHelper.WriteLine($"left: [{leftBytes.ToString("h")}]");
            this._testOutputHelper.WriteLine($"right: [{rightBytes.ToString("h")}]");

            var length = Math.Max(leftBytes.Length, rightBytes.Length);

            this._testOutputHelper.WriteLine($"expectedLength: {length}");

            var expectedLeft = leftBytes.Concat(Enumerable.Repeat((byte) 0x00, length - leftBytes.Length))
                                        .Reverse()
                                        .ToArray();

            var expectedRight = rightBytes.Concat(Enumerable.Repeat((byte) 0x00, length - rightBytes.Length))
                                          .Reverse()
                                          .ToArray();

            this._testOutputHelper.WriteLine($"expected left: [{expectedLeft.ToString("h")}]");
            this._testOutputHelper.WriteLine($"expected right: [{expectedRight.ToString("h")}]");

            var enumerable = new ConcurrentLittleEndianByteEnumerable(leftBytes, rightBytes, false);

            // Act
            var result = enumerable.ReverseEnumerable()
                                   .ToList();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IReadOnlyCollection<(byte leftByte, byte rightByte)>>(result);

            Assert.Equal(length, result.Count);

            var leftResult = result.Select(tuple => tuple.leftByte)
                                   .ToArray();
            var rightResult = result.Select(tuple => tuple.rightByte)
                                    .ToArray();

            this._testOutputHelper.WriteLine($"resulting left: [{leftResult.ToString("h")}]");
            this._testOutputHelper.WriteLine($"resulting right: [{rightResult.ToString("h")}]");

            Assert.Equal(expectedLeft, leftResult);
            Assert.Equal(expectedRight, rightResult);
            Assert.Equal(length, enumerable.Count);
        }

        #endregion

        #region Ctor

        #region Ctor(LittleEndianByteEnumerable, LittleEndianByteEnumerable)

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Ctor_LittleEndianByteEnumerable_NullInput_Throws_ArgumentNullException_Test(bool leftSet,
                                                                                                bool rightSet)
        {
            // Arrange
            var left = leftSet
                           ? new LittleEndianByteEnumerable(Array.Empty<byte>())
                           : null;

            var right = rightSet
                            ? new LittleEndianByteEnumerable(Array.Empty<byte>())
                            : null;

            // Act
            // Assert
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new ConcurrentLittleEndianByteEnumerable(left, right));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        #endregion

        #region Ctor(IEnumerable<byte>, IEnumerable<byte>)

        #endregion

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Ctor_IEnumerableByte_NullInput_Throws_ArgumentNullException_Test(bool leftSet,
                                                                                     bool rightSet)
        {
            // Arrange
            var left = leftSet
                           ? Array.Empty<byte>()
                           : null;

            var right = rightSet
                            ? Array.Empty<byte>()
                            : null;

            // Act
            // Assert
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new ConcurrentLittleEndianByteEnumerable(left, right));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        #endregion
    }
}
