using Gulliver.Enumerables;
using Xunit;

namespace Gulliver.Tests.Enumerables
{
    public class AbstractConcurrentByteEnumerableTests
    {
        #region Class

        [Fact]
        public void AssignabilityTest()
        {
            // Arrange
            var type = typeof(AbstractConcurrentByteEnumerable);

            // Act
            // Assert
            Assert.True(typeof(IConcurrentByteEnumerable).IsAssignableFrom(type));
        }

        [Fact]
        public void IsAbstractTest()
        {
            // Arrange
            var type = typeof(AbstractConcurrentByteEnumerable);

            // Act
            // Assert
            Assert.True(type.IsAbstract);
        }

        #endregion
    }
}
