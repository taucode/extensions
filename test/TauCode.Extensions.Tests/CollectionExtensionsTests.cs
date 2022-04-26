using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TauCode.Extensions.Tests
{
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
            Assert.That(dueIndex, Is.EqualTo(1));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }

        [Test]
        public void FindFirstIndex_ByValue_NullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            IList<string> list = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => list.FindFirstIndex("some"));

            // Assert
            Assert.That(ex.ParamName, Is.EqualTo("collection"));
        }

        #endregion

        #region todo Find Index for IList<T>

        [Test]
        public void FindFirstIndex_IList_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            IList<string> list = _list;

            // Act
            var dueIndex = list.FindFirstIndex("due");
            var dieciIndex = list.FindFirstIndex("dieci");


            // Assert
            Assert.That(dueIndex, Is.EqualTo(1));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }

        [Test]
        public void FindLastIndex_IList_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            IList<string> list = _list;

            // Act
            var dueIndex = list.FindLastIndex("due");
            var dieciIndex = list.FindLastIndex("dieci");


            // Assert
            Assert.That(dueIndex, Is.EqualTo(3));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }

        #endregion

        #region todo Find Index for IReadOnlyList<T>

        [Test]
        public void FindFirstIndex_IReadOnlyList_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            IReadOnlyList<string> list = _list;

            // Act
            var dueIndex = list.FindFirstIndex("due");
            var dieciIndex = list.FindFirstIndex("dieci");


            // Assert
            Assert.That(dueIndex, Is.EqualTo(1));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }

        [Test]
        public void FindLastIndex_IReadOnlyList_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            IReadOnlyList<string> list = _list;

            // Act
            var dueIndex = list.FindLastIndex("due");
            var dieciIndex = list.FindLastIndex("dieci");


            // Assert
            Assert.That(dueIndex, Is.EqualTo(3));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }


        #endregion

        #region todo Find Index for List<T>

        [Test]
        public void FindFirstIndex_List_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            List<string> list = _list;

            IList<string> listAsIList = list;
            IReadOnlyList<string> listAsReadOnlyIList = list;

            // Act
            var dueIndex = listAsIList.FindFirstIndex("due");
            var dieciIndex = listAsReadOnlyIList.FindFirstIndex("dieci");


            // Assert
            Assert.That(dueIndex, Is.EqualTo(1));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }

        [Test]
        public void FindLastIndex_List_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            List<string> list = _list;

            IList<string> listAsIList = list;
            IReadOnlyList<string> listAsReadOnlyIList = list;

            // Act
            var dueIndex = listAsReadOnlyIList.FindLastIndex("due");
            var dieciIndex = listAsIList.FindLastIndex("dieci");

            // Assert
            Assert.That(dueIndex, Is.EqualTo(3));
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
            List<char> list = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => list.AddCharRange('a', 'z'));

            // Assert
            Assert.That(ex.ParamName, Is.EqualTo("list"));
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
}
