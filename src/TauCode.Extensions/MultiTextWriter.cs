using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TauCode.Extensions
{
    public class MultiTextWriter : TextWriter
    {
        private readonly List<TextWriter> _innerWriters;

        public MultiTextWriter(Encoding encoding, IEnumerable<TextWriter> innerWriters)
        {
            this.Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));

            if (innerWriters == null)
            {
                throw new ArgumentNullException(nameof(innerWriters));
            }

            _innerWriters = innerWriters.ToList();
            if (_innerWriters.Any(x => x == null))
            {
                throw new ArgumentException($"'{nameof(innerWriters)}' cannot contain nulls.");
            }
        }

        public override Encoding Encoding { get; }

        public IReadOnlyList<TextWriter> InnerWriters => _innerWriters;

        public override void Write(char value)
        {
            foreach (var innerWriter in _innerWriters)
            {
                innerWriter.Write(value);
            }
        }

        public override void Write(string value)
        {
            foreach (var innerWriter in _innerWriters)
            {
                innerWriter.Write(value);
            }
        }

        public override async Task WriteAsync(char value)
        {
            foreach (var innerWriter in _innerWriters)
            {
                await innerWriter.WriteAsync(value);
            }
        }

        public override async Task WriteAsync(string value)
        {
            foreach (var innerWriter in _innerWriters)
            {
                await innerWriter.WriteAsync(value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _innerWriters.Clear();
        }
    }
}
