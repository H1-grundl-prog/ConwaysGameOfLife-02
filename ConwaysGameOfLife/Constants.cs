using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife
{
    public class Constants
    {
        public const short SCREEN_WIDTH = 200;
        public const short SCREEN_HEIGHT = 40;
        public const short WORLD_WIDTH = 200;
        public const short WORLD_HEIGHT = 80;
        public const short INIT_POPULATION_PERCENTAGE = 35;
        public const short FOREGROUND_COLOR = (short)ConsoleColor.Yellow;
        public const short BACKGROUND_COLOR = (short)ConsoleColor.DarkGray;

        public const char TOP_AND_BOTTOM_CHAR = (char)219;
        public const char TOP_CHAR = (char)223;
        public const char BOTTOM_CHAR = (char)220;
        public const char BACKGROUND_CHAR = (char)32;

        // Console window manipulation (prevent resize and close)
        public const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;
    }
}
