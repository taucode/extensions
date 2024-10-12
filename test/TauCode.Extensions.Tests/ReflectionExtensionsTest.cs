using NUnit.Framework.Legacy;
using System.Text;

namespace TauCode.Extensions.Tests;

[TestFixture]
public class ReflectionExtensionsTest
{
    #region Nested

    public class Foo
    {
        private readonly int _privateField;
        protected readonly int _protectedField;
        internal readonly int _internalField;
        public readonly int _publicField;

        public Foo(
            int privateField,
            int protectedField,
            int internalField,
            int publicField)
        {
            _privateField = privateField;
            _protectedField = protectedField;
            _internalField = internalField;
            _publicField = publicField;
        }

        public int GetPrivateField() => _privateField;
        public int GetProtectedField() => _protectedField;
        public int GetInternalField() => _internalField;
        public int GetPublicField() => _publicField;
    }

    #endregion

    #region SetFieldValue

    [Test]
    public void SetFieldValue_PublicField_ReturnsValue()
    {
        // Arrange
        var foo = new Foo(10, 20, 30, 40);

        // Act
        foo.SetFieldValue("_publicField", 1599);
        var newPublicFieldValue = foo.GetPublicField();

        // Assert
        Assert.That(newPublicFieldValue, Is.EqualTo(1599));
    }

    [Test]
    public void SetFieldValue_InternalField_ReturnsValue()
    {
        // Arrange
        var foo = new Foo(10, 20, 30, 40);

        // Act
        foo.SetFieldValue("_internalField", 1599);
        var newInternalFieldValue = foo.GetInternalField();

        // Assert
        Assert.That(newInternalFieldValue, Is.EqualTo(1599));
    }

    [Test]
    public void SetFieldValue_ProtectedField_ReturnsValue()
    {
        // Arrange
        var foo = new Foo(10, 20, 30, 40);

        // Act
        foo.SetFieldValue("_protectedField", 1599);
        var newProtectedFieldValue = foo.GetProtectedField();

        // Assert
        Assert.That(newProtectedFieldValue, Is.EqualTo(1599));
    }

    [Test]
    public void SetFieldValue_PrivateField_ReturnsValue()
    {
        // Arrange
        var foo = new Foo(10, 20, 30, 40);

        // Act
        foo.SetFieldValue("_privateField", 1599);
        var newPrivateFieldValue = foo.GetPrivateField();

        // Assert
        Assert.That(newPrivateFieldValue, Is.EqualTo(1599));
    }

    [Test]
    public void SetFieldValue_InstanceIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        Foo? foo = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => foo!.SetFieldValue("_publicField", 1599));

        Assert.That(ex!.ParamName, Is.EqualTo("instance"));
    }

    [Test]
    public void SetFieldValue_FieldNameIsInvalid_ThrowsArgumentException()
    {
        // Arrange
        var foo = new Foo(10, 20, 30, 40);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => foo.SetFieldValue("  ", 1599));

        Assert.That(ex.ParamName, Is.EqualTo("fieldName"));
    }

    #endregion

    #region GetFieldValue

    [Test]
    public void GetFieldValue_ValidFieldName_ReturnsValue()
    {
        // Arrange
        var foo = new Foo(10, 20, 30, 40);

        // Act
        var privateFieldValue = (int)foo.GetFieldValue("_privateField");
        var protectedFieldValue = (int)foo.GetFieldValue("_protectedField");
        var internalFieldValue = (int)foo.GetFieldValue("_internalField");
        var publicFieldValue = (int)foo.GetFieldValue("_publicField");

        // Assert
        Assert.That(privateFieldValue, Is.EqualTo(10));
        Assert.That(protectedFieldValue, Is.EqualTo(20));
        Assert.That(internalFieldValue, Is.EqualTo(30));
        Assert.That(publicFieldValue, Is.EqualTo(40));
    }

    [Test]
    public void GetFieldValue_InstanceIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        Foo foo = null!;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => foo.GetFieldValue("_publicField"));

        Assert.That(ex!.ParamName, Is.EqualTo("instance"));
    }

    [Test]
    public void GetFieldValue_FieldNameIsInvalid_ThrowsArgumentException()
    {
        // Arrange
        var foo = new Foo(10, 20, 30, 40);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => foo.GetFieldValue("  "));

        Assert.That(ex.ParamName, Is.EqualTo("fieldName"));
    }

    #endregion

    #region FindFullResourceName

    [Test]
    public void FindFullResourceName_ValidLocalName_ReturnsFullName()
    {
        // Arrange

        // Act
        var name = this.GetType().Assembly.FindFullResourceName("Some.Good.Text.File.txt");

        // Assert
        Assert.That(name, Is.EqualTo("TauCode.Extensions.Tests.Resources.Some.Good.Text.File.txt"));
    }

    #endregion

    #region GetResourceBytes

    [Test]
    public void GetResourceBytes_ValidArguments_ReturnsBytes()
    {
        // Arrange

        // Act
        var bytes = this.GetType().Assembly.GetResourceBytes("My.Binary.File.bin", true);

        // Assert
        var text = Encoding.ASCII.GetString(bytes);
        Assert.That(text, Is.EqualTo("Hello!"));
    }

    #endregion

    #region GetResourceText

    [Test]
    public void GetResourceText_ValidArguments_ReturnsText()
    {
        // Arrange

        // Act
        var text = this.GetType().Assembly.GetResourceText("My.Utf8.Text.File.txt", true);

        // Assert
        Assert.That(text, Is.EqualTo(
            @"Сделал три подхода
Четвёртый за Гарри
Гарри щас в походе вернётся едва ли /*2"));
    }

    [Test]
    public void GetResourceText_NotExistingName_ThrowsFileNotFoundException()
    {
        // Arrange

        // Act
        var ex = Assert.Throws<FileNotFoundException>(
            () => this.GetType().Assembly.GetResourceText("wat.txt", true));

        // Assert
        Assert.That(ex.Message, Is.EqualTo("Resource not found: 'wat.txt'."));
    }

    [Test]
    public void GetResourceText_AmbiguousName_ThrowsInvalidOperationException()
    {
        // Arrange

        // Act
        var ex = Assert.Throws<InvalidOperationException>(
            () => this.GetType().Assembly.GetResourceText("Text.File.txt", true));

        // Assert
        Assert.That(ex.Message, Is.EqualTo("More than one resource found: 'Text.File.txt'."));
    }

    #endregion

    #region GetResourceLines

    [Test]
    public void GetResourceLines_ValidArguments_ReturnsLines()
    {
        // Arrange

        // Act
        var lines = this.GetType().Assembly.GetResourceLines("My.Utf8.Text.File.txt", true);

        // Assert
        Assert.That(
            lines,
            Is.EquivalentTo(new[]
            {
                "Сделал три подхода",
                "Четвёртый за Гарри",
                "Гарри щас в походе вернётся едва ли /*2",
            }));
    }

    #endregion
}