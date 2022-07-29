namespace TauCode.Extensions.Tests;

[TestFixture]
public class DateTimeOffsetExtensionsTests
{
    [Test]
    public void IsUtcDateOffset_ExactUtcDateButNotZeroShift_ReturnsFalse()
    {
        // Arrange
        var d = DateTimeOffset.Parse("2019-01-01 02:00:00 +02:00");

        // Act
        var exactUtcDate = d.ToUniversalTime().TimeOfDay == TimeSpan.Zero;
        var res = d.IsUtcDateOffset();

        // Assert
        Assert.That(exactUtcDate, Is.True);
        Assert.That(res, Is.False);
    }
}