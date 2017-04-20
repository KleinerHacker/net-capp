using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.capp.Type
{
    public struct Origin
    {
        public int Left { get; }
        public int Top { get; }

        public Origin(int left, int top)
        {
            Left = left;
            Top = top;
        }

        public bool Equals(Origin other)
        {
            return Left == other.Left && Top == other.Top;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Origin && Equals((Origin) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Left * 397) ^ Top;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Left)}: {Left}, {nameof(Top)}: {Top}";
        }
    }
}
