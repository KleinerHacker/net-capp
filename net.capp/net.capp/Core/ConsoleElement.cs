using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.capp.Core
{
    public struct ConsoleElement
    {
        public char Value { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public ConsoleElement(char value, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Value = value;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
    }
}
