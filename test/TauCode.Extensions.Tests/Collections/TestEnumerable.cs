using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TauCode.Extensions.Tests.Collections
{
    public class TestEnumerable<T> : IEnumerable<T>
    {
        private readonly List<T> _items;

        public TestEnumerable(IEnumerable<T> collection)
        {
            _items = collection.ToList();
        }

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
