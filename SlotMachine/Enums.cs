using System;
using System.Collections.Generic;
using System.Text;

namespace SlotMachine
{
    public static class Enums
    {
        public enum GameMode
        {
            CHOOSE_ALL_HORIZONTAL = 1,
            CHOOSE_ALL_VERTICAL = 2,
            CHOOSE_ALL_DIAGONAL = 3,
            CHOOSE_ALL_LINES = 4
        }
        enum gameState
        {
            CONTINUE_GAME = 1,
            END_GAME = 0
        }
    }
}
