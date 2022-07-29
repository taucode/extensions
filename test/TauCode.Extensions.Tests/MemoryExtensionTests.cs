using System.Text;

namespace TauCode.Extensions.Tests
{
    public class SplitTestCase
    {
        public int Index;
        public string? Input;
        public List<string>? Parts;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{this.Index:0000} ");

            sb.Append($"'{this.Input}'");
            return sb.ToString();

        }
    }

    [TestFixture]
    public class MemoryExtensionTests
    {
        [Test]
        public void Split_NoSeparators_ThrowsArgumentException()
        {
            // Arrange

            // Act
            var ex = Assert.Throws<ArgumentException>(() => "c:/temp/file.txt".AsSpan().Split()); // no separators

            // Assert
            Assert.That(ex!.Message, Does.StartWith("'separators' cannot be empty."));
            Assert.That(ex.ParamName, Is.EqualTo("separators"));
        }

        [TestCaseSource(nameof(GetTestCases))]
        public void Split_ValidArgument_ReturnsExpectedResult(SplitTestCase testCase)
        {
            // Arrange
            var input = testCase.Input.AsSpan();

            // Act
            var parts = input.Split('/', '\\');

            // Assert
            Assert.That(parts, Has.Count.EqualTo(testCase.Parts!.Count));

            for (var i = 0; i < parts.Count; i++)
            {
                var part = parts[i];
                var expectedPart = testCase.Parts[i];
                Assert.That(part.ToString(), Is.EqualTo(expectedPart));
            }
        }

        public static List<SplitTestCase> GetTestCases()
        {
            var list = new List<SplitTestCase>
            {
                new SplitTestCase
                {
                    Input = "",
                    Parts = new List<string>
                    {
                        "",
                    },
                },
                new SplitTestCase
                {
                    Input = "/",
                    Parts = new List<string>
                    {
                        "",
                        "",
                    },
                },
                new SplitTestCase
                {
                    Input = @"c:\ temp/file.jpg ",
                    Parts = new List<string>
                    {
                        "c:",
                        " temp",
                        "file.jpg ",

                    },
                },
                new SplitTestCase
                {
                    Input = @"/etc/ak/s.c",
                    Parts = new List<string>
                    {
                        "",
                        "etc",
                        "ak",
                        "s.c"
                    },
                },
                new SplitTestCase
                {
                    Input = @"/etc/ak/dir/",
                    Parts = new List<string>
                    {
                        "",
                        "etc",
                        "ak",
                        "dir",
                        ""
                    },
                },
            };

            for (var i = 0; i < list.Count; i++)
            {
                list[i].Index = i;
            }

            return list;
        }
    }
}
