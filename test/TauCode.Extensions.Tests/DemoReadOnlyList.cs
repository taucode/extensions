using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TauCode.Extensions.Tests
{
    public class DemoReadOnlyList<T> : IReadOnlyList<T>
    {
        private readonly List<T> _list;

        public DemoReadOnlyList(IEnumerable<T> collection)
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
}
