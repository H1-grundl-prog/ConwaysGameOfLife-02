using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ConwaysGameOfLife
{
    public class ViewPort
    {
        #region properties
        public short Width { get; }
        public short Height { get; }

        SafeFileHandle h;

        CharInfo[] buf;
        SmallRect rect;

        public static Coord topLeft;
        public static Coord btmRight;

        #endregion

        #region contructors
        public ViewPort(short width, short height)
        {
            Width = width;
            Height = height;

            h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

            buf = new CharInfo[Width * Height];
            rect = new SmallRect() { Left = 0, Top = 0, Right = Width, Bottom = Height };

            topLeft = new Coord() { X = 0, Y = 0 };
            btmRight = new Coord() { X = Width, Y = Height };
        }
        #endregion

        #region methods
        public void Init()
        {
            Console.SetWindowSize(Width, Height/2);
            Console.SetBufferSize(Width, Height/2);
            Console.CursorVisible = false;

            // Console window manipulation (prevent resize and close)
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                //DeleteMenu(sysMenu, Constants.SC_CLOSE, Constants.MF_BYCOMMAND);
                DeleteMenu(sysMenu, Constants.SC_MINIMIZE, Constants.MF_BYCOMMAND);
                DeleteMenu(sysMenu, Constants.SC_MAXIMIZE, Constants.MF_BYCOMMAND);
                DeleteMenu(sysMenu, Constants.SC_SIZE, Constants.MF_BYCOMMAND);
            }
        }

        public void Render(Grid grid)
        {
            // Convert cells inside viewport to char array.

            int i = 0;
            for (int y = 0; y < Constants.SCREEN_HEIGHT - 1; y += 2)
            {
                for (int x = 0; x < Constants.SCREEN_WIDTH; x += 1)
                {
                    char charValue = Constants.BACKGROUND_CHAR;
                    short topCell = grid.Cells[x, y].IsAliveGen[0];
                    short btmCell = grid.Cells[x, y+1].IsAliveGen[0];

                    if ( topCell == 1 &&  btmCell == 1) { charValue = Constants.TOP_AND_BOTTOM_CHAR; }
                    if ( topCell == 1 && btmCell != 1) { charValue = Constants.TOP_CHAR; }
                    if ( topCell != 1 && btmCell == 1) { charValue = Constants.BOTTOM_CHAR; }

                    buf[i].Attributes = Constants.FOREGROUND_COLOR | (Constants.BACKGROUND_COLOR << 4);
                    //buf[i].Char.AsciiChar = (byte)charValue;
                    buf[i].Char = charValue;
                    i++;
                }
            }

            bool b = WriteConsoleOutput(h, buf, btmRight, topLeft, ref rect);

            //Console.SetCursorPosition(0, 0);
            //Console.Write(grid.Generation);
        }

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
            SafeFileHandle hConsoleOutput,
            CharInfo[] lpBuffer,
            Coord dwBufferSize,
            Coord dwBufferCoord,
            ref SmallRect lpWriteRegion);

        // Console window manipulation (prevent resize and close)
        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        #endregion
    }
}
