using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using net.capp.Core;
using net.capp.Type;

namespace net.capp.UI
{
    public class Label : LabeledComponent
    {
        public Label()
        {
            Text = "Label";
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.DarkGray;
        }

        internal override ComponentBuffer CreateBuffer()
        {
            var buffer = new ComponentBuffer(new Dimension(Text.Length, 1));
            for (var i = 0; i < Text.Length; i++)
            {
                buffer[i, 0] = new ConsoleElement(Text[i], ForegroundColor, BackgroundColor);
            }

            return buffer;
        }
    }
}