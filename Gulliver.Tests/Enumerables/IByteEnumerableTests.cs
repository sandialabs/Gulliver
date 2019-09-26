using System.Collections.Generic;
using Gulliver.Enumerables;
using Xunit;

namespace Gulliver.Tests.Enumerables
{
    public class IByteEnumerableTests
    {
        #region Interface

        [Fact]
        public void AssignabilityTest()
        {
            // Arrange
            var type = typeof(IByteEnumerable);

            // Act
            // Assert
            Assert.True(typeof(IReadOnlyCollection<byte>).IsAssignableFrom(type));
        }

        [Fact]
        public void IsInterfaceTest()
        {
            // Arrange
            var type = typeof(IByteEnumerable);

            // Act
            // Assert
            Assert.True(type.IsInterface);
        }

        #endregion
    }
}
