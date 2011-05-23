using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace runner
{
    /// <summary>
    /// Static class representing the game state - scores, speed etc.
    /// </summary>
    static class GameState
    {
        public static Player player;
        public static Random random;
        public static float scrollingSpeed;
        public static float score;
        public static String scoreString;
        public static String debug;
    }
}
