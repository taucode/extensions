using System.Collections;

namespace TauCode.Extensions.Tests.Collections;

public class TestReadOnlyList<T> : IReadOnlyList<T>
{
    private readonly List<T> _list;

    public TestReadOnlyList(IEnumerable<T> collection)
    {
        _list = collection.ToList();
    }

    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public int Count => _list.Count;

    public T this[int index]
    {
        get => _list[index];
        set => _list[index] = value;
    }
}
