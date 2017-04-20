using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net.capp.Type;

namespace net.capp.Core
{
    internal sealed class ConsoleBuffer : ContainerBuffer
    {
        public new Dimension Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RebuildBuffer();
            }
        }

        private Dimension _size;

        public ConsoleBuffer(Dimension size) : base(size)
        {
        }
    }
}
