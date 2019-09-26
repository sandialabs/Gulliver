using System.Collections.Generic;
using Gulliver.Enumerables;
using Xunit;

namespace Gulliver.Tests.Enumerables
{
    public class IConcurrentByteEnumerableTests
    {
        #region Interface

        [Fact]
        public void AssignabilityTest()
        {
            // Arrange
            var type = typeof(IConcurrentByteEnumerable);

            // Act
            // Assert
            Assert.True(typeof(IReadOnlyCollection<(byte leftByte, byte rightByte)>).IsAssignableFrom(type));
        }

        [Fact]
        public void IsInterfaceTest()
        {
            // Arrange
            var type = typeof(IConcurrentByteEnumerable);

            // Act
            // Assert
            Assert.True(type.IsInterface);
        }

        #endregion
    }
}
