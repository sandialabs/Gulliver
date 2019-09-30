using Gulliver.Enumerables;
using Xunit;

namespace Gulliver.Tests.Enumerables
{
    public class AbstractByteEnumerableTests
    {
        #region Class

        [Fact]
        public void AssignabilityTest()
        {
            // Arrange
            var type = typeof(AbstractByteEnumerable);

            // Act
            // Assert
            Assert.True(typeof(IByteEnumerable).IsAssignableFrom(type));
        }

        [Fact]
        public void IsAbstractTest()
        {
            // Arrange
            var type = typeof(AbstractByteEnumerable);

            // Act
            // Assert
            Assert.True(type.IsAbstract);
        }

        #endregion
    }
}
