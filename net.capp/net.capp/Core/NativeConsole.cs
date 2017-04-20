using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace net.capp.Core
{
    internal class NativeConsole
    {
        #region Native External Methods

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutput(
            SafeFileHandle hConsoleOutput,
            CharInfo[] lpBuffer,
            Coord dwBufferSize,
            Coord dwBufferCoord,
            ref SmallRect lpWriteRegion);

        #endregion

        public static void Write(ConsoleBuffer buffer)
        {
            var handle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            if (handle.IsInvalid)
                throw new InvalidOperationException("Unable to create file handle for console output");

            var buf = new CharInfo[buffer.Size.Width * buffer.Size.Height];
            var rect = new SmallRect(0, 0, (short) buffer.Size.Width, (short) buffer.Size.Height);

            for (var y = 0; y < buffer.Size.Height; y++)
            {
                for (var x = 0; x < buffer.Size.Width; x++)
                {
                    if (!buffer[x, y].HasValue)
                        return;

                    buf[x + y * buffer.Size.Width].Char.UnicodeChar = buffer[x, y].Value.Value;
                    buf[x + y * buffer.Size.Width].Attributes = (short)((int) buffer[x, y].Value.ForegroundColor | ((int)buffer[x, y].Value.BackgroundColor << 4));
                }
            }

            WriteConsoleOutput(handle, buf, new Coord((short) buffer.Size.Width, (short) buffer.Size.Height), new Coord(0, 0), ref rect);
        }

        #region Native Structs

        [StructLayout(LayoutKind.Sequential)]
        private struct Coord
        {
            public short X, Y;

            public Coord(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct CharUnion
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct CharInfo
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SmallRect
        {
            public short Left, Top, Right, Bottom;

            public SmallRect(short left, short top, short right, short bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }

        #endregion
    }
}