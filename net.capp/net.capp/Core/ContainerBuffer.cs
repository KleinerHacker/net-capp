using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net.capp.Type;

namespace net.capp.Core
{
    internal class ContainerBuffer : ComponentBuffer
    {
        public ContainerBuffer(Dimension size) : base(size)
        {
        }

        public void OverlayBuffer(ComponentBuffer buffer, int x, int y)
        {
            if (x >= Size.Width || y >= Size.Height || buffer.Size.Width + x <= 0 || buffer.Size.Height + y <= 0)
                return; //Out of view

            int sx = Math.Max(0, x), sy = Math.Max(0, y);
            int ex = Math.Min(Size.Width, x + buffer.Size.Width), ey = Math.Min(Size.Height, y + buffer.Size.Height);

            for (var cy = sy; cy < ey; cy++)
            {
                for (var cx = sx; cx < ex; cx++)
                {
                    var element = buffer[cx - sx, cy - sy];
                    if (element == null)
                        continue;

                    this[cx, cy] = element;
                }
            }
        }
    }
}