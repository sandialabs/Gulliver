using System;
using System.Collections.Generic;
using System.Linq;
using Gulliver.Enumerables;
using Xunit;

namespace Gulliver.Tests.Enumerables
{
    public class BigEndianByteEnumerableTests
    {
        #region Class

        [Fact]
        public void AssignabilityTest()
        {
            // Arrange
            var type = typeof(BigEndianByteEnumerable);

            // Act
            // Assert
            Assert.True(typeof(AbstractByteEnumerable).IsAssignableFrom(type));
        }

        [Fact]
        public void NotLittleEndian_Test()
        {
            // Arrange
            var enumerable = new BigEndianByteEnumerable(Array.Empty<byte>());

            // Act
            // Assert
            Assert.False(enumerable.IsLittleEndian);
        }

        #endregion

        #region Ctor

        [Fact]
        public void Ctor_NullBytes_Throws_ArgumentNullException_Test()
        {
            Assert.Throws<ArgumentNullException>(() => new BigEndianByteEnumerable(null));
        }

        [Fact]
        public void Ctor_Trim_Default_Test()
        {
            // Arrange
            var enumerable = new BigEndianByteEnumerable(Array.Empty<byte>());

            // Act
            // Assert
            Assert.True(enumerable.IsTrimmed);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Ctor_Trim_Set_Test(bool trim)
        {
            // Arrange
            var enumerable = new BigEndianByteEnumerable(Array.Empty<byte>(), trim);

            // Act
            // Assert
            Assert.Equal(trim, enumerable.IsTrimmed);
        }

        #endregion

        #region Iteration

        #region Standared Iteration

        [Fact]
        public void Iteration_Trimmed_Test()
        {
            // Arrange
            var input = new byte[] { 0x00, 0x00, 0xAC, 0xCA, 0x00 };
            var expected = input.SkipWhile(b => b == 0x00);

            var enumerable = new BigEndianByteEnumerable(input);

            // Act
            // Assert
            Assert.Equal(expected, enumerable);
        }

        [Fact]
        public void Iteration_Untrimmed_Test()
        {
            // Arrange
            var input = new byte[] { 0x00, 0x00, 0xAC, 0xCA, 0x00 };
            var expected = input;

            var enumerable = new BigEndianByteEnumerable(input, false);

            // Act
            // Assert
            Assert.Equal(expected, enumerable);
        }

        #endregion

        #region Reverse Iteration

        [Fact]
        public void ReverseIteration_Trimmed_Test()
        {
            // Arrange
            var input = new byte[] { 0x00, 0x00, 0xAC, 0xCA, 0x00 };
            var expected = input.SkipWhile(b => b == 0x00).Reverse();

            var enumerable = new BigEndianByteEnumerable(input);

            // Act
            var result = enumerable.ReverseEnumerable();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<byte>>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReverseIteration_Untrimmed_Test()
        {
            // Arrange
            var input = new byte[] { 0x00, 0x00, 0xAC, 0xCA, 0x00 };
            var expected = input.Reverse();

            var enumerable = new BigEndianByteEnumerable(input, false);

            // Act
            var result = enumerable.ReverseEnumerable();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<byte>>(result);
            Assert.Equal(expected, result);
        }

        #endregion

        #endregion

        #region Enumeration

        #region Standared Enumeration

        [Fact]
        public void Enumerate_Trimmed_Test()
        {
            // Arrange
            var input = new byte[] { 0x00, 0x00, 0xAC, 0xCA, 0x00 };
            var expected = input.SkipWhile(b => b == 0x00);

            var enumerable = new BigEndianByteEnumerable(input);

            // Act
            var result = new List<byte>();
            result.AddRange(enumerable);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Enumerate_Untrimmed_Test()
        {
            // Arrange
            var input = new byte[] { 0x00, 0x00, 0xAC, 0xCA, 0x00 };
            var expected = input;

            var enumerable = new BigEndianByteEnumerable(input, false);

            // Act
            var result = new List<byte>();
            result.AddRange(enumerable);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion

        #region ReverseEnumeration

        #endregion

        [Fact]
        public void ReverseEnumeration_Trimmed_Test()
        {
            // Arrange
            var input = new byte[] { 0x00, 0x00, 0xAC, 0xCA, 0x00 };
            var expected = input.SkipWhile(b => b == 0x00).Reverse();

            var enumerable = new BigEndianByteEnumerable(input);

            // Act
            var result = enumerable.ReverseEnumerable();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<byte>>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReverseEnumeration_Untrimmed_Test()
        {
            // Arrange
            var input = new byte[] { 0x00, 0x00, 0xAC, 0xCA, 0x00 };
            var expected = input.Reverse();

            var enumerable = new BigEndianByteEnumerable(input, false);

            // Act
            var result = enumerable.ReverseEnumerable();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<byte>>(result);
            Assert.Equal(expected, result);
        }

        #endregion
    }
}
