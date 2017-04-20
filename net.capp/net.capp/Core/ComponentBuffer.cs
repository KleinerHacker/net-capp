using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net.capp.Type;

namespace net.capp.Core
{
    internal class ComponentBuffer
    {
        public virtual Dimension Size { get; }

        private ConsoleElement?[][] _elements;

        public ComponentBuffer(Dimension size)
        {
            Size = size;
            RebuildBuffer();
        }

        public ConsoleElement? this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Size.Width)
                    throw new ArgumentException("x must be between 0 and " + Size.Width);
                if (y < 0 || y >= Size.Height)
                    throw new ArgumentException("y must be between 0 and " + Size.Height);

                return _elements[x][y];
            }
            set
            {
                if (x < 0 || x >= Size.Width)
                    throw new ArgumentException("x must be between 0 and " + Size.Width);
                if (y < 0 || y >= Size.Height)
                    throw new ArgumentException("y must be between 0 and " + Size.Height);

                _elements[x][y] = value;
            }
        }

        protected void RebuildBuffer()
        {
            _elements = new ConsoleElement?[Size.Width][];
            for (var i = 0; i < Size.Width; i++)
            {
                _elements[i] = new ConsoleElement?[Size.Height];
            }
        }
    }
}
