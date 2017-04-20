using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.capp.Type
{
    public struct BorderSet
    {
        public char HorizontalLine { get; }
        public char VerticalLine { get; }
        public char TopLeftCorner { get; }
        public char TopRightCorner { get; }
        public char BottomLeftCorner { get; }
        public char BottomRightCorner { get; }
        public int Width { get; }

        public BorderSet(char horizontalLine, char verticalLine, char topLeftCorner, char topRightCorner, char bottomLeftCorner, char bottomRightCorner, int width)
        {
            HorizontalLine = horizontalLine;
            VerticalLine = verticalLine;
            TopLeftCorner = topLeftCorner;
            TopRightCorner = topRightCorner;
            BottomLeftCorner = bottomLeftCorner;
            BottomRightCorner = bottomRightCorner;
            Width = width;
        }

        public BorderSet(char horizontalLine, char verticalLine, char corner, int width) : this(horizontalLine, verticalLine, corner, corner, corner, corner, width)
        {
        }

        public BorderSet(char character, int width) : this(character, character, character, width)
        {
        }
    }
}