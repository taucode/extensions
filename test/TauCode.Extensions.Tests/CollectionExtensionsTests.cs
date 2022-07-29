using TauCode.Extensions.Tests.Collections;

namespace TauCode.Extensions.Tests;

[TestFixture]
public class CollectionExtensionsTests
{
    private Dictionary<string, int> _dictionary;
    private List<string> _list;

    [SetUp]
    public void SetUp()
    {
        _dictionary = new Dictionary<string, int>
        {
            {"one", 1},
            {"two", 2},
            {"three", 3},
        };

        _list = new List<string>
        {
            "zero", // 0
            "uno", // 1
            "due", // 2
            "tre", // 3
            "quattro", // 4
            "cinque", // 5
        };
    }

    #region Find First Index

    [Test]
    public void FindFirstIndex_ByValue_ValidIListNoExplicitStartPosition_ReturnsExpectedResult()
    {
        // Arrange
        IList<string> list = _list;

        // Act
        var dueIndex = list.FindFirstIndex("due");
        var dieciIndex = list.FindFirstIndex("dieci");

        // Assert
        Assert.That(dueIndex, Is.EqualTo(2));
        Assert.That(dieciIndex, Is.EqualTo(-1));
    }

    [Test]
    public void FindFirstIndex_ByValue_ValidIReadOnlyListNoExplicitStartPosition_ReturnsExpectedResult()
    {
        // Arrange
        IReadOnlyList<string> list = new TestReadOnlyList<string>(_list);

        // Act
        var dueIndex = list.FindFirstIndex("due");
        var dieciIndex = list.FindFirstIndex("dieci");

        // Assert
        Assert.That(dueIndex, Is.EqualTo(2));
        Assert.That(dieciIndex, Is.EqualTo(-1));
    }

    [Test]
    public void FindFirstIndex_ByValue_WrongCollection_ReturnsExpectedResult()
    {
        // Arrange
        IEnumerable<string> list = new TestEnumerable<string>(_list);

        // Act
        var ex = Assert.Throws<ArgumentException>(() => list.FindFirstIndex("uno"));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("collection"));
        Assert.That(ex.Message, Does.StartWith("'collection' must be either IList<T> or IReadOnlyList<T>."));
    }

    [Test]
    public void FindFirstIndex_ByValue_CollectionIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IList<string>? list = null;

        // Act
        var ex = Assert.Throws<ArgumentNullException>(() => list!.FindFirstIndex("some"));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("collection"));
    }

    [Test]
    public void FindFirstIndex_ByValue_NegativeStartPosition_ThrowsArgumentException()
    {
        // Arrange
        IList<string> list = _list;

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => list.FindFirstIndex("tre", -5));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("startPosition"));
    }

    [Test]
    public void FindFirstIndex_ByValue_TooLargeStartPosition_ThrowsArgumentException()
    {
        // Arrange
        IList<string> list = _list;

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => list.FindFirstIndex("tre", list.Count + 1));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("startPosition"));
    }

    [Test]
    public void FindFirstIndex_ByValue_ValidIListExplicitStartPosition_ReturnsExpectedResult()
    {
        // Arrange
        _list.Add("due");

        IList<string> list = _list;

        // Act
        var dueIndex = list.FindFirstIndex("due", 1);
        var due2Index = list.FindFirstIndex("due", 3);
        var dieciIndex = list.FindFirstIndex("dieci", 1);

        // Assert
        Assert.That(dueIndex, Is.EqualTo(2));
        Assert.That(due2Index, Is.EqualTo(_list.Count - 1));
        Assert.That(dieciIndex, Is.EqualTo(-1));
    }

    #endregion

    #region Find Last Index

    [Test]
    public void FindLastIndex_ByValue_ValidIListNoExplicitStartPosition_ReturnsExpectedResult()
    {
        // Arrange
        IList<string> list = _list;

        // Act
        var dueIndex = list.FindLastIndex("due");
        var dieciIndex = list.FindLastIndex("dieci");

        // Assert
        Assert.That(dueIndex, Is.EqualTo(2));
        Assert.That(dieciIndex, Is.EqualTo(-1));
    }

    [Test]
    public void FindLastIndex_ByValue_ValidIReadOnlyListNoExplicitStartPosition_ReturnsExpectedResult()
    {
        // Arrange
        IReadOnlyList<string> list = new TestReadOnlyList<string>(_list);

        // Act
        var dueIndex = list.FindLastIndex("due");
        var dieciIndex = list.FindLastIndex("dieci");

        // Assert
        Assert.That(dueIndex, Is.EqualTo(2));
        Assert.That(dieciIndex, Is.EqualTo(-1));
    }

    [Test]
    public void FindLastIndex_ByValue_WrongCollection_ReturnsExpectedResult()
    {
        // Arrange
        IEnumerable<string> list = new TestEnumerable<string>(_list);

        // Act
        var ex = Assert.Throws<ArgumentException>(() => list.FindLastIndex("uno"));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("collection"));
        Assert.That(ex.Message, Does.StartWith("'collection' must be either IList<T> or IReadOnlyList<T>."));
    }

    [Test]
    public void FindLastIndex_ByValue_CollectionIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IList<string>? list = null;

        // Act
        var ex = Assert.Throws<ArgumentNullException>(() => list!.FindLastIndex("some"));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("collection"));
    }

    [Test]
    public void FindLastIndex_ByValue_NegativeStartPosition_ThrowsArgumentException()
    {
        // Arrange
        IList<string> list = _list;

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => list.FindLastIndex("tre", -5));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("startPosition"));
    }

    [Test]
    public void FindLastIndex_ByValue_TooLargeStartPosition_ThrowsArgumentException()
    {
        // Arrange
        IList<string> list = _list;

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => list.FindLastIndex("tre", list.Count + 1));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("startPosition"));
    }

    [Test]
    public void FindLastIndex_ByValue_ValidIListExplicitStartPosition_ReturnsExpectedResult()
    {
        // Arrange
        var list1 = _list.ToList();

        var list2 = _list.ToList();
        list2.Add("due");

        // Act
        var due1Index = list1.FindLastIndex("due", 3);
        var due2Index = list2.FindLastIndex("due", 3);

        var dieciIndex = list1.FindLastIndex("dieci", 1);

        // Assert
        Assert.That(due1Index, Is.EqualTo(-1));
        Assert.That(due2Index, Is.EqualTo(list2.Count - 1));
        Assert.That(dieciIndex, Is.EqualTo(-1));
    }

    #endregion

    [Test]
    public void AddCharRange_HappyPath_ProducesExpectedResult()
    {
        // Arrange
        var list = new List<char>();

        // Act
        list.AddCharRange('a', 'z');

        // Assert
        Assert.That(list, Has.Count.EqualTo(26));

        for (var c = 'a'; c <= 'z'; c++)
        {
            Assert.That(list.Contains(c));
        }
    }

    [Test]
    public void AddCharRange_SingleChar_AddsSingleChar()
    {
        // Arrange
        var list = new List<char>();

        // Act
        list.AddCharRange('z', 'z');

        // Assert
        Assert.That(list, Has.Count.EqualTo(1));

        Assert.That(list.Single(), Is.EqualTo('z'));
    }

    [Test]
    public void AddCharRange_ListIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        List<char>? list = null;

        // Act
        var ex = Assert.Throws<ArgumentNullException>(() => list!.AddCharRange('a', 'z'));

        // Assert
        Assert.That(ex!.ParamName, Is.EqualTo("list"));
    }

    [Test]
    public void AddCharRange_InvalidRange_ThrowsArgumentException()
    {
        // Arrange
        var list = new List<char>();

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => list.AddCharRange('b', 'a'));

        // Assert
        Assert.That(ex.Message, Does.StartWith("'to' must be not less than 'from'."));
        Assert.That(ex.ParamName, Is.EqualTo("to"));
    }
}