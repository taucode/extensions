using NUnit.Framework;

namespace TauCode.Extensions.Tests
{
    [TestFixture]
    public class StringExtensionsTest
    {
        private enum Color
        {
            Red,
            White
        }

        [Test]
        public void ToEnum_ValidEnumValue_ParsesCorrectly()
        {
            // Arrange
            var color = "red";

            // Act
            var colorEnum = color.ToEnum<Color>(true);

            // Assert
            Assert.That(colorEnum, Is.EqualTo(Color.Red));
        }
    }
}
