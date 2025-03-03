using Xunit;

namespace Gulliver.Tests
{
    /// <summary>
    ///     Conversion ByteArrayUtils Tests
    /// </summary>
    public partial class ByteArrayUtilsTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData("", new byte[] { })]
        [InlineData("00000000", new byte[] { 0x00 })]
        [InlineData("00001111", new byte[] { 0x0F })]
        [InlineData("1010110011001010", new byte[] { 0xAC, 0xCA })]
        public void ToString_Binary_Contiguous_Test(string expected, byte[] input)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, input.ToString("bc"));
            Assert.Equal(expected, input.ToString("BC"));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("", new byte[] { })]
        [InlineData("00000000", new byte[] { 0x00 })]
        [InlineData("00001111", new byte[] { 0x0F })]
        [InlineData("10101100 11001010", new byte[] { 0xAC, 0xCA })]
        public void ToString_Binary_Test(string expected, byte[] input)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, input.ToString("b"));
            Assert.Equal(expected, input.ToString("B"));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("", new byte[] { })]
        [InlineData("000", new byte[] { 0x00 })]
        [InlineData("015", new byte[] { 0x0F })]
        [InlineData("172 202", new byte[] { 0xAC, 0xCA })]
        public void ToString_Decimal_Test(string expected, byte[] input)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, input.ToString("d"));
            Assert.Equal(expected, input.ToString("D"));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("0", new byte[] { })]
        [InlineData("0", new byte[] { 0x00 })]
        [InlineData("15", new byte[] { 0x0F })]
        [InlineData("44234", new byte[] { 0xAC, 0xCA })]
        public void ToString_IntegerBigEndian_Test(string expected, byte[] input)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, input.ToString("I"));
            Assert.Equal(expected, input.ToString("IBE"));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("0", new byte[] { })]
        [InlineData("0", new byte[] { 0x00 })]
        [InlineData("15", new byte[] { 0x0F })]
        [InlineData("51884", new byte[] { 0xAC, 0xCA })]
        public void ToString_IntegerLittleEndian_Test(string expected, byte[] input)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, input.ToString("ILE"));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("", new byte[] { })]
        [InlineData("00", new byte[] { 0x00 })]
        [InlineData("0f", new byte[] { 0x0F })]
        [InlineData("acca", new byte[] { 0xAC, 0xCA })]
        public void ToString_LowerHexadecimal_Contiguous_Test(string expected, byte[] input)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, input.ToString("hc"));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("", new byte[] { })]
        [InlineData("00", new byte[] { 0x00 })]
        [InlineData("0f", new byte[] { 0x0F })]
        [InlineData("ac ca", new byte[] { 0xAC, 0xCA })]
        public void ToString_LowerHexadecimal_Test(string expected, byte[] input)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, input.ToString("h"));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("", new byte[] { })]
        [InlineData("000", new byte[] { 0x00 })]
        [InlineData("017", new byte[] { 0x0F })]
        [InlineData("254 312", new byte[] { 0xAC, 0xCA })]
        public void ToString_Octal_Test(string expected, byte[] input)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, input.ToString("o"));
            Assert.Equal(expected, input.ToString("O"));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("", new byte[] { })]
        [InlineData("00", new byte[] { 0x00 })]
        [InlineData("0F", new byte[] { 0x0F })]
        [InlineData("ACCA", new byte[] { 0xAC, 0xCA })]
        public void ToString_UpperHexadecimal_Contiguous_Test(string expected, byte[] input)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, input.ToString("HC"));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("", new byte[] { })]
        [InlineData("00", new byte[] { 0x00 })]
        [InlineData("0F", new byte[] { 0x0F })]
        [InlineData("AC CA", new byte[] { 0xAC, 0xCA })]
        public void ToString_UpperHexadecimal_Test(string expected, byte[] input)
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(expected, input.ToString(null));
            Assert.Equal(expected, input.ToString(string.Empty));
            Assert.Equal(expected, input.ToString("g"));
            Assert.Equal(expected, input.ToString("G"));
            Assert.Equal(expected, input.ToString("H"));
        }
    }
}
