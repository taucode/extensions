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
                "uno", // 0
                "due", // 1
                "tre", // 2
                "due", // 3
                "cinque", // 4
            };
        }

        #region Find Index for IList<T>

        [Test]
        public void FindFirstIndexOf_IList_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            IList<string> list = _list;

            // Act
            var dueIndex = list.FindFirstIndexOf("due");
            var dieciIndex = list.FindFirstIndexOf("dieci");


            // Assert
            Assert.That(dueIndex, Is.EqualTo(1));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }

        [Test]
        public void FindLastIndexOf_IList_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            IList<string> list = _list;

            // Act
            var dueIndex = list.FindLastIndexOf("due");
            var dieciIndex = list.FindLastIndexOf("dieci");


            // Assert
            Assert.That(dueIndex, Is.EqualTo(3));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }


        #endregion

        #region #region Find Index for IReadOnlyList<T>


        [Test]
        public void FindFirstIndexOf_IReadOnlyList_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            IReadOnlyList<string> list = _list;

            // Act
            var dueIndex = list.FindFirstIndexOf("due");
            var dieciIndex = list.FindFirstIndexOf("dieci");


            // Assert
            Assert.That(dueIndex, Is.EqualTo(1));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }

        [Test]
        public void FindLastIndexOf_IReadOnlyList_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            IReadOnlyList<string> list = _list;

            // Act
            var dueIndex = list.FindLastIndexOf("due");
            var dieciIndex = list.FindLastIndexOf("dieci");


            // Assert
            Assert.That(dueIndex, Is.EqualTo(3));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }


        #endregion

        #region Find Index for List<T>

        [Test]
        public void FindFirstIndexOf_List_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            List<string> list = _list;

            IList<string> listAsIList = list;
            IReadOnlyList<string> listAsReadOnlyIList = list;

            // Act
            var dueIndex = listAsIList.FindFirstIndexOf("due");
            var dieciIndex = listAsReadOnlyIList.FindFirstIndexOf("dieci");


            // Assert
            Assert.That(dueIndex, Is.EqualTo(1));
            Assert.That(dieciIndex, Is.EqualTo(-1));
        }

        [Test]
        public void FindLastIndexOf_List_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            List<string> list = _list;

            IList<string> listAsIList = list;
            IReadOnlyList<string> listAsReadOnlyIList = list;

            // Act
            var dueIndex = listAsReadOnlyIList.FindLastIndexOf("due");
            var dieciIndex = listAsIList.FindLastIndexOf("dieci");

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
