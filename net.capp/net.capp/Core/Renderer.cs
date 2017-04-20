using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net.capp.Type;

namespace net.capp.Core
{
    internal sealed class Renderer
    {
        public ConsoleBuffer Buffer { get; }

        public Renderer()
        {
            Buffer = new ConsoleBuffer(new Dimension(Console.BufferWidth, Console.BufferHeight));
        }

        public void Render()
        {
            NativeConsole.Write(Buffer);
        }
    }
}